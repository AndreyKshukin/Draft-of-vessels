using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class VesselElementController : DovController
{
    private VesselElementModel _vesselModel => App.Model.VesselElementModel;

    public override void OnNotifation(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case VesselElementNotifiction.AddParentElement:
                App.Model.Element.DestroyEventHandler += App.Model.VesselElementModel.VesselElementView.OnDestroy;
                if(App.Model.Element.Parent != null)
                    App.Model.Element.Parent.DestroyEventHandler += App.Model.Element.DestroyEventHandler;
                break;

            case VesselElementNotifiction.AddCalculationPosElement:
                break;

            case VesselElementNotifiction.AddClickOnObjectElement:
                App.Model.Element.Model_3D.GetComponent<ClickOnObject>().ClickEventHandler += () =>
                {
                    App.Notify(MenuObjectNotifiction.OpenMenuObject, this, p_data[0]);
                };
                break;

            case VesselElementNotifiction.DestroyElement:
                Element el = (Element)p_data[0];

                if (el.Parent != null)
                    el.Parent.DestroyEventHandler += () => el.DestroyEventHandler();

                App.Model.Element.DestroyEventHandler = () =>
                {
                    App.Model.Elements.Remove(el);
                    Destroy(el.Model_3D);
                };
                break;
                
            default:
                break;
        }
    }
}
