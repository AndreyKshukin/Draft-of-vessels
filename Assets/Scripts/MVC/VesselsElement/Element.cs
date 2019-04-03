using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Модель элемента сосуда
/// </summary>

public class Element
{
    public Guid Uid { get; set; }
    public TypeModel Type { get; set; }
    public TypePos TypePos { get; set; }
    public GameObject Model_3D { get; set; }

    public List<DovProp> Props { get; set; }
    public List<PosProp> PosProps { get; set; }

    public Element Parent { get; set; }

    public delegate void EventHandler();

    public EventHandler DestroyEventHandler;
    public EventHandler PositionEventHandler;

}


public enum TypeModel
{
    CylidricalShell,
    ConicalShell
}

public enum TypePos
{
    FirstObject,
    CylindricalCylindrical
}