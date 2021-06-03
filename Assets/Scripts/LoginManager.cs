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
        StartCoroutine(DBManager.LogIn(email.text,password.text, delegate() { SceneManager.LoadScene("POIPlacement"); }));

        //TO DO: Apare mesaj de eroare daca n-a mers coroutitina
    }

    public void goToRegister()
    {
        Debug.Log("Pressed register");
        SceneManager.LoadScene("Register");
    }
}
