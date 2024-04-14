using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OjbectHunt.Map
{
    public class MapArea : MonoBehaviour
    {
        public SerializableDictionary<int, SerializableList<GameObject>> ObjectDict;
        
        public Transform HiddenObjectContainer;

        private int AreaIndex;
        
    }
}