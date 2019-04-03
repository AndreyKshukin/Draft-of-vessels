using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosService : MonoBehaviour
{
    internal List<PosProp> FirstObject()
    {
        List<PosProp> result = new List<PosProp>();
        result.Add(new PosProp() { Title = "Выберите позицию:", TypeField = TypeField.Dropdown, TypeDropdown = TypeDropdown.FirstObject, Value = "0" });
        return result;
    }

    internal List<PosProp> CylinderCylinder(int valueDrop)
    {
        List<PosProp> result = new List<PosProp>();
        result.Add(new PosProp() { Title = "Выберите позицию:", TypeField = TypeField.Dropdown, TypeDropdown = TypeDropdown.CylindricalCylindrical, Value = Convert.ToString(valueDrop) });
        result.Add(new PosProp() { Title = "Координаты по Х:", TypeField = TypeField.Float, Value = "0" });
        result.Add(new PosProp() { Title = "Координаты по Y:", TypeField = TypeField.Float, Value = "0" });
        return result;
    }
}
