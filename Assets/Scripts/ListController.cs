using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ListController : MonoBehaviour
{
    [SerializeField] private GameObject istoric;
    [SerializeField] private GameObject listaIstoric;
    [SerializeField] private GameObject listPrefab;
    [SerializeField] private GameObject mesajPrefab;
    [SerializeField] private GameObject active;
    [SerializeField] private GameObject listaActive;

    public void populateActiveList(List<Reservation> reservations)
    {
        listaActive.transform.GetChild(0).GetComponent<TMP_Text>().text = "Ai " + reservations.Count + " rezervări active";
        int counter = 1;
        for (int i = 1; i < listaActive.transform.childCount; i++)
        {
            GameObject child = listaActive.transform.GetChild(i).gameObject;
            child.Destroy();
        }

        if (reservations.Count == 0)
        {
            var rezervare = GameObject.Instantiate(mesajPrefab, listaActive.transform);
            return;
        }
        foreach (var rez in reservations)
        {
            var rezervare = GameObject.Instantiate(listPrefab, listaActive.transform);
            rezervare.GetComponentInChildren<ReservationController>().SetInfo(rez, counter);
            counter++;
        }
    }
    public void populateList(List<Reservation> reservations)
    {
        for (int i = 0; i <listaIstoric.transform.childCount; i++)
        {
            GameObject child = listaIstoric.transform.GetChild(i).gameObject;
            child.Destroy();
        }

        int counter = 1;
        if (reservations.Count == 0)
        {
            var rezervare = GameObject.Instantiate(mesajPrefab, listaIstoric.transform);
            return;
        }
        foreach (var rez in reservations)
        {
            var rezervare = GameObject.Instantiate(listPrefab, listaIstoric.transform);
            rezervare.GetComponentInChildren<ReservationController>().SetInfo(rez,counter);
            counter++;
        }
        
    }

    public void OpenIstoric()
    {
        istoric.SetActive(true);
        StartCoroutine(DBManager.GetHistory());
    }
    public void OpenActive()
    {
        active.SetActive(true);
        StartCoroutine(DBManager.GetActive());
    }
}
