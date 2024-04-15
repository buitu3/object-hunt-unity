using System;
using System.Collections;
using System.Collections.Generic;
using DMCoin.Ultils;
using OjbectHunt.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OjbectHunt.Map
{
    [Serializable]
    public class HiddenObject : MonoBehaviour, IPointerClickHandler
    {
        public int ObjectID;
        
        /// <summary>
        /// The Index of the object in the list of Area dictionary
        /// </summary>
        [SerializeField]
        private int ObjectIndex;
        
        private bool IsFound = false;
        
        private SpriteRenderer ObjSprite;

        public void Setup(int ObjectID, int IndexInArea)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnObjectClickedHandler();
        }

        private void OnObjectClickedHandler()
        {
            IsFound = true;
            gameObject.SetActive(false);
            
            var message = new Dictionary<string, object>();
            var objectData = new HiddenObjectClickedMessage
            {
                ObjectID = this.ObjectID
            };
            message.Add(Constant.EVENT_DATA_KEY, objectData);
            EventDispatcher.PostEvent(EventID.ON_HIDDEN_OBJECT_FOUND, message);
        }
    }

    public class HiddenObjectClickedMessage
    {
        public int ObjectID;
    }

}
