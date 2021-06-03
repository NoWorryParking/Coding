
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
  
    public Text popoutText;
    public GameObject modalWindow;
   
    public void GetAllTextFromInputFields()
    {
        string err_email = "";
        string err_nume = "";
        string err_prenume = "";
        string err_parola = "";
        string err_confirmare = "";


        Regex regex_mail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        if (email.text != "" )
        {
            Match match = regex_mail.Match(email.text);
            if (!match.Success)
            {
                err_email = "Email incorect.";
            }
        }
        else
        {
            err_email = "Introduceti email.";
        }

        if (nume.text != "")
        {
            bool isDigitPresent = nume.text.Any(c => char.IsDigit(c));
            if (isDigitPresent)
            {
                err_nume = "Nume incorect.";
            }
        }
        else
        {
            err_nume = "Introduceti nume.";
        }

        if (prenume.text != "")
        {
            bool isDigitPresent = prenume.text.Any(c => char.IsDigit(c));
            if (isDigitPresent)
            {
                err_nume = "Preume incorect.";
            }
        }
        else
        {
            err_prenume = "Introduceti prenume.";
        }

        if (parola.text != "")
        {
            if(parola.text.Length < 6)
            {
                err_parola = "Parola trb sa contina minim 6 caractere";
            }

        }
        else
        {
            err_parola = "Introduceti parola.";
        }

        if (confirmare.text != "")
        {
            if(confirmare.text != parola.text)
            {
                err_confirmare = "Parola si confirmarea nu se potrivesc.";
            }
        }
        else
        {
            err_confirmare = "Introduceti confirmare parola.";
        }

        if (err_parola == "" && err_nume == "" && err_prenume == "" && err_email == "" && err_confirmare == "") //daca nu sunt erori
        {
            print("user " + nume + " " + prenume + "email " + email);
           StartCoroutine(DBManager.InsertUser(nume.text, prenume.text, email.text, parola.text));
            if (User.email != "") //Daca a avut loc cu succes
            {
                SceneManager.LoadScene("POIPlacement");
            }
            else //Afisez popup de eroare
            {
                modalWindow.GetComponent<ModalWindowManager>().OpenWindow();
                popoutText.text = "A aparut o problema la inserarea in baza de date";
                Debug.Log("Eroare la inserare in baza de date"); 

            }
        }
        else
        {
           // modalWindow.GetComponent<PopupWindowManager>().OpenWindow();
            popoutText.text = err_nume + "<br>" + err_prenume + "<br>" + err_email + "<br>" + err_confirmare + "<br>" + err_parola + "<br>";
            print("erori");

        }






    }
}
