using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DovProp : MonoBehaviour
{
    public string NameField { get; set; }
    public TypeField TypeField { get; set; }
    //public TypeGeomery TypeGeometry { get; set; }
    public TypeDropdown TypeDrop { get; set; }
    public string Value { get; set; }
    public string Description { get; set; }
}

public enum TypeField
{
    String,
    Float,
    Dropdown,
}

public enum TypeDropdown
{
    Material,
    FlangeShell,
    FlangeBottom,
    //позиция
    FirstObject,
    CylindricalCylindrical
}
