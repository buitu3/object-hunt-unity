using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace OjbectHunt.Data
{
    [CreateAssetMenu(fileName = "MapCollection", menuName = "ScriptableObjects/CreateMapCollectionSO", order = 1)]
    public class MapsCollectionSO : ScriptableObject
    {
        [InlineEditor]
        public SerializableList<MapDataSO> MapLst;
    }
}
