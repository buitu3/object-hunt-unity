using System.Collections;
using System.Collections.Generic;
using OjbectHunt.Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OjbectHunt.UI
{
    public class ObjectCounterGUIItem : MonoBehaviour
    {
        [SerializeField] private Image ObjectIcon;
        [SerializeField] private TextMeshProUGUI ObjectCountText;

        private int MaxCount;

        public void SetupView(HiddenObjectData obj, int maxCount, int foundCount)
        {
            if (ObjectIcon) ObjectIcon.sprite = obj.ObjectPreview;
            
            MaxCount = maxCount;
            UpdateCounter(foundCount);
        }

        public void UpdateCounter(int foundCount)
        {
            if (ObjectCountText) ObjectCountText.text = foundCount + "/" + MaxCount;
        }
    }
}

