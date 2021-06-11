using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class MainApp
    {
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("POIPlacement");
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator HasSearchUI()
        {
            var userInput = GameObject.Find("UserInput").GetComponent<InputField>();
            Assert.NotNull(userInput);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        [UnityTest]
        public IEnumerator ReturnsParkings()
        {
            var userInput = GameObject.Find("UserInput").GetComponent<InputField>();
       
            userInput.onEndEdit.Invoke("Bucuresti");
            yield return new WaitForSeconds(1.5f);
            Assert.NotNull(GameObject.Find("ParkingSpotPrefab(Clone)"));
            
        }
    }
}
