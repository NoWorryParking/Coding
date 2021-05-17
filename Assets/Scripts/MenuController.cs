using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


namespace Michsky.UI.ModernUIPack
{
public class MenuController : MonoBehaviour
{
    private GameObject popupMenu;
    private GameObject popupMenu1;
    private GameObject modalWindow;
    private PoiLabelTextSetter poi;
    // Start is called before the first frame update
    void Start()
    {
        //popupMenu = GameObject.Find("PopUpMenu");
        popupMenu1 = GameObject.Find("ContentPopUpMenu");
        modalWindow = GameObject.Find("Style 1");
        poi = gameObject.GetComponentInParent<PoiLabelTextSetter>();
    }

    void OnMouseUpAsButton()
    {
        //popupMenu.GetComponent<MenuInfoController>().setInfo(poi.info);
        popupMenu1.GetComponent<MenuInfoController>().setInfo(poi.info);
        modalWindow.GetComponent<ModalWindowManager>().OpenWindow();
        //LeanTween.scale(popupMenu, new Vector3(1, 1, 1), 0.25f);
    }
}
}

