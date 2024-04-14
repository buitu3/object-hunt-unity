using System.Collections;
using System.Collections.Generic;
using OjbectHunt.GamePlay;
using UnityEngine;

namespace OjbectHunt.UI
{
    public class LevelSelectGUI : MonoBehaviour
    {
        [SerializeField]
        private LevelSelectGUIItem LevelSelectGUIItemPrefab;
        [SerializeField]
        private Transform LevelSelectGUIItemContainer;

        void Start()
        {
            var mapLst = GameDataManager.Instance.MapDataCollection.MapLst;
            for (int i = 0; i < mapLst.Count; i++)
            {
                var newLevelSelectItem = Instantiate(LevelSelectGUIItemPrefab, LevelSelectGUIItemContainer);
                newLevelSelectItem.Show(mapLst[i]);
            }
        }
    }

}
