using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : DovController
{
    private CameraModel _camCont => App.Model.CameraModel;

    public override void OnNotifation(string p_event_path, Object p_target, params object[] p_data)
    {
        Vector3 angle = _camCont.Camera.transform.eulerAngles;
        Vector3 pos = new Vector3(0,0,-40);
        Matrix3x3 matr;

        switch (p_event_path)
        {
            case CameraNotifiction.UpCamera:
                angle.x = angle.x + 90;
                _camCont.Camera.transform.eulerAngles = angle;
                matr = new Matrix3x3(pos, angle);
                _camCont.Camera.transform.position = matr.Vector(true);
                break;

            case CameraNotifiction.DownCamera:
                angle.x = angle.x - 90;
                _camCont.Camera.transform.eulerAngles = angle;
                matr = new Matrix3x3(pos, angle);
                _camCont.Camera.transform.position = matr.Vector();
                break;

            case CameraNotifiction.RightCamera:
                angle.y = angle.y + 90;
                _camCont.Camera.transform.eulerAngles = angle;
                matr = new Matrix3x3(pos, angle);
                _camCont.Camera.transform.position = matr.Vector();
                break;

            case CameraNotifiction.LeftCamera:
                angle.y = angle.y - 90;
                _camCont.Camera.transform.eulerAngles = angle;
                matr = new Matrix3x3(pos, angle);
                _camCont.Camera.transform.position = matr.Vector();
                break;
        }
    }
}
