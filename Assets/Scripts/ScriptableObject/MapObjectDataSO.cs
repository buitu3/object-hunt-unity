using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MapObjectData", menuName = "ScriptableObjects/CreateMapObjectDataSO", order = 3)]
public class MapObjectDataSO : ScriptableObject
{
    public SerializableDictionary<int, HiddenObjectData> ObjectDict;

#if UNITY_EDITOR
    
    public Action OnObjDictSizeChanged;

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
            var objectId = 0;

            if (ObjectDict.Count > 0)
            {
                var lastItem = ObjectDict.Last();
                objectId = lastItem.Key + 1;
            }

            objectData.ObjectID = objectId;
            ObjectDict.Add(objectId, objectData);

            OdinGameObjectPicker.OnGameObjectPicked -= OnGameObjectPickedHandler;
            OdinGameObjectPicker.OnGameObjectCancel -= OnGameObjectPickedCancel;

            OdinGameObjectPicker.CloseWindow();
            
            // OnObjDictSizeChanged?.Invoke();
        }
    }

    public void OnGameObjectPickedCancel()
    {
        OdinGameObjectPicker.OnGameObjectPicked -= OnGameObjectPickedHandler;
        OdinGameObjectPicker.OnGameObjectCancel -= OnGameObjectPickedCancel;
    }

    public HiddenObjectData GetObjectPrefabById(int id)
    {
        if (ObjectDict.ContainsKey(id)) return ObjectDict[id];
        return null;
    }
    
    private void OnObjectDictSizeChangedHandler()
    {
        OnObjDictSizeChanged?.Invoke();
    }

#endif
}

[Serializable]
public class HiddenObjectData
{
    public int ObjectID;
    [PreviewField] public Sprite ObjectPreview;
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