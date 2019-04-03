using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPosition : MonoBehaviour
{
    /// <summary>
    /// Нулевая позиция в массиве возвращает положение, 1 - угол поворота 
    /// </summary>
    /// <returns></returns>
    public void Position(string pos, GameObject model)
    {
        model.transform.position = (pos == "0") ? new Vector3(model.transform.localScale.x / 2000, 0, 0) : new Vector3(0, -model.transform.localScale.x / 2000, 0);
        model.transform.eulerAngles = (pos == "0") ? new Vector3(0, 0, 0) : new Vector3(0, 0, -90);
    }
}
