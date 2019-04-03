using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlange : MonoBehaviour
{
    private Element _element = new Element();
    [SerializeField] private int _count = 0;
    
    private void Awake()
    {
        PropService prop = new PropService();
        _element.Model_3D = gameObject;
        _element.Props = prop.CylindricalShell();
        _element.Props[_element.Props.Count - 1].Value = System.Convert.ToString(_count);

        Flange flange = new Flange();
        flange.FlangeService(_element);
    }


    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Flange flange = new Flange();
            flange.FlangeService(_element);
        }
    }

}
