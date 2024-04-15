using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DMCoin.Ultils;
using OjbectHunt.Common;
using OjbectHunt.Map;
using OjbectHunt.UI;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OjbectHunt.GamePlay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [HideInInspector]
        public LevelMap CurrentLevel;
        private MapObjectDataSO currentMapData;
        
        private Dictionary<int, int> TotalObjectDict = new Dictionary<int, int>();
        private Dictionary<int, int> FoundObjectDict = new Dictionary<int, int>();

        public GameManager()
        {
            Instance = this;
        }

        private void Awake()
        {
            var selectedMap = GameDataManager.Instance.SelectedMap;
            if (selectedMap == null) return;

            CurrentLevel = Instantiate(selectedMap.MapPrefab).GetComponent<LevelMap>();
            currentMapData = selectedMap.HiddenObjectsData;
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
                        if (TotalObjectDict.ContainsKey(objKey)) TotalObjectDict[objKey] += obj.Value.Count;
                        else TotalObjectDict.Add(objKey, obj.Value.Count); 
                    }
                }
            }
        }

        private void OnDestroy()
        {
            EventDispatcher.RemoveListener(EventID.ON_HIDDEN_OBJECT_FOUND, OnHiddenObjectFoundHandler);
        }

        public HiddenObjectData GetObjectData(int objId)
        {
            return currentMapData.GetObjectPrefabById(objId);
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

        public void ExitToLevelSelect()
        {
            SceneManager.LoadScene("LevelSelectScene");
        }

        #region Events Handler

        private void OnHiddenObjectFoundHandler(Dictionary<string, object> message)
        {
            var data = message[Constant.EVENT_DATA_KEY];
            if (data is HiddenObjectClickedMessage)
            {
                var hiddenObjectData = data as HiddenObjectClickedMessage;

                if (!FoundObjectDict.ContainsKey(hiddenObjectData.ObjectID)) FoundObjectDict.Add(hiddenObjectData.ObjectID, 1);
                else FoundObjectDict[hiddenObjectData.ObjectID] += 1;
                
                // Update UI counter
                LevelObjectCounterUI.Instance.UpdateObjectCount(hiddenObjectData.ObjectID,FoundObjectDict[hiddenObjectData.ObjectID]);
            }
        }

        #endregion
    }

}
