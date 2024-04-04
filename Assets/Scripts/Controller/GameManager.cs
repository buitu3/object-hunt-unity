using System;
using System.Collections;
using System.Collections.Generic;
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

        public GameManager()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            EventDispatcher.RegisterListener(EventID.ON_HIDDEN_OBJECT_FOUND, OnHiddenObjectFoundHandler);
        }

        private void OnDestroy()
        {
            EventDispatcher.RemoveListener(EventID.ON_HIDDEN_OBJECT_FOUND, OnHiddenObjectFoundHandler);
        }

        private void OnHiddenObjectFoundHandler(Dictionary<string, object> message)
        {
            var data = message[Constant.EVENT_DATA_KEY];
            if (data is HiddenObjectClickedMessage)
            {
                var hiddenObjectData = data as HiddenObjectClickedMessage;
                Debug.LogError("found object: " + hiddenObjectData.ObjectID);
            }
        }
    }

}
