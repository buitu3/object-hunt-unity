using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "MapObjectData", menuName = "ScriptableObjects/CreateMapObjectDataSO", order = 1)]
public class MapObjectDataSO : ScriptableObject
{
    public SerializableDictionary<int, GameObject> ObjectDict;

    [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
    private void AddNewItem()
    {
        if (ObjectDict != null)
        {
            var lastItem = ObjectDict.Last();
            ObjectDict.Add(lastItem.Key + 1, null);
        }
    }
}
