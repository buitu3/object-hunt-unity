using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using MyNamespace;
using OjbectHunt.Common;
using OjbectHunt.Data;
using OjbectHunt.Map;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace OjbectHunt.Editor
{
    public class MapCreator : MonoBehaviour
    {
#if UNITY_EDITOR

        [SerializeField] private MapsCollectionSO MapDataCollection;

        [Title("Map properties")] [FoldoutGroup("Create new Map", Expanded = true)]
        public string MapName;
        
        [FoldoutGroup("Create new Map")]
        public Sprite MapPreview;

        [FoldoutGroup("Create new Map")]
        [Tooltip("The number of Area in this map")]
        [OnValueChanged("OnAreaCountChanged")]
        public int NumberOfArea;

        [FoldoutGroup("Create new Map")] public int NumOfAreaInAColumn;

        [Title("Area Backgrounds properties")] [FoldoutGroup("Create new Map")]
        public int BGsRowInArea;

        [FoldoutGroup("Create new Map")] public int BGsColumnInArea;

        [FoldoutGroup("Create new Map")]
        public SerializableList<SerializableList<Sprite>> MapAreaBGLst =
            new SerializableList<SerializableList<Sprite>>();

        [FoldoutGroup("Create new Map")]
        [Button(ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1f)]
        private void CreateMap()
        {
            // Get map template and are template based on pre defined path
            GameObject mapTemplate = AssetDatabase.LoadAssetAtPath<GameObject>(EditorConstant.EDITOR_MAP_TEMPLATE_PATH);
            GameObject areaTemplate =
                AssetDatabase.LoadAssetAtPath<GameObject>(EditorConstant.EDITOR_AREA_TEMPLATE_PATH);

            // Clear old map prefab
            UnityUtilities.ClearAllChildren(transform);
            // Instantiate map prefab in scene
            var newMap = ((GameObject)PrefabUtility.InstantiatePrefab(mapTemplate, transform)).GetComponent<LevelMap>();
            if (!string.IsNullOrEmpty(MapName)) newMap.name = MapName;

            // Init Areas
            for (int i = 0; i < MapAreaBGLst.Count; i++)
            {
                var newArea = ((GameObject)PrefabUtility.InstantiatePrefab(areaTemplate, newMap.transform))
                    .GetComponent<MapArea>();
                newArea.name = "Area" + (i + 1);
                newMap.AreaLst.Add(newArea);

                int areaXIndex = i % NumOfAreaInAColumn;
                int areaYIndex = i / NumOfAreaInAColumn;
                var areaWidth = MapAreaBGLst[i][0].bounds.size.x * BGsColumnInArea;
                var areaHeight = MapAreaBGLst[i][0].bounds.size.y * BGsRowInArea;
                var posX = (areaXIndex * areaWidth) + (areaWidth / 2);
                var posY = (areaYIndex * areaHeight) + (areaHeight / 2);
                newArea.transform.position = new Vector3(posX, posY, 0);

                InitAreaBGs(newArea, MapAreaBGLst[i]);
            }

            // Save the current map object in scene as a new prefab
            var newMapPrefab = PrefabUtility.SaveAsPrefabAsset(newMap.gameObject,
                EditorConstant.EDITOR_MAP_PREFAB_SAVING_FOLDER_PATH + newMap.gameObject.name + ".prefab");

            // Create new map hidden object data in a scriptable object
            var newHiddenObjectData = ScriptableObject.CreateInstance<MapObjectDataSO>();
            AssetDatabase.CreateAsset(newHiddenObjectData,
                EditorConstant.EDITOR_MAP_OBJECT_DATA_SAVING_FOLDER_PATH + newMap.name + "_ObjectData.asset");

            // Create new map data in a scriptable object and add to collection
            var newMapData = ScriptableObject.CreateInstance<MapDataSO>();
            newMapData.MapName = MapName;
            newMapData.MapPreview = MapPreview;
            newMapData.MapPrefab = newMapPrefab;
            newMapData.HiddenObjectsData = newHiddenObjectData;
            
            AssetDatabase.CreateAsset(newMapData,
                EditorConstant.EDITOR_MAP_DATA_SAVING_FOLDER_PATH + newMap.name + ".asset");
            MapDataCollection.MapLst.Add(newMapData);

            // Get the Map editor component and load newly created map data into it
            var mapEditor = GetComponent<MapEditor>();
            if (mapEditor != null)
            {
                mapEditor.CurrentMapData = newMapData;
                mapEditor.LoadMapData();
            }
        }

        private void InitAreaBGs(MapArea area, List<Sprite> bgList)
        {
            if (bgList.Count == 0) return;

            var bgWidth = bgList[0].bounds.size.x;
            var bgHeight = bgList[0].bounds.size.y;
            var bgContainer = area.transform.Find("BG Container");

            if (bgContainer == null)
            {
                Debug.LogError("Could not find BG Container in Area template");
                return;
            }

            for (int i = 0; i < bgList.Count; i++)
            {
                var newBG = new GameObject();
                newBG.name = "BG" + (i + 1);
                newBG.transform.parent = bgContainer;

                var newBGSprite = newBG.AddComponent<SpriteRenderer>();
                newBGSprite.sprite = bgList[i];

                int xIndex = i % BGsColumnInArea;
                int yIndex = i / BGsColumnInArea;

                var posX = (xIndex * bgWidth) + (bgWidth / 2);
                var posY = (yIndex * bgHeight) + (bgHeight / 2);
                newBGSprite.transform.localPosition = new Vector3(posX, posY, 0);
            }
        }

        private void OnAreaCountChanged()
        {
            int originalCount = MapAreaBGLst.Count;
            if (originalCount > NumberOfArea) MapAreaBGLst.RemoveRange(NumberOfArea, originalCount - NumberOfArea);
            else if (originalCount < NumberOfArea)
            {
                for (int i = 0; i < NumberOfArea - originalCount; i++) MapAreaBGLst.Add(new SerializableList<Sprite>());
            }
        }

        [FoldoutGroup("Import Map", Expanded = true)]
        public MapDataSO ImportMapData;

        [FoldoutGroup("Import Map", Expanded = true)]
        [Button(ButtonSizes.Large), GUIColor(1f, 0.4f, 0.8f)]
        private void ImportMap()
        {
            if(ImportMapData == null) Debug.LogError("There are no import map data selected");
            
            // Get the Map editor component and load newly created map data into it
            var mapEditor = GetComponent<MapEditor>();
            if (mapEditor == null) Debug.LogError("Could not get the map editor component");
            
            mapEditor.CurrentMapData = ImportMapData;
            mapEditor.LoadMapData();
        }

#endif
    }
}