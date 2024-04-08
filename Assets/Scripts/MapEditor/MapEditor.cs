using System.Collections;
using System.Collections.Generic;
using OjbectHunt.Map;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace OjbectHunt.Editor
{
    public class MapEditor : MonoBehaviour
    {
        [OnValueChanged("LoadMapData")]
        public MapDataSO CurrentMapData;

        [TableList]
        public List<MapAreaDataView> AreaDatas = new List<MapAreaDataView>();
        
        private void LoadMapData()
        {
            if(CurrentMapData == null) return;
            var MapAreas = CurrentMapData.MapPrefab.GetComponent<LevelMap>();
            
            AreaDatas.Clear();  
            foreach (var area in MapAreas.AreaLst)
            {
                var currentArea = new MapAreaDataView();
                // currentArea.LoadObjectInArea(area);

                currentArea.AreaName = area.gameObject.name;
                AreaDatas.Add(currentArea);
            } 
        }
        
    }

    [System.Serializable]
    public class MapAreaDataView
    {
        [ReadOnly]
        [TableColumnWidth(150, Resizable = false)]
        public string AreaName;
        [TableList]
        public List<AreaObjectDataView> MapObjLst = new List<AreaObjectDataView>();
        
        public void LoadObjectInArea(MapArea area, MapObjectDataSO mapObjData)
        {
            MapObjLst.Clear();
            
            // load list of object can be used in map
            foreach (var obj in mapObjData.ObjectDict.Values)
            {
                MapObjLst.Add(new AreaObjectDataView
                {
                    ObjectID = obj.ObjectID,
                    ObjectPreview = obj.ObjectPreview,
                    ObjectCount = 0,
                    ObjsInMap = new List<GameObject>()
                });
            }
            
            // Iterate through all hidden object in map prefab and count into dict
            for (int i = 0; i < area.HiddenObjectContainer.childCount; i++)
            {
                var obj = area.HiddenObjectContainer.GetChild(i).GetComponent<HiddenObject>();
                if(obj == null) continue;

                var objType = GetObjectTypeById(obj.ObjectID);
                if (objType != null)
                {
                    objType.ObjectCount++;
                    objType.ObjsInMap.Add(obj.gameObject);
                }
                else
                {
                    Debug.LogError("object with id " + obj.ObjectID + " does not exist in map data");
                }
            }
        }
        
        private AreaObjectDataView GetObjectTypeById(int id)
        {
            for (int i = 0; i < MapObjLst.Count; i++)
            {
                if (MapObjLst[i].ObjectID == id) return MapObjLst[i];
            }
            return null;
        }
    }

    [System.Serializable]
    public class AreaObjectDataView
    {
        [ReadOnly]
        [TableColumnWidth(60, Resizable = false)]
        public int ObjectID;
        [ReadOnly]
        [TableColumnWidth(60, Resizable = false)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)] public Sprite ObjectPreview;
        [ReadOnly]
        [TableColumnWidth(60, Resizable = false)]
        public int ObjectCount;
        
        public List<GameObject> ObjsInMap;

        [ReadOnly]
        [TableColumnWidth(60, Resizable = false)]
        [Button]
        private void Add()
        {
            
        }
    }
}

