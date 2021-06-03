using UnityEngine;
using Mapbox.Examples;
using Michsky.UI.ModernUIPack;

public class MenuController : MonoBehaviour
{
    private GameObject popupMenu;
    private GameObject popupMenu1;
    private GameObject modalWindow;
    private PoiLabelTextSetter poi;
    // Start is called before the first frame update
    void Start()
    {
      
        popupMenu1 = GameObject.Find("ContentPopUpMenu");
        modalWindow = GameObject.Find("PopUpInfoParcare");
        poi = gameObject.GetComponentInParent<PoiLabelTextSetter>();
    }

    void OnMouseUpAsButton()
    {
       
        popupMenu1.GetComponent<MenuInfoController>().setInfo(poi.info);
        modalWindow.GetComponent<ModalWindowManager>().OpenWindow();
        //LeanTween.scale(popupMenu, new Vector3(1, 1, 1), 0.25f);
    }
}


