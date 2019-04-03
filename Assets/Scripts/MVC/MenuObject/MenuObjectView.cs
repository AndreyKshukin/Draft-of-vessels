using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObjectView : DovElement
{
    public void Close()
    {
        App.Notify(MenuObjectNotifiction.CloseMenuObject, this);
    }

    public void Props()
    {
        App.Notify(MenuObjectNotifiction.ChangePropsMenuObject, this);
    }

    public void Position()
    {
        App.Notify(MenuObjectNotifiction.ChangePosMenuObject, this);
    }

    public void Destroy()
    {
        App.Notify(MenuObjectNotifiction.DestroyMenuObject, this);
    }

    public void Drawing()
    {
        App.Notify(MenuObjectNotifiction.DrawingMenuObject, this);
    }
}
