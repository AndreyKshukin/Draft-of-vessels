using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateView : DovView
{
    public void CreateCylindricalShell()
    {
        App.Notify(CreateNotifiction.CreateCylindricalShell, this);
    }

    public void CreateConicalShell()
    {
        App.Notify(CreateNotifiction.CreateConicalShell, this);
    }

}
