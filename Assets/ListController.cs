using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListController : MonoBehaviour
{
    [SerializeField] private GameObject listaIstoric;
    [SerializeField] private GameObject listPrefab;
    private void Start()
    {
        populateList();
    }
    public void populateList()
    {
        var rezervare = GameObject.Instantiate(listPrefab);
       
        rezervare.transform.SetParent(listaIstoric.transform);
        rezervare.transform.localScale.Set(1f, 1f, 1f);
    }
}
