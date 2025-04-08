using System;
using System.Collections.Generic;
using Core.Scripts.Units.Components;
using Core.Scripts.Units.Models;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    [SerializeField] private UnitContext _unitContext;
    
    [Space]
    [SerializeReference] public List<UnitComponentBase> Components = new();

    private void Awake()
    {
        foreach (var component in Components)
        {
            component.Initialize(_unitContext);
        }
    }

    private void Start()
    {
        MonoContext.Instance.CameraProvider.SetObjectToFollow(_unitContext.UnitT);
    }

    private void Update()
    {
        Components.ForEach(component => component.Execute());
    }
}