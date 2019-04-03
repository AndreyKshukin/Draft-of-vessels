using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropService
{
    private readonly List<string> _firstObjectPos = new List<string> { "Горизонтальное", "Вертикальное" };
    private readonly List<string> _cylindricalCylindrical = new List<string> { "Нормальное", "Вертикальное", "Горизонтальное", "Слева", "Справа"};
    private readonly List<string> _flangeShell = new List<string> {"Нет", "Слева и справа", "Слева", "Справа "};
    private readonly List<string> _flangeBottom = new List<string> { "Нет", "Есть"};

    internal List<string> Prop(TypeDropdown drop)
    {
        switch (drop)
        {
            case TypeDropdown.FlangeShell:
                return _flangeShell;
                break;

            case TypeDropdown.FlangeBottom:
                return _flangeBottom;
                break;

            default:
                return null;
                break;
        }
    }

    internal List<string> Pos(TypeDropdown drop)
    {
        switch (drop)
        {
            case TypeDropdown.FirstObject:
                return _firstObjectPos;
                break;

            case TypeDropdown.CylindricalCylindrical:
                return _cylindricalCylindrical;
                break;

            default:
                return null;
                break;
        }
    }
}
