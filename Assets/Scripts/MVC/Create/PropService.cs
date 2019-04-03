using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropService
{
    internal List<DovProp> CylindricalShell()
    {
        var result = new List<DovProp>
        {
            new DovProp() { NameField = "Title", TypeField = TypeField.String, Description = "Название объекта:", Value = "Обечайка цилинидрическая" },
            new DovProp() { NameField = "Length", TypeField = TypeField.Float, Description = "Длина (мм):", Value = "6000"},
            new DovProp() { NameField = "Diameter", TypeField = TypeField.Float, Description = "Внутренний диаметр (мм):", Value = "2000"},
            new DovProp() { NameField = "Thickness", TypeField = TypeField.Float, Description = "Исполнительная толщина (мм):", Value = "10" },
            new DovProp() { NameField = "Pressure", TypeField = TypeField.Float, Description = "Расчетное давление (МПа):", Value = "1.5" },
            new DovProp() { NameField = "Welding", TypeField = TypeField.Float, Description = "Коэффициент прочности сварных швов:", Value = "1.0" },
            new DovProp() { NameField = "Material", TypeField = TypeField.String, Description = "Материал:", Value = "сталь Ст3" },
            new DovProp() { NameField = "Temperature", TypeField = TypeField.Float, Description = "Расчетная температура (градусы):", Value = "20" },
            new DovProp() { NameField = "AllowableStress", TypeField = TypeField.Float, Description = "Допускаемые напряжения:", Value = "154" },
            new DovProp() { NameField = "Flanges", TypeField = TypeField.Dropdown, TypeDrop = TypeDropdown.FlangeShell, Description = "Наличие фланцев:", Value = "0" }
        };

        return result;
    }

    internal List<DovProp> ConicalShell()
    {
        var result = new List<DovProp>
        {
            new DovProp() { NameField = "Title", TypeField = TypeField.String, Description = "Название объекта.", Value = "Обечайка коническая" },
            new DovProp() { NameField = "Length", TypeField = TypeField.Float, Description = "Длина обечайки (мм):", Value = "6000" },
            new DovProp() { NameField = "DiameterMax", TypeField = TypeField.Float, Description = "D - внутренний диаметр сосуда или аппарата (мм):", Value = "2000" },
            new DovProp() { NameField = "DiameterMin", TypeField = TypeField.Float, Description = "D1 - диаметр меньшего основания сосуда или аппарата (мм):", Value = "2000" },
            new DovProp() { NameField = "Thickness", TypeField = TypeField.Float, Description = "tисп - исполнительная толщина (мм):", Value = "0" },
            new DovProp() { NameField = "Pressure", TypeField = TypeField.Float, Description = "p - сумма прибавок к расчетным толщинам стенок (МПа):", Value = "1.5" },
            new DovProp() { NameField = "Welding", TypeField = TypeField.Float, Description = "Fi - коэффициент прочности сварных швов.", Value = "0" },
            new DovProp() { NameField = "Material", TypeField = TypeField.String, Description = "Материал:", Value = "сталь Ст3" },
            new DovProp() { NameField = "Temperature", TypeField = TypeField.Float, Description = "Расчетная температура (градусы):", Value = "20" },
            new DovProp() { NameField = "AllowableStress", TypeField = TypeField.Float, Description = "R - допускаемые напряжения (МПа):", Value = "154" },
            new DovProp() { NameField = "Flanges", TypeField = TypeField.Dropdown, TypeDrop = TypeDropdown.FlangeShell, Description = "Наличие фланцев:", Value = "0" }
        };

        return result;
    }

    //internal List<DovProp> EllepticalBottom()
    //{
    //    var result = new List<DovProp>();

    //    result.Add(new DovProp() { NameField = "Title",          TypeField = TypeField.String, Description = "Название объекта.", Value = "Днище эллиптическое" });
    //    result.Add(new DovProp() { NameField = "Diameter",       TypeField = TypeField.Float,  Description = "D - внутренний диаметр сосуда или аппарата (мм):", Value = "2000" });
    //    result.Add(new DovProp() { NameField = "Thickness",      TypeField = TypeField.Float,  Description = "tисп - исполнительная толщина (мм):", Value = "0" });
    //    result.Add(new DovProp() { NameField = "Pressure",       TypeField = TypeField.Float,  Description = "p - сумма прибавок к расчетным толщинам стенок (МПа):", Value = "1.5" });
    //    result.Add(new DovProp() { NameField = "Welding",        TypeField = TypeField.Float,  Description = "Fi - коэффициент прочности сварных швов.", Value = "0" });
    //    result.Add(new DovProp() { NameField = "Material",       TypeField = TypeField.String, Description = "Материал:", Value = "сталь Ст3" });
    //    result.Add(new DovProp() { NameField = "Temperature",    TypeField = TypeField.Float,  Description = "Расчетная температура (градусы):", Value = "20" });
    //    result.Add(new DovProp() { NameField = "AllowableStress",TypeField = TypeField.Float,  Description = "R - допускаемые напряжения (МПа):", Value = "154" });
    //    result.Add(new DovProp() { NameField = "Flanges",        TypeField = TypeField.FlangeShell, Description = "Наличие фланцев (мм):", Value = "false" });

    //    return result;
    //}

    //internal List<DovProp> HimeSphericalBottom()
    //{
    //    var result = new List<DovProp>();

    //    result.Add(new DovProp() { NameField = "Title",          TypeField = TypeField.String, Description = "Название объекта.", Value = "Днище Полусферическое" });
    //    result.Add(new DovProp() { NameField = "Diameter",       TypeField = TypeField.Float,  Description = "D - внутренний диаметр сосуда или аппарата (мм):", Value = "2000" });
    //    result.Add(new DovProp() { NameField = "Thickness",      TypeField = TypeField.Float,  Description = "tисп - исполнительная толщина (мм):", Value = "0" });
    //    result.Add(new DovProp() { NameField = "Pressure",       TypeField = TypeField.Float,  Description = "p - сумма прибавок к расчетным толщинам стенок (МПа):", Value = "1.5" });
    //    result.Add(new DovProp() { NameField = "Welding",        TypeField = TypeField.Float,  Description = "Fi - коэффициент прочности сварных швов.", Value = "0" });
    //    result.Add(new DovProp() { NameField = "Material",       TypeField = TypeField.String, Description = "Материал:", Value = "сталь Ст3" });
    //    result.Add(new DovProp() { NameField = "Temperature",    TypeField = TypeField.Float,  Description = "Расчетная температура (градусы):", Value = "20" });
    //    result.Add(new DovProp() { NameField = "AllowableStress",TypeField = TypeField.Float,  Description = "R - допускаемые напряжения (МПа):", Value = "154" });
    //    result.Add(new DovProp() { NameField = "Flanges",        TypeField = TypeField.FlangeShell, Description = "Наличие фланцев (мм):", Value = "false" });

    //    return result;
    //}

    //internal List<DovProp> TorosphericBottom()
    //{
    //    var result = new List<DovProp>();

    //    result.Add(new DovProp() { NameField = "Title",          TypeField = TypeField.String, Description = "Название объекта.", Value = "Днище торосферическое" });
    //    result.Add(new DovProp() { NameField = "Diameter",       TypeField = TypeField.Float,  Description = "D - внутренний диаметр сосуда или аппарата (мм):", Value = "2000" });
    //    result.Add(new DovProp() { NameField = "Diameter",          TypeField = TypeField.Float,  Description = "H - Высота выпуклой части днища (мм):", Value = "2000" });
    //    result.Add(new DovProp() { NameField = "Thickness",      TypeField = TypeField.Float,  Description = "tисп - исполнительная толщина (мм):", Value = "0" });
    //    result.Add(new DovProp() { NameField = "Pressure",       TypeField = TypeField.Float,  Description = "p - сумма прибавок к расчетным толщинам стенок (МПа):", Value = "1.5" });
    //    result.Add(new DovProp() { NameField = "Welding",        TypeField = TypeField.Float,  Description = "Fi - коэффициент прочности сварных швов.", Value = "0" });
    //    result.Add(new DovProp() { NameField = "Material",       TypeField = TypeField.String, Description = "Материал:", Value = "сталь Ст3" });
    //    result.Add(new DovProp() { NameField = "Temperature",    TypeField = TypeField.Float,  Description = "Расчетная температура (градусы):", Value = "20" });
    //    result.Add(new DovProp() { NameField = "AllowableStress",TypeField = TypeField.Float,  Description = "R - допускаемые напряжения (МПа):", Value = "154" });
    //    result.Add(new DovProp() { NameField = "Flanges",        TypeField = TypeField.FlangeShell, Description = "Наличие фланцев (мм):", Value = "false" });

    //    return result;
    //}

    //internal List<DovProp> FlatBottom()
    //{
    //    var result = new List<DovProp>();

    //    result.Add(new DovProp() { NameField = "Title",          TypeField = TypeField.String, Description = "Название объекта.", Value = "Днище плоское" });
    //    result.Add(new DovProp() { NameField = "Diameter",       TypeField = TypeField.Float,  Description = "D - внутренний диаметр сосуда или аппарата (мм):", Value = "2000" });
    //    result.Add(new DovProp() { NameField = "Thickness",      TypeField = TypeField.Float,  Description = "tисп - исполнительная толщина (мм):", Value = "0" });
    //    result.Add(new DovProp() { NameField = "Pressure",       TypeField = TypeField.Float,  Description = "p - сумма прибавок к расчетным толщинам стенок (МПа):", Value = "1.5" });
    //    result.Add(new DovProp() { NameField = "Welding",        TypeField = TypeField.Float,  Description = "Fi - коэффициент прочности сварных швов.", Value = "0" });
    //    result.Add(new DovProp() { NameField = "StiffnessCoeff", TypeField = TypeField.Float,  Description = "K - коэффициент жесткости.", Value = "0,45" });
    //    result.Add(new DovProp() { NameField = "Material",       TypeField = TypeField.String, Description = "Материал:", Value = "сталь Ст3" });
    //    result.Add(new DovProp() { NameField = "Temperature",    TypeField = TypeField.Float,  Description = "Расчетная температура (градусы):", Value = "20" });
    //    result.Add(new DovProp() { NameField = "AllowableStress",TypeField = TypeField.Float,  Description = "R - допускаемые напряжения (МПа):", Value = "154" });

    //    return result;
    //}

    internal DovModel GetNewDovElement(Guid guid)
    {
        throw new NotImplementedException();
    }

}