using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using TMPro;
using Michsky.UI.ModernUIPack;
namespace Tests
{
    public class LoginTests
    {
    
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Login");
        }
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator LogInFormExists()
        {
            var butonLogin = GameObject.Find("ButtonLogin").GetComponent<ButtonManagerBasicWithIcon>();
            var inputParola = GameObject.Find("Parola").GetComponent<TMP_InputField>();
            var inputEmail = GameObject.Find("Username").GetComponent<TMP_InputField>();
            Assert.NotNull(butonLogin, "Buton login nu exista");
            Assert.NotNull(inputParola, "Input parola nu exista");
            Assert.NotNull(inputEmail, "Input email nu exista");
            yield return null;
        }

        [UnityTest]
        public IEnumerator CanLogInWithValidCredentials()
        {
            var butonLogin = GameObject.Find("ButtonLogin").GetComponent<ButtonManagerBasicWithIcon>();
            var inputParola = GameObject.Find("Parola").GetComponent<TMP_InputField>();
            var inputEmail = GameObject.Find("Username").GetComponent<TMP_InputField>();
            inputEmail.text = "radu.ndlcu@gmail.com";
            inputParola.text = "qweasd123";
         
            butonLogin.clickEvent.Invoke();
            yield return new WaitForSeconds(2.5f); //Astept sa se faca request-ul de la server
            Assert.IsNotEmpty(User.email, "User email is empty after login");
        }

        [UnityTest]
        public IEnumerator LoginFailsWithWrongPassword()
        {
            var butonLogin = GameObject.Find("ButtonLogin").GetComponent<ButtonManagerBasicWithIcon>();
            var inputParola = GameObject.Find("Parola").GetComponent<TMP_InputField>();
            var inputEmail = GameObject.Find("Username").GetComponent<TMP_InputField>();
            inputEmail.text = "radu.ndlcu@gmail.com";
            inputParola.text = "qweasd2123";

            butonLogin.clickEvent.Invoke();
            yield return new WaitForSeconds(2.5f); //Astept sa se faca request-ul de la server
            Assert.IsEmpty(User.email, "User email is not empty after failed login");
        }

        [UnityTest]
        public IEnumerator LoginFailsWithWrongEmail()
        {
            var butonLogin = GameObject.Find("ButtonLogin").GetComponent<ButtonManagerBasicWithIcon>();
            var inputParola = GameObject.Find("Parola").GetComponent<TMP_InputField>();
            var inputEmail = GameObject.Find("Username").GetComponent<TMP_InputField>();
            inputEmail.text = "radu.ndlcuaaa@gmail.com";
            inputParola.text = "qweasd123";

            butonLogin.clickEvent.Invoke();
            yield return new WaitForSeconds(2.5f); //Astept sa se faca request-ul de la server
            Assert.IsEmpty(User.email, "User email is not empty after failed login");
        }

        [UnityTest]
        public IEnumerator CanGoToRegister()
            {
            var butonLogin = GameObject.Find("ButtonRegister").GetComponent<ButtonManagerBasicWithIcon>();
            butonLogin.clickEvent.Invoke();
            SceneManager.activeSceneChanged += delegate { Assert.AreEqual(SceneManager.GetActiveScene().name, "Register", "S-a incarcat alta scena"); };
            yield return new WaitForSeconds(2.5f); //Astept sa se incarce scena
            Assert.AreEqual(SceneManager.GetActiveScene().name, "Register", "Nu s-a ajuns la scena de register");
          

        }

    }
}
