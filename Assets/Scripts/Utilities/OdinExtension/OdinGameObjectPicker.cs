using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class OdinGameObjectPicker : OdinEditorWindow
{
    [ShowInInspector, ReadOnly]
    private GameObject selectedGameObject;
    
    public static Action<GameObject> OnGameObjectPicked;
    public static Action OnGameObjectCancel;

    public static void OpenWindow()
    {
        GetWindow<OdinGameObjectPicker>().Show();
    }
    
    public static void CloseWindow()
    {
        GetWindow<OdinGameObjectPicker>().Close();
    }

    [Button("Select GameObject")]
    private void SelectGameObject()
    {
        EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, string.Empty, 0);
    }

    protected override void OnGUI()
    {
        base.OnImGUI();
        // Listen for object selector updates
        if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == 0)
        {
            selectedGameObject = EditorGUIUtility.GetObjectPickerObject() as GameObject;
            OnGameObjectPicked?.Invoke(selectedGameObject);
        }
        else if (Event.current.commandName == "ObjectSelectorClosed")
        {
            OnGameObjectCancel?.Invoke();
        }
        
    }
}