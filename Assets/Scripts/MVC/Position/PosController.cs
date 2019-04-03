using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Object = UnityEngine.Object;

public class PosController : DovController
{
    private PosModel _posModel => App.Model.PosModel;

    public override void OnNotifation(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case PosNotifiction.OpenPosView:

                _posModel.Content.ClearList();
                _posModel.PosView.SetActive(true);

                bool checkList = false;
                
                Element result = App.Model.Elements.
                    Find(delegate(Element el) 
                        { return el.Uid == App.Model.Element.Uid; });

                if (result == null)
                {
                    PosService service = new PosService();

                    if (App.Model.Element.Parent == (null))
                    {
                        App.Model.Element.PosProps = service.FirstObject();
                        App.Notify(PosNotifiction.UpdateList, this);
                    }

                    else
                    {
                        switch (App.Model.Element.Parent.Type)
                        {
                            case (TypeModel.CylidricalShell):

                                switch (App.Model.Element.Type)
                                {
                                    case (TypeModel.CylidricalShell):
                                        App.Model.Element.PosProps = service.CylinderCylinder(0);
                                        App.Notify(PosNotifiction.UpdateList, this);
                                        break;
                                }
                                break;
                        }
                    }
                }

                else
                {
                    App.Notify(PosNotifiction.UpdateList, this);
                }
                break;


            case PosNotifiction.UpdateList:
                foreach (var el in App.Model.Element.PosProps)
                {
                    _posModel.Content.UpdateList(el);
                }
                break;

            case PosNotifiction.ApplyPos:
                _posModel.Content.ApplyPos(App.Model.Element.PosProps);
                _posModel.PosView.SetActive(false);
                
                bool addList = true;  //проверка на наличие объекта в списке, если false - объект заменяется, true - добавляется в библиотеку

                for (int j = 0; j < App.Model.Elements.Count; j++)
                {
                    if (App.Model.Elements[j].Uid == App.Model.Element.Uid)
                    {
                        App.Model.Elements[j].PosProps = App.Model.Element.PosProps;
                        addList = false;
                        break;
                    }
                }

                if (addList)
                {
                    if (App.Model.Element.Parent == null)
                    {
                        FirstPosition calculPos = new FirstPosition();
                        App.Model.Element.PositionEventHandler = () =>
                            calculPos.Position(App.Model.Element.PosProps[0].Value, App.Model.Element.Model_3D);
                    }

                    else
                    {
                        if (App.Model.Element.Parent.Type == TypeModel.CylidricalShell)
                        {
                            PosCylinderCylinder calcul = new PosCylinderCylinder();

                            App.Model.Element.PositionEventHandler += () =>
                                calcul.Position(App.Model.Element.PosProps[0].Value,
                                    Convert.ToSingle(App.Model.Element.PosProps[1].Value),
                                    Convert.ToSingle(App.Model.Element.PosProps[2].Value), 
                                    App.Model.Element.Model_3D, App.Model.Element.Parent.Model_3D);
                        }
                        App.Model.Element.Parent.PositionEventHandler += App.Model.Element.PositionEventHandler;
                    }
                    App.Model.Elements.Add(App.Model.Element);
                    App.Notify(VesselElementNotifiction.AddClickOnObjectElement, this, App.Model.Elements[App.Model.Elements.Count - 1]);
                    App.Notify(VesselElementNotifiction.DestroyElement, this, App.Model.Elements[App.Model.Elements.Count - 1]);
                }
                App.Model.Element.PositionEventHandler.Invoke();
                App.Notify(CreateNotifiction.OpenCreateViewPanel, this);
                break;

            case PosNotifiction.CancelPos:
                {
                    bool checkDel = true;
                    _posModel.PosView.SetActive(false);
                    App.Notify(CreateNotifiction.OpenCreateViewPanel, this);
                    foreach (var el in App.Model.Elements)
                    {
                        if (el.Uid != App.Model.Element.Uid) continue;
                        checkDel = false;
                        break;
                    }

                    if (checkDel)
                        Destroy(App.Model.Element.Model_3D);
                }
                break;
        }
    }
}
