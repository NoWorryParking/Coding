using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
public class MenuController : MonoBehaviour
{
    private GameObject popupMenu;
    private PoiLabelTextSetter poi;
    // Start is called before the first frame update
    void Start()
    {
        popupMenu = GameObject.Find("PopUpMenu");
        poi = gameObject.GetComponentInParent<PoiLabelTextSetter>();
    }

    void OnMouseUpAsButton()
    {
        popupMenu.GetComponent<MenuInfoController>().setInfo(poi.info);
        LeanTween.scale(popupMenu, new Vector3(1, 1, 1), 0.25f);
    }
}
