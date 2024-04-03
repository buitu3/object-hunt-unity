using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MapObjectData", menuName = "ScriptableObjects/CreateMapObjectDataSO", order = 1)]
public class MapObjectDataSO : ScriptableObject
{
    public SerializableDictionary<int, HiddenObjectData> ObjectDict;

    [Button(ButtonSizes.Large), GUIColor(0, 1, 0)]
    public void AddNewItem()
    {
        OdinGameObjectPicker.OnGameObjectPicked += OnGameObjectPickedHandler;
        OdinGameObjectPicker.OnGameObjectCancel += OnGameObjectPickedCancel;
        OdinGameObjectPicker.OpenWindow();
    }

    public void OnGameObjectPickedHandler(GameObject gameObject)
    {
        if (ObjectDict != null)
        {
            var objectData = new HiddenObjectData(gameObject);
            
            if (ObjectDict.Count > 0)
            {
                var lastItem = ObjectDict.Last();
                ObjectDict.Add(lastItem.Key + 1, objectData);
            }
            else ObjectDict.Add(0, objectData);
            
            OdinGameObjectPicker.OnGameObjectPicked -= OnGameObjectPickedHandler;
            OdinGameObjectPicker.OnGameObjectCancel -= OnGameObjectPickedCancel;
            
            OdinGameObjectPicker.CloseWindow();
        }
    }

    public void OnGameObjectPickedCancel()
    {
        OdinGameObjectPicker.OnGameObjectPicked -= OnGameObjectPickedHandler;
        OdinGameObjectPicker.OnGameObjectCancel -= OnGameObjectPickedCancel;
    }
}

public struct HiddenObjectData
{
    [PreviewField]
    public Sprite ObjectPreview;
    public GameObject ObjectPrefab;

    public HiddenObjectData(GameObject prefab)
    {
        ObjectPrefab = prefab;
        
        var sprite = prefab.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            ObjectPreview = sprite.sprite;
        }
        else ObjectPreview = null;
    }
}

