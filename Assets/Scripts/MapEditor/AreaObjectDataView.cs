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

        [ReadOnly]
        [TableColumnWidth(60, Resizable = false)]
        private void Add()
        {
            if(RepresentObj == null || RepresentArea == null) return; 
            var newObj = PrefabUtility.InstantiatePrefab(RepresentObj, RepresentArea.HiddenObjectContainer) as GameObject;
            
            // Update info show on editor
            Count++;
            ObjsInMap.Add(newObj);
        }

        private void BeforeListObjChanged(CollectionChangeInfo info, object value)
        {
            if (info.ChangeType == CollectionChangeType.RemoveIndex)
            {
                var removeIndex = info.Index;
                GameObject.DestroyImmediate(ObjsInMap[removeIndex]);
                
                // var path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(MigrateMapTemplate.gameObject);
                // PrefabUtility.UnpackPrefabInstance(MigrateMapTemplate.gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
                //GameObject.DestroyImmediate(MigrateMapTemplate.WestPortal.gameObject);
                // PrefabUtility.SaveAsPrefabAsset(MigrateMapTemplate.gameObject, path, out result);
            }
        }

        private void AfterListObjChanged(CollectionChangeInfo info, object value)
        {
            
        }
    }
}