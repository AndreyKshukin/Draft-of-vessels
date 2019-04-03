using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ContentList : MonoBehaviour
{
    [SerializeField]
    private List<FieldProp> _listFiel = new List<FieldProp>();
    public FieldProp Field;

    public void UpdateList(DovProp prop)
    {
        Field.LoadProp(prop);
        FieldProp f = Instantiate(Field, transform);
        _listFiel.Add(f);
    }

    public void UpdateList(PosProp prop)
    {
        Field.LoadProp(prop);
        FieldProp f = Instantiate(Field, transform);
        _listFiel.Add(f);
    }

    public void UpdateList(Element element, DovApplication app)
    {
        Field.LoadProp(element, app);
        FieldProp f = Instantiate(Field, transform);
        f.LoadProp(element, app);
        _listFiel.Add(f);
    }

    public void ClearList()
    {
        foreach (var el in _listFiel)
        {
            el.OnDestroy();
        }
        _listFiel.Clear();
    }

    public void ApplyProps(Element element)
    {
        int i = 0;
        Vector3 trans;
        foreach (var el in _listFiel)
        {
            el.Apply(element.Props[i]);

            switch (element.Props[i].NameField)
            {
                case ("Length"):
                    trans = element.Model_3D.transform.localScale;
                    trans.x = Convert.ToSingle(element.Props[i].Value);
                    element.Model_3D.transform.localScale = trans;
                    break;

                case ("Diameter"):
                    trans = element.Model_3D.transform.localScale;
                    trans.y = Convert.ToSingle(element.Props[i].Value);
                    trans.z = trans.y;
                    element.Model_3D.transform.localScale = trans;
                    break;

                case ("Falnge"):

                    break;

                default:
                    break;
            }
            i++;
        }
    }

    public void ApplyPos(List<PosProp> props)
    {
        int i = 0;
        foreach (var el in _listFiel)
        {
            el.Apply(props[i]);
            i++;
        }
    }
}
