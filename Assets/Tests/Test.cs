using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject Parent;
    public float x = 0;
    public float y = 0;
    public int i = 0;
    private PosCylinderCylinder _normal;

    private void Awake()
    {
         _normal = new PosCylinderCylinder();
    }

    private void Update()
    {
        if (i > 2)
            i = 2;

        _normal.Position(Convert.ToString(i), x, y, gameObject, Parent);
    }

}
