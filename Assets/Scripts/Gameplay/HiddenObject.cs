using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OjbectHunt.Map
{
    public class HiddenObject : MonoBehaviour, IPointerClickHandler
    {
        private SpriteRenderer ObjSprite;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.LogError("Object clicked");
            OnObjectClickedHandler();
        }

        private void OnObjectClickedHandler()
        {
            
        }
    }

}
