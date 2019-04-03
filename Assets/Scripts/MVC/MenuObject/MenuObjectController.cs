using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MenuObjectController : DovController
{
    private MenuObjectModel _menuModel => App.Model.MenuObjectModel;

    public override void OnNotifation(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case (MenuObjectNotifiction.OpenMenuObject):
                App.Notify(CreateNotifiction.CloseCreateViewPanel, this);
                App.Model.Element = (Element)p_data[0];
                _menuModel.MenuObjectView.SetActive(true);
                break;

            case (MenuObjectNotifiction.CloseMenuObject):
                _menuModel.MenuObjectView.SetActive(false);
                App.Notify(CreateNotifiction.OpenCreateViewPanel, this);
                break;

            case (MenuObjectNotifiction.DestroyMenuObject):
                foreach (var el in App.Model.Elements)
                {
                    if (el.Uid != App.Model.Element.Uid) continue;
                    App.Model.Element = null;
                    el.DestroyEventHandler();
                    _menuModel.MenuObjectView.SetActive(false);
                    App.Notify(CreateNotifiction.OpenCreateViewPanel, this);
                    break;
                }
                break;

            case (MenuObjectNotifiction.DrawingMenuObject):
                _menuModel.MenuObjectView.SetActive(false);
                App.Notify(DrawerNotifiction.OpenDrawer, this);
                break;

            case (MenuObjectNotifiction.ChangePropsMenuObject):
                _menuModel.MenuObjectView.SetActive(false);
                App.Notify(PropNotifiction.UpdateFieldsProp, this);
                break;

            case (MenuObjectNotifiction.ChangePosMenuObject):
                _menuModel.MenuObjectView.SetActive(false);
                App.Notify(PosNotifiction.OpenPosView, this);
                break;

            default:
                break;
        }
    }
}
