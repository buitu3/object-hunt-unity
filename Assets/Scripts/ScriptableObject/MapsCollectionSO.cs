using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OjbectHunt.Data
{
    [CreateAssetMenu(fileName = "MapCollection", menuName = "ScriptableObjects/CreateMapCollectionSO", order = 1)]
    public class MapsCollectionSO : ScriptableObject
    {
        public SerializableList<MapDataSO> MapLst;
    }
}
