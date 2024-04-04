using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OjbectHunt.Map
{
    public class MapArea : MonoBehaviour
    {
        [SerializeField]
        private Transform HiddenObjectContainer;
        
        [SerializeField]
        private SerializableDictionary<int, List<HiddenObject>> ObjectDict;

        private int AreaIndex;
        
    }
}