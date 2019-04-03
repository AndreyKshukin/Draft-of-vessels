using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flange : MonoBehaviour
{
    internal void FlangeService(Element element)
    {
        DestroyOldFlange(element.Model_3D);
        DropService drop = new DropService();
        int sizeDrop = drop.Prop(element.Props[element.Props.Count - 1].TypeDrop).Count;
        int valueDrop = Convert.ToInt32(element.Props[element.Props.Count - 1].Value);

        if (sizeDrop==4)
        {
            if (valueDrop == 1)
            {
                CreateFlangeLeft(element.Model_3D);
                CreateFlangeRight(element.Model_3D);
            }

            else if (valueDrop == 2)
            {
                CreateFlangeLeft(element.Model_3D);
            }
            else if (valueDrop == 3)
            {
                CreateFlangeRight(element.Model_3D);
            }
        }
    }

    private Vector3 SizeFlange(GameObject element)
    {
        Vector3 size;

        size.x = element.transform.localScale.z / element.transform.localScale.x * 0.15f;
        size.y = 1.15f;
        size.z = size.y;

        return size;
    }

    private void CreateFlangeLeft(GameObject element)
    {
        GameObject flangeLeft = Instantiate(Resources.Load<GameObject>("Prefabs/Flange"), Vector3.zero, new Quaternion(0, 0, 0, 0), element.transform);
        flangeLeft.name = "FlangeLeft";
        flangeLeft.transform.localScale = SizeFlange(element);

        Vector3 pos = Vector3.zero;
        pos.x = -0.001f;

        flangeLeft.transform.localPosition = pos;
    }

    private void CreateFlangeRight(GameObject element)
    {
        GameObject flangeRight = Instantiate(Resources.Load<GameObject>("Prefabs/Flange"), Vector3.zero, new Quaternion(0, 0, 0, 0), element.transform);
        flangeRight.name = "FlangeRight";
        flangeRight.transform.localScale = SizeFlange(element);

        Vector3 pos = Vector3.zero;
        pos.x = 0.001f * (element.transform.localScale.z / element.transform.localScale.x * 0.15f);

        flangeRight.transform.localPosition = pos;
    }

    private void DestroyOldFlange(GameObject element)
    {
        GameObject flangeLeft = GameObject.Find("FlangeLeft");
        GameObject flangeRight = GameObject.Find("FlangeRight");
        if (flangeLeft != null)
        {
            Destroy(flangeLeft);
        }
        if (flangeRight != null)
        {
            Destroy(flangeRight);
        }
    }

    //private void PosRight(GameObject element)
    //{
    //    Matrix3x3 matrix = new Matrix3x3(Parent.transform.eulerAngles, Position);

    //    Vector3 transformator = matrix.Vector();
    //    Vector3 position;

    //    position.x = transformator.x + Parent.transform.position.x;
    //    position.y = transformator.y + Parent.transform.position.y;
    //    position.z = transformator.z + Parent.transform.position.z;

    //    transform.position = position;

    //    transform.eulerAngles = Parent.transform.eulerAngles;
    //}
}
