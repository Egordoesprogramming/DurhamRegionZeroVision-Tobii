namespace TurnTheGameOn.SimpleTrafficSystem
{
    using UnityEngine;

    [HelpURL("https://simpletrafficsystem.turnthegameon.com/documentation/api/aitrafficstop")]
    public class AITrafficStop : MonoBehaviour
    {
        [Tooltip("Cars can't exit assigned route until AITrafficStopManager allows it.")]
        public AITrafficWaypointRoute waypointRoute;
        public bool stopForTraffic { get; private set; }

        public void StopTraffic()
        {
            waypointRoute.StopForTrafficlight(true);
            stopForTraffic = true;
        }

        public void AllowCarToProceed()
        {
            waypointRoute.StopForTrafficlight(false);
            stopForTraffic = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = stopForTraffic ? Color.red : Color.green;
            Gizmos.DrawCube(transform.position, new Vector3(1, 2, 1));
        }

    }
}