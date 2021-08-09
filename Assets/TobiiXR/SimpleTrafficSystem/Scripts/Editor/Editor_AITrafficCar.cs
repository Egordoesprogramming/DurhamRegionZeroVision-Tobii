namespace TurnTheGameOn.SimpleTrafficSystem
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(AITrafficCar))]
    public class Editor_AITrafficCar : Editor
    {
        private static bool showSensorSettings;

        public override void OnInspectorGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((AITrafficCar)target), typeof(AITrafficCar), false);
            GUI.enabled = true;
            EditorGUIUtility.wideMode = true;

            SerializedProperty vehicleType = serializedObject.FindProperty("vehicleType");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(vehicleType, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty topSpeed = serializedObject.FindProperty("topSpeed");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(topSpeed, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty accelerationPower = serializedObject.FindProperty("accelerationPower");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(accelerationPower, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty minDrag = serializedObject.FindProperty("minDrag");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(minDrag, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty minAngularDrag = serializedObject.FindProperty("minAngularDrag");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(minAngularDrag, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            showSensorSettings = EditorGUILayout.Foldout(showSensorSettings, "Sensor Settings", true);
            if (showSensorSettings)
            {
                SerializedProperty frontSensorSize = serializedObject.FindProperty("frontSensorSize");
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(frontSensorSize, true);
                if (EditorGUI.EndChangeCheck())
                    serializedObject.ApplyModifiedProperties();

                SerializedProperty frontSensorLength = serializedObject.FindProperty("frontSensorLength");
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(frontSensorLength, true);
                if (EditorGUI.EndChangeCheck())
                    serializedObject.ApplyModifiedProperties();

                SerializedProperty sideSensorSize = serializedObject.FindProperty("sideSensorSize");
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(sideSensorSize, true);
                if (EditorGUI.EndChangeCheck())
                    serializedObject.ApplyModifiedProperties();

                SerializedProperty sideSensorLength = serializedObject.FindProperty("sideSensorLength");
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(sideSensorLength, true);
                if (EditorGUI.EndChangeCheck())
                    serializedObject.ApplyModifiedProperties();
            }

            SerializedProperty goToStartOnStop = serializedObject.FindProperty("goToStartOnStop");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(goToStartOnStop, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();

            SerializedProperty brakeMaterial = serializedObject.FindProperty("brakeMaterial");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(brakeMaterial, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty brakeMaterialMesh = serializedObject.FindProperty("brakeMaterialMesh");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(brakeMaterialMesh, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty brakeMaterialIndex = serializedObject.FindProperty("brakeMaterialIndex");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(brakeMaterialIndex, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty frontSensorTransform = serializedObject.FindProperty("frontSensorTransform");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(frontSensorTransform, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty leftSensorTransform = serializedObject.FindProperty("leftSensorTransform");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(leftSensorTransform, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty rightSensorTransform = serializedObject.FindProperty("rightSensorTransform");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(rightSensorTransform, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty headLight = serializedObject.FindProperty("headLight");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(headLight, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty _wheels = serializedObject.FindProperty("_wheels");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_wheels, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Align Wheel Colliders"))
            {
                AlignWheelColliders();
            }
        }

        public void AlignWheelColliders()
        {
            AITrafficCar driveSystem = (AITrafficCar)target;
            Transform defaultColliderParent = driveSystem._wheels[0].collider.transform.parent; // make a reference to the colliders original parent

            driveSystem._wheels[0].collider.transform.parent = driveSystem._wheels[0].mesh.transform;// move colliders to the reference positions
            driveSystem._wheels[1].collider.transform.parent = driveSystem._wheels[1].mesh.transform;
            driveSystem._wheels[2].collider.transform.parent = driveSystem._wheels[2].mesh.transform;
            driveSystem._wheels[3].collider.transform.parent = driveSystem._wheels[3].mesh.transform;

            driveSystem._wheels[0].collider.transform.position = new Vector3(driveSystem._wheels[0].mesh.transform.position.x,
                driveSystem._wheels[0].collider.transform.position.y, driveSystem._wheels[0].mesh.transform.position.z); //adjust the wheel collider positions on x and z axis to match the new wheel position
            driveSystem._wheels[1].collider.transform.position = new Vector3(driveSystem._wheels[1].mesh.transform.position.x,
                driveSystem._wheels[1].collider.transform.position.y, driveSystem._wheels[1].mesh.transform.position.z);
            driveSystem._wheels[2].collider.transform.position = new Vector3(driveSystem._wheels[2].mesh.transform.position.x,
                driveSystem._wheels[2].collider.transform.position.y, driveSystem._wheels[2].mesh.transform.position.z);
            driveSystem._wheels[3].collider.transform.position = new Vector3(driveSystem._wheels[3].mesh.transform.position.x,
                driveSystem._wheels[3].collider.transform.position.y, driveSystem._wheels[3].mesh.transform.position.z);

            driveSystem._wheels[0].collider.transform.parent = defaultColliderParent; // move colliders back to the original parent
            driveSystem._wheels[1].collider.transform.parent = defaultColliderParent;
            driveSystem._wheels[2].collider.transform.parent = defaultColliderParent;
            driveSystem._wheels[3].collider.transform.parent = defaultColliderParent;
        }
    }
}