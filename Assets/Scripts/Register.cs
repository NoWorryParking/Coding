
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField nume;
    [SerializeField] private TMP_InputField prenume;
    [SerializeField] private TMP_InputField parola;
    [SerializeField] private TMP_InputField confirmare;

    [SerializeField] TextMeshProUGUI errorMessage;
    [SerializeField] GameObject notification;

    public void GetAllTextFromInputFields()
    {
        string err_email = "";
        string err_nume = "";
        string err_prenume = "";
        string err_parola = "";
        string err_confirmare = "";


        Regex regex_mail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        if (email.text != "")
        {
            Match match = regex_mail.Match(email.text);
            if (!match.Success)
            {
                err_email = "Email incorect.\n\n";
            }
        }
        else
        {
            err_email = "Introduceti email.\n\n";
        }

        if (nume.text != "")
        {
            bool isDigitPresent = nume.text.Any(c => char.IsDigit(c));
            if (isDigitPresent)
            {
                err_nume = "Nume incorect.\n\n";
            }
        }
        else
        {
            err_nume = "Introduceti nume.\n\n";
        }

        if (prenume.text != "")
        {
            bool isDigitPresent = prenume.text.Any(c => char.IsDigit(c));
            if (isDigitPresent)
            {
                err_nume = "Preume incorect.\n\n";
            }
        }
        else
        {
            err_prenume = "Introduceti prenume.\n\n";
        }

        if (parola.text != "")
        {
            if (parola.text.Length < 6)
            {
                err_parola = "Parola trb sa contina minim 6 caractere.\n\n";
            }

        }
        else
        {
            err_parola = "Introduceti parola.\n\n";
        }

        if (confirmare.text != "")
        {
            if (confirmare.text != parola.text)
            {
                err_confirmare = "Parola si confirmarea nu se potrivesc.\n\n";
            }
        }
        else
        {
            err_confirmare = "Introduceti confirmare parola.\n\n";
        }

        if (err_parola == "" && err_nume == "" && err_prenume == "" && err_email == "" && err_confirmare == "") //daca nu sunt erori
        {
            print("user " + nume + " " + prenume + "email " + email);
            StartCoroutine(DBManager.InsertUser(nume.text, prenume.text, email.text, parola.text, delegate { SceneManager.LoadScene("POIPlacement"); }, OpenErrorDialog));

        }
        else
        {
            string eroare = err_nume + err_prenume +  err_email + err_confirmare +  err_parola ;
            errorMessage.text = eroare;
            notification.GetComponent<NotificationManager>().OpenNotification();
            
            Debug.Log(eroare);

        } }



        public void OpenErrorDialog()
        {

            errorMessage.text = "Exista deja un cont asociat email-ului introdus.";
            notification.GetComponent<NotificationManager>().OpenNotification();
            
            Debug.Log("Eroare la inserare in baza de date");
        }


    }

