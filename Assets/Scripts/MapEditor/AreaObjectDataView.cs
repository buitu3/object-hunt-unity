using System.Collections;
using System.Collections.Generic;
using OjbectHunt.Map;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace OjbectHunt.Editor
{
    [System.Serializable]
    public class AreaObjectDataView
    {
        #if UNITY_EDITOR
        
        [ReadOnly]
        [TableColumnWidth(60, Resizable = false)]
        public int ObjectID;
        
        [ReadOnly]
        [TableColumnWidth(60, Resizable = false)]
        [PreviewField(Alignment = ObjectFieldAlignment.Center)] public Sprite Preview;
        [ReadOnly]
        [TableColumnWidth(60, Resizable = false)]
        public int Count;
        
        [OnCollectionChanged("BeforeListObjChanged", "AfterListObjChanged")]
        public List<GameObject> ObjsInMap;

        [HideInInspector] public MapArea RepresentArea;
        [HideInInspector] public GameObject RepresentObj;

        [TableColumnWidth(60, Resizable = false)]
        [Button, GUIColor(0, 1, 0)]
        private void Add()
        {
            if(RepresentObj == null || RepresentArea == null) return; 
            var newObj = PrefabUtility.InstantiatePrefab(RepresentObj, RepresentArea.HiddenObjectContainer) as GameObject;
            newObj.transform.position = RepresentArea.transform.position;
            
            // Update info show on editor
            Count++;
            ObjsInMap.Add(newObj);
            
            if(RepresentArea.ObjectDict.ContainsKey(ObjectID)) RepresentArea.ObjectDict[ObjectID].Add(newObj);
            else RepresentArea.ObjectDict.Add(ObjectID, new SerializableList<GameObject>{newObj});
        }

        private void BeforeListObjChanged(CollectionChangeInfo info, object value)
        {
            if (info.ChangeType == CollectionChangeType.RemoveIndex)
            {
                var removeIndex = info.Index;
                var destroyObj = ObjsInMap[removeIndex];
                
                ObjsInMap.RemoveAt(removeIndex);
                Count--;
                
                if(RepresentArea.ObjectDict.ContainsKey(ObjectID)) RepresentArea.ObjectDict[ObjectID].Remove(destroyObj);
                
                GameObject.DestroyImmediate(destroyObj);
            }
        }

        private void AfterListObjChanged(CollectionChangeInfo info, object value)
        {
            
        }
        
        #endif
    }
}