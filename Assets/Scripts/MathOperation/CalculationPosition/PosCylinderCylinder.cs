using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCylinderCylinder : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos">Позция в DropDown</param>
    /// <param name="x">x координата</param>
    /// <param name="y">y координата</param>
    /// <param name="model">Модель объекта</param>
    /// <param name="parent">Модель родителя объекта</param>
    public void Position(string pos, float x, float y, GameObject model, GameObject parent)
    {
        Transform trans = model.transform;
        switch (pos)
        {
            case "0":
                trans = NormalPosition(x, y, parent.transform.localScale.x,
                    parent.transform.localScale.z, model.transform);
                break;

            case "1":
                trans = VerticalPosition(x, y, parent.transform.localScale.x,
                    parent.transform.localScale.z, model.transform);
                break;

            case "2":
                trans = HorizontalPosition(x, y, parent.transform.localScale.x,
                    parent.transform.localScale.z, model.transform);
                break;

            case "3":

                break;
        }

        if (parent.transform.localEulerAngles != Vector3.zero)
        {
            Matrix3x3 matrix = new Matrix3x3(trans.position, parent.transform.localEulerAngles);
            trans.position = matrix.Vector();
            //matrix = new Matrix3x3(parent.transform.localEulerAngles, parent.transform.localEulerAngles);
            //trans.localEulerAngles = trans.localEulerAngles - matrix.Vector();
        }

        model.transform.position = trans.position + parent.transform.position;
        model.transform.localEulerAngles = trans.localEulerAngles;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">х - координата относительно длины цилиндра (мм)</param>
    /// <param name="y">у - коорданата относительно диаметра цилиндра (мм)</param>
    /// <param name="length">Длина цилиндра родителя (мм)</param>
    /// <param name="diameter">Диаметер цилиндра родителя (мм)</param>
    /// <param name="trans">Трансформ объекта</param>
    private Transform NormalPosition(float x, float y, float length, float diameter, Transform trans)
    {
        float R = diameter / 2;
        Vector3 angle = Vector3.zero;
        Vector3 pos = Vector3.zero;
        Transform newTrans = trans;

        // Проверка, не выходит ли объект за границу,
        int i = (R >= trans.localScale.z) ? 1 : 0;
        float lim = i * 0.95f * Mathf.Sqrt(Mathf.Abs(R * R - trans.localScale.z / 2 * trans.localScale.z / 2)); // минимальная высота чтобы не было видно границ объекта

        x = (x > length) ? length : (x < 0) ? 0 : x;
        x = x - length;

        y = y - R;
        y = (y > lim) ? lim : (y < -lim) ? -lim: y;

        // Расчет угла поворота
        angle.x = (lim <= 0) ? 0 : -Mathf.Acos(y / lim) * 180f / Mathf.PI;

        // расчет позиции
        pos.x = x;
        pos.y = y;
        pos.z = lim * Mathf.Sin(angle.x * Mathf.PI / 180);

        //// Перевод в глобальные координаты
        pos = pos / 1000;
        angle = angle + new Vector3(0, 0, -90);
        newTrans.position = pos;
        newTrans.localEulerAngles = angle;
        return newTrans;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">х - координата относительно длины цилиндра (мм)</param>
    /// <param name="y">у - коорданата относительно диаметра цилиндра (мм)</param>
    /// <param name="length">Длина цилиндра родителя (мм)</param>
    /// <param name="diameter">Диаметер цилиндра родителя (мм)</param>
    /// <param name="trans">Трансформ объекта</param>
    private Transform VerticalPosition(float x, float y, float length, float diameter, Transform trans)
    {
        float R = diameter / 2;
        Vector3 angle;
        Vector3 pos = Vector3.zero;
        Transform newTrans = trans;

        // Проверка, не выходит ли объект за границу,
        int i = (R >= trans.localScale.z) ? 1 : 0;
        float lim = i * 0.95f * Mathf.Sqrt(R * R - trans.localScale.z / 2 * trans.localScale.z / 2); // минимальная высота чтобы не было видно границ объекта

        x = (x > length) ? length : (x < 0) ? 0 : x;
        x = x - length;

        y = y - R;
        y = (y > lim) ? lim : (y < -lim) ? -lim : y;

        // Расчет угла поворота
        angle = (y < 0) ? new Vector3(0, 0, 90) : new Vector3(0, 0, -90);

        // расчет позиции
        pos.x = x;
        pos.y = y;
        pos.z = (lim != 0) ? -trans.localScale.z * Mathf.Sqrt(1 - Mathf.Pow((y / lim), 2)) * 0.475f : 0;

        // Перевод в глобальные координаты
        pos = pos / 1000;
        newTrans.position = pos;
        newTrans.eulerAngles = angle;
        return newTrans;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">х - координата относительно длины цилиндра (мм)</param>
    /// <param name="y">у - коорданата относительно диаметра цилиндра (мм)</param>
    /// <param name="length">Длина цилиндра родителя (мм)</param>
    /// <param name="diameter">Диаметер цилиндра родителя (мм)</param>
    /// <param name="trans">Трансформ объекта</param>
    private Transform HorizontalPosition(float x, float y, float length, float diameter, Transform trans)
    {
        float R = diameter / 2;
        float r = trans.localScale.z / 2;
        Vector3 angle = new Vector3(90, 0, 90);
        Vector3 pos = Vector3.zero;
        Transform newTrans = trans;

        // Проверка, не выходит ли объект за границу,
        int i = (R >= trans.localScale.z) ? 1 : 0;
        float limZ = i * 0.95f * Mathf.Sqrt(R * R - trans.localScale.z / 2 * trans.localScale.z / 2); // минимальная высота чтобы не было видно границ объекта
        float limY = R - r * 1.05f;

        x = (x > length) ? length : (x < 0) ? 0 : x;
        x = x - length;

        y = y - R;
        y = (y > limY) ? limY :
            (y < -limY) ? -limY : y;

        // расчет позиции
        pos.x = x;
        pos.y = y;
        pos.z = limZ * Mathf.Sqrt(1 - Mathf.Pow((y / limY), 2));

        // Перевод в глобальные координаты
        pos = pos / 1000;
        newTrans.position = pos;
        newTrans.eulerAngles = angle;
        return newTrans;
    }
}
