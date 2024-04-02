using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace DMCoin.Ultils
{
    /**
	* - description:
	* this is a base class for using Observer Pattern, receive and forward information.
	*/
    
    public static class EventDispatcher
    {
        
        private static Dictionary<string, Action<Dictionary<string, object>>> listeners = new Dictionary<string, Action<Dictionary<string, object>>>();

        public static void RegisterListener(string eventID, Action<Dictionary<string, object>> callback)
        {
            if (listeners.ContainsKey(eventID))
            {
                listeners[eventID] += callback;
            }
            else
            {
                listeners.Add(eventID, null);
                listeners[eventID] += callback;
            }
        }
        public static void PostEvent(string eventID, Dictionary<string, object> param = null)
        {
            if (!listeners.ContainsKey(eventID))
                return;
            var callback = listeners[eventID];
            if (callback != null)
                callback(param);
            else
                listeners.Remove(eventID);
        }
        public static void RemoveListener(string eventID, Action<Dictionary<string, object>> callback)
        {
            if (listeners.ContainsKey(eventID))
                listeners[eventID] -= callback;
        }

        public static void ClearListener(string eventID)
        {
            if (listeners.ContainsKey(eventID))
                listeners.Remove(eventID);
        }

        public static void ClearAllListener()
        {
            listeners.Clear();
        }
    }
}
