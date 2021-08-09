namespace TurnTheGameOn.SimpleTrafficSystem
{
    using System.Collections.Generic;
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

    public static class STSPrefs
    {
#pragma warning disable 0414
        private static bool loaded = false;
#pragma warning restore 0414
        public static bool routeGizmos = true;
        public static Color pathColor = new Color(1, 0.7725532f, 0, 1);
        public static Color selectedPathColor = new Color(0, 1, 0, 1);
        public static Color junctionColor = new Color(1, 1, 0, 0.298f);
        public static Color selectedJunctionColor = new Color(1, 1, 0, 1);
        public static Color yieldTriggerColor = new Color(1, 1, 0, 0.25f);
        public static Color selectedYieldTriggerColor = new Color(1, 1, 0, 0.5f);
        public static Vector3 arrowScale = Vector3.one;

        public static bool waypointGizmos = true;
        public static Color pointColor = new Color(1, 1, 1, 0.75f);

        public static bool hideSpawnPointsInEditMode = false;

        public static bool sensorGizmos = false;
        public static bool sideSensorGizmos = true;
        public static Color detectColor = new Color(1, 0, 0, 0.25f);
        public static Color normalColor = new Color(1, 1, 1, 0.25f);

        public static bool poolGizmos = true;
        public static Color minSpawnZoneColor = new Color(0.86f, 0.86f, 0.86f, 0.45f);
        public static Color cullHeadLightZone = new Color(1, 0, 0, 0.39f);
        public static Color activeZoneColor = new Color(0, 0.04f, 0.61f, 0.25f);
        public static Color spawnZoneColor = new Color(0.88f, 0, 1, 0.25f);

        public static bool debugProcessTime = true;

        public static Color handleColor = new Color(1, 1, 1, 0.8f);
        public static Color handleSelectedColor = new Color(1, 0, 0, 0.8f);
        public static Color handleTextColor = new Color(0, 0, 0, 1);
        public static Color handleTextSelectedColor = new Color(1, 1, 1, 1);
        public static Color connectionColor = new Color(1, 0, 0, 1);
        public static float drawDistance = 150;

#if UNITY_EDITOR
        static STSPrefs()
        {
            LoadPrefs();
        }

        [SettingsProvider]
        public static SettingsProvider STSSettingsProvider()
        {
            SettingsProvider provider = new SettingsProvider("Preferences/Simple Traffic System", SettingsScope.User)
            {
                label = "Simple Traffic System",
                guiHandler = (searchContext) =>
                {
                    OnGUI();
                },
                keywords = new HashSet<string>(new[] { "TurnTheGameOn", "Simple", "Traffic", "System" })
            };
            return provider;
        }

        public static void OnGUI()
        {
            if (!loaded) LoadPrefs();
            EditorGUILayout.Space();
            EditorGUIUtility.wideMode = true;

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Route Gizmos", EditorStyles.boldLabel);
            routeGizmos = EditorGUILayout.Toggle("Show", routeGizmos);
            pathColor = EditorGUILayout.ColorField("Path", pathColor);
            selectedPathColor = EditorGUILayout.ColorField("Path Selected", selectedPathColor);
            junctionColor = EditorGUILayout.ColorField("Junction", junctionColor);
            selectedJunctionColor = EditorGUILayout.ColorField("Junction Selected", selectedJunctionColor);
            yieldTriggerColor = EditorGUILayout.ColorField("Yield Trigger", yieldTriggerColor);
            selectedYieldTriggerColor = EditorGUILayout.ColorField("Yield Trigger Selected", selectedYieldTriggerColor);
            arrowScale = EditorGUILayout.Vector3Field("Arrow Scale", arrowScale);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Waypoint Gizmos", EditorStyles.boldLabel);
            waypointGizmos = EditorGUILayout.Toggle("Show", waypointGizmos);
            pointColor = EditorGUILayout.ColorField("Point", pointColor);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Spawn Points", EditorStyles.boldLabel);
            hideSpawnPointsInEditMode = EditorGUILayout.Toggle("Hide In Edit Mode", hideSpawnPointsInEditMode);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Car Sensor Gizmos", EditorStyles.boldLabel);
            sensorGizmos = EditorGUILayout.Toggle("Show", sensorGizmos);
            sideSensorGizmos = EditorGUILayout.Toggle("Force Side On", sideSensorGizmos);
            detectColor = EditorGUILayout.ColorField("Detect", detectColor);
            normalColor = EditorGUILayout.ColorField("Normal", normalColor);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Traffic Pool Area Gizmos", EditorStyles.boldLabel);
            poolGizmos = EditorGUILayout.Toggle("Show", poolGizmos);
            minSpawnZoneColor = EditorGUILayout.ColorField("Min Spawn Zone", minSpawnZoneColor);
            cullHeadLightZone = EditorGUILayout.ColorField("Cull Head Light Zone", cullHeadLightZone);
            activeZoneColor = EditorGUILayout.ColorField("Active Zone", activeZoneColor);
            spawnZoneColor = EditorGUILayout.ColorField("Spawn Zone", spawnZoneColor);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("AI Traffic Controller", EditorStyles.boldLabel);
            debugProcessTime = EditorGUILayout.Toggle("Debug Job Time", debugProcessTime);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("STS Tools Scene View Handles", EditorStyles.boldLabel);
            handleColor = EditorGUILayout.ColorField("Handle", handleColor);
            handleSelectedColor = EditorGUILayout.ColorField("Handle Selected", handleSelectedColor);
            handleTextColor = EditorGUILayout.ColorField("Handle Text", handleTextColor);
            handleTextSelectedColor = EditorGUILayout.ColorField("Handle Text Selected", handleTextSelectedColor);
            connectionColor = EditorGUILayout.ColorField("Connection", connectionColor);
            drawDistance = EditorGUILayout.FloatField("Draw Distance", drawDistance);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            if (GUILayout.Button("Use Defaults", GUILayout.Width(120)))
            {
                routeGizmos = true;
                sideSensorGizmos = true;
                yieldTriggerColor = new Color(1, 1, 0, 0.25f);
                selectedYieldTriggerColor = new Color(1, 1, 0, 0.5f);
                pathColor = new Color(1, 0.7725532f, 0, 1);
                selectedPathColor = new Color(0, 1, 0, 1);
                junctionColor = new Color(1, 1, 0, 0.298f);
                selectedJunctionColor = new Color(1, 1, 0, 1);
                sensorGizmos = true;
                detectColor = new Color(1, 0, 0, 0.25f);
                normalColor = new Color(1, 1, 1, 0.25f);
                debugProcessTime = false;
                waypointGizmos = true;
                pointColor = new Color(1, 1, 1, 0.75f);
                hideSpawnPointsInEditMode = false;
                poolGizmos = true;
                minSpawnZoneColor = new Color(0.86f, 0.86f, 0.86f, 0.45f);
                cullHeadLightZone = new Color(1, 0, 0, 0.39f);
                activeZoneColor = new Color(0, 0.04f, 0.61f, 0.25f);
                spawnZoneColor = new Color(0.88f, 0, 1, 0.25f);
                arrowScale = Vector3.one;
                handleColor = new Color(1, 1, 1, 0.8f);
                handleSelectedColor = new Color(1, 0, 0, 0.8f);
                handleTextColor = new Color(0, 0, 0, 1);
                handleTextSelectedColor = new Color(1, 1, 1, 1);
                connectionColor = new Color(1, 0, 0, 1);
                drawDistance = 150;
                SavePrefs();
            }
            if (GUI.changed)
            {
                SavePrefs();
                SceneView.RepaintAll();
            }
        }

        public static void LoadPrefs()
        {
            routeGizmos = EditorPrefs.GetBool("STS.routeGizmos", true);
            sideSensorGizmos = EditorPrefs.GetBool("STS.sideSensorGizmos", true);
            yieldTriggerColor = LoadColor("STS.yieldTriggerColor", new Color(1, 1, 0, 0.25f));
            selectedYieldTriggerColor = LoadColor("STS.selectedYieldTriggerColor", new Color(1, 1, 0, 0.5f));
            pathColor = LoadColor("STS.pathColor", new Color(1, 0.7725532f, 0, 1));
            selectedPathColor = LoadColor("STS.selectedPathColor", new Color(0, 1, 0, 1));
            junctionColor = LoadColor("STS.junctionColor", new Color(1, 1, 0, 0.298f));
            selectedJunctionColor = LoadColor("STS.selectedJunctionColor", new Color(1, 1, 0, 1));
            sensorGizmos = EditorPrefs.GetBool("STS.sensorGizmos", true);
            detectColor = LoadColor("STS.detectColor", new Color(1, 0, 0, 0.25f));
            normalColor = LoadColor("STS.normalColor", new Color(1, 1, 1, 0.25f));
            debugProcessTime = EditorPrefs.GetBool("STS.debugProcessTime", false);
            waypointGizmos = EditorPrefs.GetBool("STS.waypointGizmos", true);
            pointColor = LoadColor("STS.pointColor", new Color(1, 1, 1, 0.75f));
            hideSpawnPointsInEditMode = EditorPrefs.GetBool("STS.hideSpawnPointsInEditMode", false);
            poolGizmos = EditorPrefs.GetBool("STS.poolGizmos", true);
            minSpawnZoneColor = LoadColor("STS.minSpawnZoneColor", new Color(0.86f, 0.86f, 0.86f, 0.45f));
            cullHeadLightZone = LoadColor("STS.cullHeadLightZone", new Color(1, 0, 0, 0.39f));
            activeZoneColor = LoadColor("STS.activeZoneColor", new Color(0, 0.04f, 0.61f, 0.25f));
            spawnZoneColor = LoadColor("STS.spawnZoneColor", new Color(0.88f, 0, 1, 0.25f));
            Vector3 arrwScaleVector3 = new Vector3();
            arrwScaleVector3.x = EditorPrefs.GetFloat("STS.arrowScale.x", 1f);
            arrwScaleVector3.y = EditorPrefs.GetFloat("STS.arrowScale.y", 1f);
            arrwScaleVector3.z = EditorPrefs.GetFloat("STS.arrowScale.z", 1f);
            arrowScale = arrwScaleVector3;
            handleColor = LoadColor("STS.handleColor", new Color(1, 1, 1, 0.8f));
            handleSelectedColor = LoadColor("STS.handleSelectedColor", new Color(1, 0, 0, 0.8f));
            handleTextColor = LoadColor("STS.handleTextColor", new Color(0, 0, 0, 1));
            handleTextSelectedColor = LoadColor("STS.handleTextSelectedColor", new Color(1, 1, 1, 1));
            connectionColor = LoadColor("STS.connectionColor", new Color(1, 0, 0, 1));
            drawDistance = EditorPrefs.GetFloat("STS.drawDistance", 150);

            loaded = true;
        }

        private static Color LoadColor(string name, Color defaultValue)
        {
            Color col = Color.white;
            string colorString = EditorPrefs.GetString(name, defaultValue.r + ":" + defaultValue.g + ":" + defaultValue.b + ":" + defaultValue.a);
            string[] elements = colorString.Split(':');
            if (elements.Length < 4) return col;
            float r = 0f, g = 0f, b = 0f, a = 0f;
            float.TryParse(elements[0], out r);
            float.TryParse(elements[1], out g);
            float.TryParse(elements[2], out b);
            float.TryParse(elements[3], out a);
            col = new Color(r, g, b, a);
            return col;
        }

        public static void SavePrefs()
        {
            EditorPrefs.SetBool("STS.routeGizmos", routeGizmos);
            EditorPrefs.SetBool("STS.sideSensorGizmos", sideSensorGizmos);
            EditorPrefs.SetString("STS.yieldTriggerColor", yieldTriggerColor.r + ":" + yieldTriggerColor.g + ":" + yieldTriggerColor.b + ":" + yieldTriggerColor.a);
            EditorPrefs.SetString("STS.selectedYieldTriggerColor", selectedYieldTriggerColor.r + ":" + selectedYieldTriggerColor.g + ":" + selectedYieldTriggerColor.b + ":" + selectedYieldTriggerColor.a);
            EditorPrefs.SetString("STS.pathColor", pathColor.r + ":" + pathColor.g + ":" + pathColor.b + ":" + pathColor.a);
            EditorPrefs.SetString("STS.selectedPathColor", selectedPathColor.r + ":" + selectedPathColor.g + ":" + selectedPathColor.b + ":" + selectedPathColor.a);
            EditorPrefs.SetString("STS.junctionColor", junctionColor.r + ":" + junctionColor.g + ":" + junctionColor.b + ":" + junctionColor.a);
            EditorPrefs.SetString("STS.selectedJunctionColor", selectedJunctionColor.r + ":" + selectedJunctionColor.g + ":" + selectedJunctionColor.b + ":" + selectedJunctionColor.a);
            EditorPrefs.SetBool("STS.sensorGizmos", sensorGizmos);
            EditorPrefs.SetString("STS.detectColor", detectColor.r + ":" + detectColor.g + ":" + detectColor.b + ":" + detectColor.a);
            EditorPrefs.SetString("STS.normalColor", normalColor.r + ":" + normalColor.g + ":" + normalColor.b + ":" + normalColor.a);
            EditorPrefs.SetBool("STS.waypointGizmos", waypointGizmos);
            EditorPrefs.SetString("STS.pointColor", pointColor.r + ":" + pointColor.g + ":" + pointColor.b + ":" + pointColor.a);
            EditorPrefs.SetBool("STS.hideSpawnPointsInEditMode", hideSpawnPointsInEditMode);
            EditorPrefs.SetBool("STS.poolGizmos", poolGizmos);
            EditorPrefs.SetString("STS.minSpawnZoneColor", minSpawnZoneColor.r + ":" + minSpawnZoneColor.g + ":" + minSpawnZoneColor.b + ":" + minSpawnZoneColor.a);
            EditorPrefs.SetString("STS.cullHeadLightZone", cullHeadLightZone.r + ":" + cullHeadLightZone.g + ":" + cullHeadLightZone.b + ":" + cullHeadLightZone.a);
            EditorPrefs.SetString("STS.activeZoneColor", activeZoneColor.r + ":" + activeZoneColor.g + ":" + activeZoneColor.b + ":" + activeZoneColor.a);
            EditorPrefs.SetString("STS.spawnZoneColor", spawnZoneColor.r + ":" + spawnZoneColor.g + ":" + spawnZoneColor.b + ":" + spawnZoneColor.a);
            EditorPrefs.SetBool("STS.routeGizmos", routeGizmos);
            EditorPrefs.SetFloat("STS.arrowScale.x", arrowScale.x);
            EditorPrefs.SetFloat("STS.arrowScale.y", arrowScale.y);
            EditorPrefs.SetFloat("STS.arrowScale.z", arrowScale.z);
            EditorPrefs.SetString("STS.handleColor", handleColor.r + ":" + handleColor.g + ":" + handleColor.b + ":" + handleColor.a);
            EditorPrefs.SetString("STS.handleSelectedColor", handleSelectedColor.r + ":" + handleSelectedColor.g + ":" + handleSelectedColor.b + ":" + handleSelectedColor.a);
            EditorPrefs.SetString("STS.handleTextColor", handleTextColor.r + ":" + handleTextColor.g + ":" + handleTextColor.b + ":" + handleTextColor.a);
            EditorPrefs.SetString("STS.handleTextSelectedColor", handleTextSelectedColor.r + ":" + handleTextSelectedColor.g + ":" + handleTextSelectedColor.b + ":" + handleTextSelectedColor.a);
            EditorPrefs.SetString("STS.connectionColor", connectionColor.r + ":" + connectionColor.g + ":" + connectionColor.b + ":" + connectionColor.a);
            EditorPrefs.SetFloat("STS.drawDistance", drawDistance);
        }
#endif
    }
}