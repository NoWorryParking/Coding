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

    [SerializeField] private TextMeshProUGUI errorMessage;
    [SerializeField] private GameObject notification;

    public void Login()
    {
        Debug.Log("Email: " + email.text);
        Debug.Log("Password: " + password.text);
        StartCoroutine(DBManager.LogIn(email.text,password.text, delegate() { Debug.Log(User.email);  SceneManager.LoadScene("POIPlacement"); }, OpenErrorDialog));
        
    }

    public void goToRegister()
    {
        Debug.Log("Pressed register");
        SceneManager.LoadScene("Register");
    }

    public void OpenErrorDialog()
    {
        Debug.Log("Eroare la login.");

        errorMessage.text = "Logarea a eșuat.";
        notification.GetComponent<NotificationManager>().OpenNotification();

        
    }

}
