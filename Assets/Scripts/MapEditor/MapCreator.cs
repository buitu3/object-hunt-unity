using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace OjbectHunt.Editor
{
    public class MapCreator : MonoBehaviour
    {
        #if UNITY_EDITOR
        [OnValueChanged("LoadMapObjInfo")]
        public MapObjectDataSO MapObjLst;
        
        public MapInfo Info;

        private void LoadMapObjInfo()
        {
            if(MapObjLst == null) return;
            Info = new MapInfo();
            foreach (var obj in MapObjLst.ObjectDict.Values)
            {
                var objSprite = obj.ObjectPreview;

                var newObj = new HiddenObjInMap();
                newObj.ObjectSprite = objSprite;
                Info.ObjInMap.Add(newObj);
            }
        }
        
        #endif
    }

    [Serializable]
    public class MapInfo
    {
        public SerializableList<HiddenObjInMap> ObjInMap = new SerializableList<HiddenObjInMap>();
    }

    [Serializable]
    public struct HiddenObjInMap
    {
        [PreviewField] public Sprite ObjectSprite;
    }
}