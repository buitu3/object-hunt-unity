using System.Collections;
using System.Collections.Generic;
using OjbectHunt.GamePlay;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OjbectHunt.UI
{
    public class LevelSelectGUIItem : MonoBehaviour
    {
        [SerializeField] private Image MapPreview;
        [SerializeField] private TextMeshProUGUI MapNameText;

        private MapDataSO RepresentMap;

        public void Show(MapDataSO mapData)
        {
            if (mapData == null) return;
            RepresentMap = mapData;
            
            if (MapPreview) MapPreview.sprite = mapData.MapPreview;
            if (MapNameText) MapNameText.text = mapData.MapName;
        }

        public void OnItemSelectedHandler()
        {
            GameDataManager.Instance.SelectedMap = RepresentMap;
            SceneManager.LoadScene("GameScene");
        }
    }

}
