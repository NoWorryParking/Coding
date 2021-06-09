
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using System;
using Michsky.UI.ModernUIPack;


public class Rezervare : MonoBehaviour
{
    [SerializeField] DatePickerControl datePicker;
    [SerializeField] DatePickerControl timePicker;
    [SerializeField] TMP_InputField nrOre;
    [SerializeField] TMP_InputField nrInmatriculare;

    [SerializeField] TextMeshProUGUI errorMessage;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] GameObject notification;
   
    
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
        String errorMsg = "";
        if (!checkDateToBeInFuture(zi, luna, an, ora, minut))
        { Debug.Log("Rezervarea trebuie sa fie in viitor");
            errorMsg += "Rezervarea trebuie sa fie in viitor.\n\n";
            ok = false;
        }
        else
        {
            Debug.Log("Data: " + zi + " " + luna + " " + an);
            Debug.Log("Ora: " + ora + " " + minut);
        }

        try
        {
            if (int.Parse(timpRezervat) > 0)
                Debug.Log("Nr ore: " + timpRezervat);
            else
            {
                Debug.Log("Trebuie sa rezervi minim o ora.");
                errorMsg += "Trebuie sa rezervi minim o ora.\n\n";
                ok = false;
            }
        }
        catch(FormatException e)
        {
            Debug.Log("Trebuie sa rezervi minim o ora.");
            errorMsg += "Trebuie sa rezervi minim o ora.\n\n";
            ok = false;
        }
        if (nr.IsMatch(inmatriculare))
            Debug.Log("Nr inmatriculare: " + inmatriculare);
        else
        { Debug.Log("Nr inmatriculare gresit");
            errorMsg += "Nr inmatriculare gresit.\n\n";
            ok = false;
        }

        if (ok) //Daca toate datele au fost introduse corect, trimit request de inregistrare
        {
            StartCoroutine(DBManager.Rezerve(zi, luna, an, ora, minut,timpRezervat, delegate { OnFinishRezervation("Rezervarea a avut loc cu succes"); }, delegate { OnFinishRezervation("A aparut o eroare"); }));
        }
        else
        {
            errorMessage.text = errorMsg;
            notification.GetComponent<NotificationManager>().OpenNotification();
        }
    }

    private bool checkDateToBeInFuture(int zi, int luna, int an, int ora, int min)
    {
       
        DateTime rezervat = new DateTime(an, luna, zi, ora, min, 59);
        return DateTime.Compare(rezervat,DateTime.Now) >=0;
    }

    public void OnFinishRezervation(string msg)
    {
        Debug.Log(msg);
        if(msg.Equals("Rezervarea a avut loc cu succes. "))
        {
            title.text = "FELICITĂRI";
        }
        
        errorMessage.text = msg;
        notification.GetComponent<NotificationManager>().OpenNotification();
        //Caut gameobject in scena si setez mesajul, fie de "rezervarea a avut succes" fie de eroare
    }
}
