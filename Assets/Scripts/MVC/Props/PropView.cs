using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropView : DovView
{
    public void ClickApply()
    {
        App.Notify(PropNotifiction.ApplyProp, this);
    }

    public void ClickCancel()
    {
        App.Notify(PropNotifiction.CancelProp, this);
    }

}

