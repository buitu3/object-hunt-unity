using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/CreateMapDataSO", order = 2)]
[System.Serializable]
public class MapDataSO : ScriptableObject
{
    public string MapName;
    public Texture2D MapPreview;
    public GameObject MapPrefab;
    public MapObjectDataSO HiddenObjectsData;
}


