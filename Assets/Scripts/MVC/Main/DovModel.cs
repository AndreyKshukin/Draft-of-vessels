using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Главная модель MVC
/// </summary>
public class DovModel : DovElement
{
    public List<Element> Elements { get; set; }      //Список элементов
    public Element Element { get; set; }         //Свойства открытой модели

    public CameraModel CameraModel;
    public VesselElementModel VesselElementModel;
    public MenuObjectModel MenuObjectModel;
    public CreateModel CreateModel;
    public PropModel PropModel;
    public ListModel ListModel;
    public PosModel PosModel;
    public DrawerModel DrawerModel;

    public GameObject ModelsVeiw;

    private void Start()
    {
        Elements = new List<Element>();
    }
}

