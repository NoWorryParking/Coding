
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using System;
public class Rezervare : MonoBehaviour
{
    [SerializeField] DatePickerControl datePicker;
    [SerializeField] DatePickerControl timePicker;
    [SerializeField] TMP_InputField nrOre;
    [SerializeField] TMP_InputField nrInmatriculare;

    private Regex nr = new Regex("[A-Z][A-Z]-[0-9][0-9][0-9]?[-][A-Z]{3}");
    public void rezerva() // TO DO: Mesajele de eroare
    {
        bool ok = true;
        var tmp = datePicker.fecha;
        var zi = tmp.Day;
        var luna = tmp.Month;
        var an = tmp.Year;
        tmp = timePicker.fecha;
        var ora = tmp.Hour;
        var minut = tmp.Minute;
        var timpRezervat = nrOre.text;
        var inmatriculare = nrInmatriculare.text;
        if (!checkDateToBeInFuture(zi, luna, an, ora, minut))
        { Debug.Log("Rezervarea trebuie sa fie in viitor");
            ok = false;
        }
        else
        {
            Debug.Log("Data: " + zi + " " + luna + " " + an);
            Debug.Log("Ora: " + ora + " " + minut);
        }
        if (int.Parse(timpRezervat) > 0)
            Debug.Log("Nr ore: " + timpRezervat);
        else
        { Debug.Log("Trebuie sa rezervi minim o ora.");
            ok = false;
        }
        if (nr.IsMatch(inmatriculare))
            Debug.Log("Nr inmatriculare: " + inmatriculare);
        else
        { Debug.Log("Nr inmatriculare gresit");
            ok = false;
        }

        if (ok) //Daca toate datele au fost introduse corect, trimit request de inregistrare
        {
            StartCoroutine(DBManager.Rezerve(zi, luna, an, ora, minut, delegate { OnFinishRezervation("Rezervarea a avut loc cu succes"); }, delegate { OnFinishRezervation("A aparut o eroare"); }));
        }
    }

    private bool checkDateToBeInFuture(int zi, int luna, int an, int ora, int min)
    {
        var now = DateTime.Now;
        return (an >= now.Year && luna >= now.Month && zi >= now.Day && ora >= now.Hour && min >= now.Minute);
    }

    public void OnFinishRezervation(string msg)
    {
        Debug.Log(msg);
        //Caut gameobject in scena si setez mesajul, fie de "rezervarea a avut succes" fie de eroare
    }
}
