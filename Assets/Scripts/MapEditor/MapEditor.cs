using System.Collections;
using System.Collections.Generic;
using MyNamespace;
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
    [OnInspectorInit("OnInspectorInit")]
    public class MapEditor : MonoBehaviour
    {
        #if UNITY_EDITOR
        
        [ReadOnly]
        public MapDataSO CurrentMapData;

        [InlineEditor]
        public MapObjectDataSO CurrentMapObjectData;

        [TableList]
        public List<MapAreaDataView> AreaDatas = new List<MapAreaDataView>();
        
        public void LoadMapData()
        {
            if (CurrentMapData == null)
            {
                CleanupData();
                return;
            }
            
            // Remove event listener from old map object data
            if (CurrentMapObjectData != null && CurrentMapObjectData.OnObjDictSizeChanged.GetInvocationList().Length > 0)
            {
                CurrentMapObjectData.OnObjDictSizeChanged = null;
            }
            CurrentMapObjectData = CurrentMapData.HiddenObjectsData;
            
            // Register event from new map object data
            CurrentMapObjectData.OnObjDictSizeChanged = ReloadObjectsInArea;
            
            ReloadObjectsInArea();
        }

        /// <summary>
        /// This method instantiate a prefab of the current map data onto the scene, then scan through all object in each area of prefab and keep track of them
        /// </summary>
        public void ReloadObjectsInArea()
        {
            // Check if gameobject has any child, if yes destroy them
            UnityUtilities.ClearAllChildren(this.transform);
            
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

        private void CleanupData()
        {
            CurrentMapObjectData = null;
            AreaDatas.Clear();
        }

        private void OnInspectorInit()
        {
            // CurrentMapObjectData.OnObjDictSizeChanged = ReloadObjectsInArea;
        }
        
        #endif
    }
}

