using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateController : DovController
{
    private CreateModel _crModel => App.Model.CreateModel; 
    override public void OnNotifation(string p_event_path, Object p_target, params object[] p_data)
    {
        PropService service = new PropService();
        switch (p_event_path)
        {
            case CreateNotifiction.OpenCreateViewPanel:
                _crModel.CreateView.SetActive(true);
                foreach (var el in App.Model.Elements)
                {
                    el.Model_3D.GetComponent<MeshCollider>().enabled = true;
                }
                break;

            case CreateNotifiction.CreateCylindricalShell:
                App.Model.Element = new Element();
                App.Model.Element.Uid = System.Guid.NewGuid();
                App.Model.Element.Type = TypeModel.CylidricalShell;
                GameObject model =  Instantiate(_crModel.Cylinder, App.Model.ModelsVeiw.transform);
                App.Model.Element.Model_3D = model;
                
                App.Model.Element.Props = service.CylindricalShell();
                App.Notify(CreateNotifiction.CloseCreateViewPanel, this);
                App.Notify(PropNotifiction.UpdateFieldsProp, this);
                break;

            case CreateNotifiction.CreateConicalShell:
                App.Model.Element = new Element();
                App.Model.Element.Uid = System.Guid.NewGuid();
                App.Model.Element.Props = service.ConicalShell();
                App.Notify(CreateNotifiction.CloseCreateViewPanel, this);
                App.Notify(PropNotifiction.UpdateFieldsProp, this);
                break;

            case CreateNotifiction.CloseCreateViewPanel:
                foreach (var el in App.Model.Elements)
                {
                    el.Model_3D.GetComponent<MeshCollider>().enabled = false;
                }
                _crModel.CreateView.SetActive(false);
                break;
        }
    }
}
