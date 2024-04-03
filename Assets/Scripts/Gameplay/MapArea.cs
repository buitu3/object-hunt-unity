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

        private int AreaIndex;
        private Dictionary<int, HiddenObject> ObjectDict;

        private void Awake()
        {
            ObjectDict = new Dictionary<int, HiddenObject>();
            
            
        }
    }
}