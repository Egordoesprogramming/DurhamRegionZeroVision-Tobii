namespace TurnTheGameOn.SimpleTrafficSystem
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "AssetReferences", menuName = "TurnTheGameOn/STS/AssetReferences")]
    public class AssetReferences : ScriptableObject
    {
        public GameObject _AITrafficController;
        public GameObject _AITrafficLightManager;
        public GameObject _AITrafficSpawnPoint;
        public GameObject _AITrafficStopManager;
        public GameObject _AITrafficWaypoint;
        public GameObject _AITrafficWaypointRoute;
        public GameObject _SplineRouteCreator;
        public GameObject _YieldTrigger;
    }
}