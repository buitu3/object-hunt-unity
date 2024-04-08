using System;
using System.Collections;
using System.Collections.Generic;
using OjbectHunt.GamePlay;
using UnityEngine;
using UnityEngine.Serialization;

namespace OjbectHunt.UI
{
    public class LevelObjectCounterUI : MonoBehaviour
    {
        public static LevelObjectCounterUI Instance;

        [SerializeField] private Transform ObjCountItemContainer;
        [SerializeField] private ObjectCounterGUIItem ObjCountItemPrefab;

        private Dictionary<int, ObjectCounterGUIItem> ObjCountItemDict =
            new Dictionary<int, ObjectCounterGUIItem>();
        
        public LevelObjectCounterUI()
        {
            Instance = this;
        }
        
        private void Start()
        {
            var objectIdLst = GameManager.Instance.GetListObject();
            foreach (var objId in objectIdLst)
            {
                var obj = GameManager.Instance.GetOriginalObjectCount(objId);
                var objMaxCount = GameManager.Instance.GetOriginalObjectCount(objId);
                var objFoundCount = GameManager.Instance.GetFoundObjectCount(objId);

                ObjectCounterGUIItem counterItem = Instantiate(ObjCountItemPrefab, ObjCountItemContainer);
                counterItem.SetupView(GameManager.Instance.GetObjectData(objId), objMaxCount, objFoundCount);
                ObjCountItemDict.Add(objId, counterItem);
            }
        }

        public void UpdateObjectCount(int objId,int foundCount)
        {
            if (GetCounterGUIItemById(objId) != null) GetCounterGUIItemById(objId).UpdateCounter(foundCount);
        }

        private ObjectCounterGUIItem GetCounterGUIItemById(int objId)
        {
            if (ObjCountItemDict.ContainsKey(objId)) return ObjCountItemDict[objId];
            else return null;
        }
    }
}

