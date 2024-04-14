using System.Collections;
using System.Collections.Generic;
using OjbectHunt.Data;
using UnityEngine;

namespace OjbectHunt.GamePlay
{
    public class GameDataManager : Singleton<GameDataManager>
    {
        public MapsCollectionSO MapDataCollection;

        [HideInInspector]
        public MapDataSO SelectedMap;
    }

}
