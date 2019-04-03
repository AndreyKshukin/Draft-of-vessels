using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DovElement : MonoBehaviour
{
    private DovApplication _app;

    public DovApplication App
    {
        get
        {
            if (_app==null)
                _app= GameObject.FindObjectOfType<DovApplication>();
            return _app;
        }
    }

}
