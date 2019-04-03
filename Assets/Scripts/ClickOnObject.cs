using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnObject : MonoBehaviour
{
    public Action ClickEventHandler;

    public void OnMouseDown()
    {
        ClickEventHandler?.Invoke();
    }
}
