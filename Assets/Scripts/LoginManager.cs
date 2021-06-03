using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Michsky.UI.ModernUIPack;
public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private WindowManager windowManager; 
    public void Login()
    {
        Debug.Log("Email: " + email.text);
        Debug.Log("Password: " + password.text);
        DBManager.LogIn(email.text,password.text);
        if (User.email != "") //A avut loc log in cu succes
            SceneManager.LoadScene("POIPlacement");
        else //Daca nu afisez ceva gen "Login invalid"
        {
            Debug.Log("Login invalid");
            windowManager.OpenFirstTab(); //TO DO;
        }
    }

    public void goToRegister()
    {
        Debug.Log("Pressed register");
        SceneManager.LoadScene("Register");
    }
}
