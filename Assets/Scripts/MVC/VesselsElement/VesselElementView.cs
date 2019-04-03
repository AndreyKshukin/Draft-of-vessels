using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselElementView : DovElement
{
    public void OnDestroy()
    {
        if (App?.Model?.Element?.Model_3D == null)
            return;

        Destroy(App.Model.Element.Model_3D);

        foreach (var el in App.Model.Elements)
        {
            if (App.Model.Element.Uid != el.Uid) continue;
            App.Model.Elements.Remove(el);
            break;
        }
    }
}
