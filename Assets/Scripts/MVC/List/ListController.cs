using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = System.Diagnostics.Debug;
using Object = UnityEngine.Object;

public class ListController : DovController
{
    private ListModel _listModel => App.Model.ListModel;
    public override void OnNotifation(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ListNotifiction.OpenListViewPanel:
                if (App.Model.Elements.Count > 0)
                {
                    _listModel.ListView.SetActive(true);
                    App.Notify(ListNotifiction.UpdateFieldsList, this);
                }
                else
                    App.Notify(PosNotifiction.OpenPosView, this);
                break;

            case ListNotifiction.UpdateFieldsList:
                _listModel.Content.ClearList();
                foreach (var el in App.Model.Elements)
                {
                    _listModel.Content.UpdateList(el, App);
                }
                break;

            case ListNotifiction.SelectElement:
                //int i = 0;

                //foreach (var el in App.Model.Elements)
                //{
                //    if (Convert.ToString(el.Uid) == p_data[0].ToString()) 
                //    {
                //        print("test foreach");
                //        App.Model.Element.Parent = App.Model.Elements[i];
                //        string test;
                //        test = (App.Model.Element.Parent == null) ? "Нет" : "есть";
                //        print(test);
                //        break;
                //    }

                //    else
                //    {
                //        i++;
                //    }
                //}
                App.Model.Element.Parent = (Element) p_data[0];
                _listModel.ListView.SetActive(false);
                App.Notify(PosNotifiction.OpenPosView, this);

                break;

            default:
                break;
        }
    }
}
