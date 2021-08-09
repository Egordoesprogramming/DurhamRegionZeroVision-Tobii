namespace TurnTheGameOn.SimpleTrafficSystem
{
    using UnityEngine;
    using Unity.Mathematics;
    using Unity.Collections;
    using Unity.Burst;
    using UnityEngine.Jobs;

    [BurstCompile]
    public struct AITrafficCarJob : IJobParallelForTransform
    {
        public float deltaTime;
        public float maxSteerAngle;
        public float speedMultiplier;
        public float steerSensitivity;
        public float stopThreshold;
        public NativeArray<float> frontSensorLengthNA;
        public NativeArray<int> currentRoutePointIndexNA;
        public NativeArray<int> waypointDataListCountNA;
        public NativeArray<bool> isDrivingNA;
        public NativeArray<bool> isActiveNA;
        public NativeArray<bool> canProcessNA;
        public NativeArray<bool> overrideInputNA;
        public NativeArray<bool> isBrakingNA;
        public NativeArray<bool> frontHitNA;
        public NativeArray<bool> stopForTrafficLightNA;
        public NativeArray<bool> yieldForCrossTrafficNA;
        public NativeArray<float> accelerationPowerNA;
        public NativeArray<float> speedNA;
        public NativeArray<float> topSpeedNA;
        public NativeArray<float> routeProgressNA;
        public NativeArray<float> targetSpeedNA;
        public NativeArray<float> speedLimitNA;
        public NativeArray<float> accelNA;
        public NativeArray<float> targetAngleNA;
        public NativeArray<float> moveSteerNA;
        public NativeArray<float> moveAccelNA;
        public NativeArray<float> moveFootBrakeNA;
        public NativeArray<float> moveHandBrakeNA;
        public NativeArray<float> overrideAccelerationPowerNA;
        public NativeArray<float> overrideBrakePowerNA;
        public NativeArray<float> frontHitDistanceNA;
        public NativeArray<float> distanceToEndPointNA;
        public NativeArray<float3> routePointPositionNA;
        public NativeArray<Vector3> carTransformPreviousPositionNA;
        public NativeArray<Vector3> carTransformPositionNA;
        public NativeArray<Vector3> localTargetNA;

        public void Execute(int index, TransformAccess driveTargetTransformAccessArray)
        {
            if (isActiveNA[index] && canProcessNA[index])
            {
                driveTargetTransformAccessArray.position = routePointPositionNA[index];

                #region StopThreshold
                if (stopForTrafficLightNA[index] && routeProgressNA[index] > 0 && currentRoutePointIndexNA[index] >= waypointDataListCountNA[index] - 2)
                {
                    distanceToEndPointNA[index] = Vector3.Distance(carTransformPositionNA[index], routePointPositionNA[index]);
                    if (!overrideInputNA[index])
                    {
                        overrideInputNA[index] = true;
                        overrideBrakePowerNA[index] = 1f;
                        overrideAccelerationPowerNA[index] = 0f;
                    }
                }
                else if (frontHitNA[index] && frontHitDistanceNA[index] < stopThreshold)
                {
                    if (!overrideInputNA[index])
                    {
                        overrideInputNA[index] = true;
                        overrideBrakePowerNA[index] = 1f;
                        overrideAccelerationPowerNA[index] = 0f;
                    }
                }
                else if (yieldForCrossTrafficNA[index])
                {
                    if (!overrideInputNA[index])
                    {
                        overrideInputNA[index] = true;
                        overrideBrakePowerNA[index] = 1f;
                        overrideAccelerationPowerNA[index] = 0f;
                    }
                }
                else if (overrideInputNA[index])
                {
                    overrideBrakePowerNA[index] = 0f;
                    overrideAccelerationPowerNA[index] = 0f;
                    overrideInputNA[index] = false;
                }
                #endregion

                #region move
                if (isDrivingNA[index])
                {
                    targetSpeedNA[index] = topSpeedNA[index];
                    if (targetSpeedNA[index] > speedLimitNA[index]) targetSpeedNA[index] = speedLimitNA[index];
                    if (frontHitNA[index]) targetSpeedNA[index] = Mathf.InverseLerp(0, frontSensorLengthNA[index], frontHitDistanceNA[index]) * targetSpeedNA[index];
                    accelNA[index] = targetSpeedNA[index] - speedNA[index];
                    localTargetNA[index] = driveTargetTransformAccessArray.localPosition;
                    targetAngleNA[index] = math.atan2(localTargetNA[index].x, localTargetNA[index].z) * 52.29578f;
                    moveSteerNA[index] = math.clamp(targetAngleNA[index] * steerSensitivity, -1, 1) * math.sign(speedNA[index]);
                    moveSteerNA[index] *= maxSteerAngle;
                    if (speedNA[index] > topSpeedNA[index]) moveAccelNA[index] = 0;
                    else moveAccelNA[index] = (math.clamp(accelNA[index], 0, 1)) * accelerationPowerNA[index];
                    moveFootBrakeNA[index] = (-1 * math.clamp(accelNA[index], -1, 0));
                    moveHandBrakeNA[index] = 0;
                }
                else
                {
                    if (speedNA[index] > 2)
                    {
                        localTargetNA[index] = driveTargetTransformAccessArray.localPosition;
                        targetAngleNA[index] = math.atan2(localTargetNA[index].x, localTargetNA[index].z) * 52.29578f;
                        moveSteerNA[index] = math.clamp(targetAngleNA[index] * steerSensitivity, -1, 1) * math.sign(speedNA[index]);
                        moveSteerNA[index] *= maxSteerAngle;
                        moveAccelNA[index] = 0;
                        moveFootBrakeNA[index] = -1;
                        moveHandBrakeNA[index] = 1;
                    }
                    else
                    {
                        moveSteerNA[index] = 0;
                        moveAccelNA[index] = 0;
                        moveFootBrakeNA[index] = -1;
                        moveHandBrakeNA[index] = 1;
                    }
                }

                if (overrideInputNA[index])
                {
                    moveAccelNA[index] = overrideAccelerationPowerNA[index] * accelerationPowerNA[index];
                    moveFootBrakeNA[index] = overrideBrakePowerNA[index];
                }
                if (moveFootBrakeNA[index] > 0.0f) isBrakingNA[index] = true;
                else if (moveFootBrakeNA[index] == 0.0f) isBrakingNA[index] = false;

                speedNA[index] = ((carTransformPositionNA[index] - carTransformPreviousPositionNA[index]).magnitude / deltaTime) * speedMultiplier;
                #endregion
            }
        }
    }
}