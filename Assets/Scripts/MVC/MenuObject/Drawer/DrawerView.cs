using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerView : DovElement
{
    public void OpenPanel()
    {
        App.Notify(DrawerNotifiction.OpenDrawer, this);
    }

    public void Apply()
    {
        App.Notify(DrawerNotifiction.ApplyDrawer, this);
    }

    public void Cancel()
    {
        App.Notify(DrawerNotifiction.CancelDrawer, this);
    }

    public void Fill()
    {
        App.Notify(DrawerNotifiction.FillWhiteDrawer, this);
    }

    public void Pen()
    {
        App.Notify(DrawerNotifiction.PenDrawer, this);
    }

    public void Eraser()
    {
        App.Notify(DrawerNotifiction.EraserDrawer, this);
    }

    public void VerticalLine()
    {
        App.Notify(DrawerNotifiction.VerticalDrawer, this);
    }

    public void HorizontalLine()
    {
        App.Notify(DrawerNotifiction.HorizontalDrawer, this);
    }

    public void PictureApply()
    {
        App.Notify(DrawerNotifiction.PictureApplyDrawer, this);
    }

    public void PictureCancel()
    {
        App.Notify(DrawerNotifiction.PictureCancelDrawer, this);
    }
}
