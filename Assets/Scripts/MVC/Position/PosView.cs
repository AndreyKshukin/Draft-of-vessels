using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosView : DovView
{
    public void Apply()
    {
        App.Notify(PosNotifiction.ApplyPos, this);
    }

    public void Cancel()
    {
        App.Notify(PosNotifiction.CancelPos, this);
    }

    public void SelectOnTheScheme()
    {
        App.Notify(PosNotifiction.SelectOnTheSchemePos, this);
    }
}
