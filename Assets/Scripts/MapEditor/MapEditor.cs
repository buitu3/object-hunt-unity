using System.Collections;
using System.Collections.Generic;
using OjbectHunt.Common;
using OjbectHunt.Map;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace OjbectHunt.Editor
{
    public class MapEditor : MonoBehaviour
    {
        #if UNITY_EDITOR
        
        [OnValueChanged("LoadMapData")]
        public MapDataSO CurrentMapData;

        [ReadOnly]
        [InlineEditor]
        public MapObjectDataSO CurrentMapObjectData;

        [TableList]
        public List<MapAreaDataView> AreaDatas = new List<MapAreaDataView>();
        
        private void LoadMapData()
        {
            if (CurrentMapData == null)
            {
                CleanData();
                return;
            }

            CurrentMapObjectData = CurrentMapData.HiddenObjectsData;

            // Check if gameobject has any child, if yes destroy them
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    DestroyImmediate(child.gameObject);
                }
            }
            
            // Instantiate a temp map prefab as the child of this gameobject
            var tempMapPrefab = PrefabUtility.InstantiatePrefab(CurrentMapData.MapPrefab, transform) as GameObject;
            var MapAreas = tempMapPrefab.GetComponent<LevelMap>();
            
            // Iterate through all areas in map and track hidden objects in each area
            AreaDatas.Clear();  
            foreach (var area in MapAreas.AreaLst)
            {
                var currentArea = new MapAreaDataView();
                currentArea.AreaName = area.gameObject.name;
                currentArea.RepresentArea = area;
                
                currentArea.LoadObjectInArea(area, CurrentMapData.HiddenObjectsData);
                AreaDatas.Add(currentArea);
            } 
        }

        [FoldoutGroup("Create new Map", Expanded = true)]
        public string MapName;
        [FoldoutGroup("Create new Map")]
        public SerializableList<SerializableList<Sprite>> MapAreaBGLst = new SerializableList<SerializableList<Sprite>>();

        [FoldoutGroup("Create new Map")]
        [Button(ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1f)]
        private void CreateMap()
        {
            // Get map template and are template based on pre defined path
            GameObject mapTemplate = AssetDatabase.LoadAssetAtPath<GameObject>(EditorConstant.EDITOR_MAP_TEMPLATE_PATH);
            GameObject areaTemplate = AssetDatabase.LoadAssetAtPath<GameObject>(EditorConstant.EDITOR_AREA_TEMPLATE_PATH);

            var newMap = ((GameObject)PrefabUtility.InstantiatePrefab(mapTemplate, transform)).GetComponent<LevelMap>();
            if(!string.IsNullOrEmpty(MapName)) newMap.name = MapName;
            
            for (int i = 0; i < MapAreaBGLst.Count; i++)
            {
                var newArea = ((GameObject)PrefabUtility.InstantiatePrefab(areaTemplate, newMap.transform)).GetComponent<MapArea>();
                newArea.name = "Area" + (i + 1);
                newMap.AreaLst.Add(newArea);
                
                var bgContainer = newArea.transform.Find("BG Container");
                
                // Init BG for area
                for (int j = 0; j < MapAreaBGLst[i].Count; j++)
                {
                    var newBG = new GameObject();
                    newBG.name = "BG" + (j + 1); 
                    newBG.transform.parent = bgContainer;
                    
                    var newBGSprite = newBG.AddComponent<SpriteRenderer>();
                    newBGSprite.sprite = MapAreaBGLst[i][j];
                }
            }
        }

        [FoldoutGroup("Import Map", Expanded = true)]
        public GameObject ImportTemplate;

        [FoldoutGroup("Import Map", Expanded = true)]
        [Button(ButtonSizes.Large), GUIColor(1f, 0.4f, 0.8f)]
        private void ImportMap()
        {
            
        }

        private void CleanData()
        {
            CurrentMapObjectData = null;
            AreaDatas.Clear();
        }
        
        #endif
    }
}

