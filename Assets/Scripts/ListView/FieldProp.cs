using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class FieldProp : MonoBehaviour
{
    public Text Title;
    public GameObject TitleObject;
    public InputField InputValue;
    public GameObject EnterValue;
    public Dropdown Dropdown;
    public GameObject SelectDropdown;
    public Button NameElement;
    public GameObject ButtonName;

    //Загрузка параметров элемена в таблицу
    public void LoadProp(DovProp prop)
    {
        DropService dropService = new DropService();

        PanelOff();
        TitleObject.SetActive(true);
        Title.text = " " + prop.Description + " ";
        switch (prop.TypeField)
        {
            case TypeField.String:
                InputValue.contentType = InputField.ContentType.Standard;
                InputValue.text = prop.Value;
                EnterValue.SetActive(true);
                break;
            case TypeField.Float:
                InputValue.contentType = InputField.ContentType.DecimalNumber;
                InputValue.text = prop.Value;
                EnterValue.SetActive(true);
                break;
            case TypeField.Dropdown:
                SelectDropdown.SetActive(true);
                Dropdown.ClearOptions();

                Dropdown.AddOptions(dropService.Prop(prop.TypeDrop));
                Dropdown.value = Convert.ToInt32(prop.Value);
                break;

            default:
                break;
        }
    }

    //Применение прааметров элемента в таблицу
    public void Apply(DovProp prop)
    {
        switch (prop.TypeField)
        {
            case TypeField.String:
                prop.Value = InputValue.text;
                break;
            case TypeField.Float:
                prop.Value = InputValue.text;
                break;
            case TypeField.Dropdown:
                prop.Value = Convert.ToString(Dropdown.value);
                break;
            default:
                break;
        }
    }

    public void LoadProp(PosProp prop)
    {
        DropService dropService = new DropService();

        PanelOff();
        TitleObject.SetActive(true);
        Title.text = " " + prop.Title + " ";

        switch (prop.TypeField)
        {
            case TypeField.Float:
                InputValue.contentType = InputField.ContentType.DecimalNumber;
                InputValue.text = prop.Value;
                EnterValue.SetActive(true);
                break;

            case TypeField.Dropdown:
                SelectDropdown.SetActive(true);
                Dropdown.ClearOptions();

                Dropdown.AddOptions(dropService.Pos(prop.TypeDropdown));
                Dropdown.value = Convert.ToInt32(prop.Value);
                break;

            default:
                break;
        }
    }

    public void Apply(PosProp prop)
    {
        switch (prop.TypeField)
        {
            case TypeField.Float:
                prop.Value = InputValue.text;
                break;

            case TypeField.Dropdown:
                prop.Value = Convert.ToString(Dropdown.value);
                break;
            default:
                break;
        }
    }

    public void LoadProp(Element element, DovApplication app)
    {
        PanelOff();
        ButtonName.SetActive(true);
        NameElement.onClick.RemoveAllListeners();
        NameElement.onClick.AddListener(() => app.Notify(ListNotifiction.SelectElement, this, element));
    }

    // удаление элемента из таблицы
    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void PanelOff()
    {
        TitleObject.SetActive(false);
        EnterValue.SetActive(false);
        SelectDropdown.SetActive(false);
        ButtonName.SetActive(false);
    }
}
