using System.Collections;
using System.Collections.Generic;
using DMCoin.Ultils;
using OjbectHunt.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OjbectHunt.Map
{
    public class HiddenObject : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private int ObjectID;
        
        private int IndexInArea;
        private bool IsFound = false;
        
        private SpriteRenderer ObjSprite;

        public void Setup(int ObjectID, int IndexInArea)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.LogError("Object clicked");
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
