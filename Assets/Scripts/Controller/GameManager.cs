using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DMCoin.Ultils;
using OjbectHunt.Common;
using OjbectHunt.Map;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace OjbectHunt.GamePlay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField]
        private LevelMap CurrentLevel;
        
        private Dictionary<int, int> TotalObjectDict = new Dictionary<int, int>();
        private Dictionary<int, int> FoundObjectDict = new Dictionary<int, int>();

        public GameManager()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            EventDispatcher.RegisterListener(EventID.ON_HIDDEN_OBJECT_FOUND, OnHiddenObjectFoundHandler);
            
            // Get the total object count
            TotalObjectDict = new Dictionary<int, int>();
            if (CurrentLevel != null)
            {
                foreach (var area in CurrentLevel.AreaLst)
                {
                    var areaObjDict = area.ObjectDict;
                    foreach (var obj in areaObjDict)
                    {
                        var objKey = obj.Key;
                        if (TotalObjectDict.ContainsKey(objKey))
                        {
                            TotalObjectDict[objKey] += obj.Value.Count;
                        }
                    }
                }
            }
        }

        private void OnDestroy()
        {
            EventDispatcher.RemoveListener(EventID.ON_HIDDEN_OBJECT_FOUND, OnHiddenObjectFoundHandler);
        }

        public List<int> GetListObject()
        {
            return TotalObjectDict.Keys.ToList();
        }

        /// <summary>
        /// Get the original amount of specific object in current level
        /// </summary>
        /// <param name="ObjectID"></param>
        public int GetOriginalObjectCount(int ObjectID)
        {
            if (!TotalObjectDict.ContainsKey(ObjectID)) return 0;
            else return TotalObjectDict[ObjectID];
        }
        
        /// <summary>
        /// Get the found amount of specific object in current level
        /// </summary>
        /// <param name="ObjectID"></param>
        /// <returns></returns>
        public int GetFoundObjectCount(int ObjectID)
        {
            if (!FoundObjectDict.ContainsKey(ObjectID)) return 0;
            else return FoundObjectDict[ObjectID];
        }

        #region Events Handler

        private void OnHiddenObjectFoundHandler(Dictionary<string, object> message)
        {
            var data = message[Constant.EVENT_DATA_KEY];
            if (data is HiddenObjectClickedMessage)
            {
                var hiddenObjectData = data as HiddenObjectClickedMessage;
                Debug.LogError("found object: " + hiddenObjectData.ObjectID);

                if (!FoundObjectDict.ContainsKey(hiddenObjectData.ObjectID)) FoundObjectDict.Add(hiddenObjectData.ObjectID, 1);
                else FoundObjectDict[hiddenObjectData.ObjectID] += 1;
            }
        }

        #endregion
    }

}
