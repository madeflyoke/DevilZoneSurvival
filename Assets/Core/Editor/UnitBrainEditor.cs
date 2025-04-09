using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Scripts.Units.Components;
using Core.Scripts.Utils;
using Core.Units.UnitBrains;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnitBrain), true)]
public class UnitBrainEditor : Editor
{
    private class TypeNamePair
    {
        public string Name;
        public Type Type;
    }
    
    private List<TypeNamePair> availableComponentTypes;
    private SerializedProperty _componentsProperty;

    private void OnEnable()
    {
        _componentsProperty = serializedObject.FindProperty("Components");
        
        LoadAvailableComponentTypes();
    }

    private void LoadAvailableComponentTypes()
    {
        availableComponentTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(UnitComponentBase).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
            .Select(t =>
            {
                var attribute = (ComponentNameAttribute)t.GetCustomAttributes(typeof(ComponentNameAttribute)).First();

                return new TypeNamePair()
                {
                    Name = attribute.Name,
                    Type = t,
                };
            })
            .ToList();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        EditorGUILayout.Space();

        if (GUILayout.Button("Add Component"))
        {
            var menu = new GenericMenu();
            foreach (var type in availableComponentTypes)
            {
                menu.AddItem(new GUIContent(type.Name), false, () => AddComponentOfType(type.Type));
            }
            menu.ShowAsContext();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void AddComponentOfType(Type type)
    {
        var newComponent = (UnitComponentBase)Activator.CreateInstance(type);

        var playerBrain = (UnitBrain)target;
        playerBrain.Components.Add(newComponent);

        EditorUtility.SetDirty(target);
    }
}