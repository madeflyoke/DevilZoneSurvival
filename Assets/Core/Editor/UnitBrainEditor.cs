using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Core.Units.Components;
using Core.Units.Components.Base;
using Core.Units.UnitBrains;
using Core.Utils;
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

    private List<TypeNamePair> _availableComponentTypes;
    private UnitBrain _unitBrain;

    private void OnEnable()
    {
        _unitBrain = (UnitBrain)target;

        LoadAvailableComponentTypes();
    }

    private void LoadAvailableComponentTypes()
    {
        _availableComponentTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(UnitComponentBase).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
            .Select(t =>
            {
                ComponentNameAttribute attribute;
                try
                {
                    attribute =
                        (ComponentNameAttribute)t.GetCustomAttributes(typeof(ComponentNameAttribute)).First();
                }
                catch
                {
                    throw new Exception($"No component name attribute found in type {t}");
                }

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
            foreach (var type in _availableComponentTypes.Where(t => _unitBrain.Components.All(c => c.GetType() != t.Type)))
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
        newComponent.EditorName = FormatName(newComponent.GetType().Name);

        var playerBrain = (UnitBrain)target;
        playerBrain.Components.Add(newComponent);

        EditorUtility.SetDirty(target);
    }

    private string FormatName(string input)
    {
        input = Regex.Replace(input, "Component$", "");
        return Regex.Replace(input, "([a-z])([A-Z])", "$1 $2").Trim();
    }
}