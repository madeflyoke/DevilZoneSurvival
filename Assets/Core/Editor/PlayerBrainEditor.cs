using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Scripts.Units.Components;

[CustomEditor(typeof(PlayerBrain))]
public class PlayerBrainEditor : Editor
{
    private List<Type> availableComponentTypes;

    private void OnEnable()
    {
        LoadAvailableComponentTypes();
    }

    private void LoadAvailableComponentTypes()
    {
        availableComponentTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(UnitComponentBase).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
            .ToList();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        DrawDefaultInspector();

        if (GUILayout.Button("Add Component"))
        {
            GenericMenu menu = new GenericMenu();
            foreach (var type in availableComponentTypes)
            {
                menu.AddItem(new GUIContent(type.Name), false, () => AddComponentOfType(type));
            }
            menu.ShowAsContext();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void AddComponentOfType(Type type)
    {
        var newComponent = (UnitComponentBase)Activator.CreateInstance(type);

        var playerBrain = (PlayerBrain)target;
        playerBrain.Components.Add(newComponent);

        EditorUtility.SetDirty(target);
    }
}