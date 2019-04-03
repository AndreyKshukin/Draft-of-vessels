using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DovApplication : MonoBehaviour
{
    public DovModel Model;
    public DovView View;
    public DovController Controller;

    public void Notify(string p_event_path, Object p_target, params object[] p_data)
    {
        DovController[] controllerList = GetAllControllers();
        foreach (DovController c in controllerList)
        {
            c.OnNotifation(p_event_path, p_target, p_data);
        }
    }

    public DovController[] GetAllControllers()
    {
        return GameObject.FindObjectsOfType<DovController>();
    }
}
