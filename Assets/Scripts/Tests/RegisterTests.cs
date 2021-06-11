using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class RegisterTests
    {

        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Register");
        }


        [UnityTest]
        public IEnumerator FormExists()
        {
            var inputNume = GameObject.Find("Nume").GetComponent<TMP_InputField>();
            var inputPrenume = GameObject.Find("Prenume").GetComponent<TMP_InputField>();
            var inputParola = GameObject.Find("Parola").GetComponent<TMP_InputField>();
            var inputEmail = GameObject.Find("Email").GetComponent<TMP_InputField>();
            var inputCParola = GameObject.Find("CParola").GetComponent<TMP_InputField>();
            var butonRegister = GameObject.Find("ButtonRegister").GetComponent<ButtonManagerBasicWithIcon>();
            Assert.IsNotNull(inputNume, "Input Nume nu exista");
            Assert.IsNotNull(inputPrenume, "Input Prenume nu exista");
            Assert.IsNotNull(inputParola, "Input Parola nu exista");
            Assert.IsNotNull(inputEmail, "Input Email nu exista");
            Assert.IsNotNull(inputCParola, "Input Confirmare Parola nu exista");
            Assert.IsNotNull(butonRegister, "Buton register nu exista");
            yield return null;
        }
        [UnityTest]
        public IEnumerator MustInsertValues()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            var butonRegister = GameObject.Find("ButtonRegister").GetComponent<ButtonManagerBasicWithIcon>();
            var popup = GameObject.Find("Popup Notification").GetComponent<CanvasGroup>();
            butonRegister.clickEvent.Invoke();
            yield return null;
            Assert.IsTrue(popup.interactable, "Popup nu e activ desi sunt erori");
        }

        [UnityTest]
        public IEnumerator PasswordsMustMatch()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            var inputNume = GameObject.Find("Nume").GetComponent<TMP_InputField>();
            var inputPrenume = GameObject.Find("Prenume").GetComponent<TMP_InputField>();
            var inputParola = GameObject.Find("Parola").GetComponent<TMP_InputField>();
            var inputEmail = GameObject.Find("Email").GetComponent<TMP_InputField>();
            var inputCParola = GameObject.Find("CParola").GetComponent<TMP_InputField>();
            var butonRegister = GameObject.Find("ButtonRegister").GetComponent<ButtonManagerBasicWithIcon>();
            var popup = GameObject.Find("Popup Notification").GetComponent<CanvasGroup>();
            inputNume.text = "Integration";
            inputPrenume.text = "Test";
            inputParola.text = "qweasd123";
            inputCParola.text = "123";
            inputEmail.text = "qewq@gmail.com";
            butonRegister.clickEvent.Invoke();
            yield return null;
            Assert.IsTrue(popup.interactable, "Popup nu e activ desi sunt erori");
        }

        [UnityTest]
        public IEnumerator UserAlreadyExists()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            var inputNume = GameObject.Find("Nume").GetComponent<TMP_InputField>();
            var inputPrenume = GameObject.Find("Prenume").GetComponent<TMP_InputField>();
            var inputParola = GameObject.Find("Parola").GetComponent<TMP_InputField>();
            var inputEmail = GameObject.Find("Email").GetComponent<TMP_InputField>();
            var inputCParola = GameObject.Find("CParola").GetComponent<TMP_InputField>();
            var butonRegister = GameObject.Find("ButtonRegister").GetComponent<ButtonManagerBasicWithIcon>();
            var popup = GameObject.Find("Popup Notification").GetComponent<CanvasGroup>();
            inputNume.text = "Integration";
            inputPrenume.text = "Test";
            inputParola.text = "qweasd123";
            inputCParola.text = "qweasd123";
            inputEmail.text = "radu.ndlcu@gmail.com";
            butonRegister.clickEvent.Invoke();
            yield return new WaitForSeconds(2f); //Astept request de la server
            Assert.IsTrue(popup.interactable, "Popup nu e activ desi sunt erori");
        }

    }
}
