using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OjbectHunt.Map
{
    public class MapArea : MonoBehaviour
    {
        public SerializableDictionary<int, SerializableList<HiddenObject>> ObjectDict;
        
        [SerializeField] private Transform HiddenObjectContainer;

        private int AreaIndex;
        
    }
}