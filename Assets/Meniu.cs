using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Meniu : MonoBehaviour, IPointerDownHandler
{
    public static bool menuIsOpened = false;
    public GameObject menu;
    

    

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(" Was Clicked.");
        if (menuIsOpened)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    void Close()
    {
        menuIsOpened = false;
        menu.SetActive(false);
    }

    void Open()
    {
        menuIsOpened = true;
        menu.SetActive(true);
    }
}
