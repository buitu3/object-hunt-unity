using System.Collections;
using System.Collections.Generic;
using OjbectHunt.Map;
using Sirenix.OdinInspector;
using UnityEngine;

namespace OjbectHunt.Editor
{
    [System.Serializable]
    public class MapAreaDataView
    {
        #if UNITY_EDITOR
        
        [ReadOnly]
        [TableColumnWidth(150, Resizable = false)]
        public string AreaName;
        [TableList]
        public List<AreaObjectDataView> MapObjLst = new List<AreaObjectDataView>();
        
        [HideInInspector] public MapArea RepresentArea;
        
        public void LoadObjectInArea(MapArea area, MapObjectDataSO mapObjData)
        {
            MapObjLst.Clear();
            
            // load list of object can be used in map
            foreach (var obj in mapObjData.ObjectDict)
            {
                MapObjLst.Add(new AreaObjectDataView
                {
                    ObjectID = obj.Key,
                    Preview = obj.Value.ObjectPreview,
                    Count = 0,
                    ObjsInMap = new List<GameObject>(),
                    RepresentArea = RepresentArea,
                    RepresentObj = obj.Value.ObjectPrefab
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
                    objType.Count++;
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

    #endif
}