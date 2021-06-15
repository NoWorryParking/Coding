using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;
using UnityEngine.UI;

public class ReservationController : MonoBehaviour
{
 
    public TMP_Text numeData;
    public TMP_Text durata;
    public TMP_Text inmatriculare;
    public ButtonManagerBasicWithIcon button;
    private string lat;
    private string lng;
    public void SetInfo(Reservation reservation, int index)
    {
        numeData.text = index + ". " + reservation.nume + "\n Data: " + reservation.data;
        durata.text = "Durata: " + reservation.durata;
        inmatriculare.text = "Nr înmatriculare: "+ reservation.inmatriculare;
        this.lat = reservation.lat;
        this.lng = reservation.lng;
    }
    public void SeeOnMap()
    {var userInput = GameObject.Find("UserInput").GetComponent<InputField>();

        userInput.onEndEdit.Invoke(lng+","+lat);
        var istoric = GameObject.Find("Istoric");
        var rezActiv = GameObject.Find("RezervariActive");
        var win = GameObject.Find("Windows");
        try
        {
            istoric.SetActive(false);
        }
        catch(System.NullReferenceException e)
        {
            Debug.Log("Exception");
        }
        try
        {
            rezActiv.SetActive(false);
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("Exception");
        }
        win.SetActive(false);
    }
}
