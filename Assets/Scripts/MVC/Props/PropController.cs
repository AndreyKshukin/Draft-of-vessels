using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// В будущем, присваивание трехмерных моделей нужно перенести в PropsController
public class PropController : DovController
{
    private PropModel _propModel => App.Model.PropModel;

    override public void OnNotifation(string p_event_path, Object p_target, params object[] p_data)
    {
        
        PropService service = new PropService();
        switch (p_event_path)
        {
            // открывет таблицу с параметрами
            case PropNotifiction.UpdateFieldsProp:
                foreach (var el in App.Model.Element.Props)
                {
                    _propModel.Content.UpdateList(el);
                }
                _propModel.PropView.SetActive(true);
                break;

            //Сохраняет изменения введенные в таблицу
            case PropNotifiction.ApplyProp:
                Flange flange = new Flange();
                bool addList = true;
                int i = 0;

                foreach (var el in App.Model.Elements)
                {
                    if (el.Uid == App.Model.Element.Uid)
                    {
                        App.Notify(CreateNotifiction.OpenCreateViewPanel, this);
                        _propModel.Content.ApplyProps(App.Model.Elements[i]);
                        flange.FlangeService(App.Model.Elements[i]);
                        el.PositionEventHandler?.Invoke();
                        addList = false;
                        break;
                    }
                    else
                        i++;
                }

                if (addList)
                {
                    _propModel.Content.ApplyProps(App.Model.Element);
                    flange.FlangeService(App.Model.Element);
                    App.Notify(ListNotifiction.OpenListViewPanel, this);
                }
                
                _propModel.Content.ClearList();
                _propModel.PropView.SetActive(false);
                break;

            //Закрывает таблицу без передачи параметров
            case PropNotifiction.CancelProp:
                // Идет проверка списка. Если елемент находится в списке, то объект остается на месте, в противном случае он удаляется.
                bool del = true;
                foreach (var el in App.Model.Elements)
                {
                    if (el.Uid == App.Model.Element.Uid)
                        del = false;
                }

                if (del)
                    Destroy(App.Model.Element.Model_3D);

                _propModel.Content.ClearList();
                _propModel.PropView.SetActive(false);
                App.Notify(CreateNotifiction.OpenCreateViewPanel, this);
                break;
        }
    }
}
