using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class HSController : MonoBehaviour
{
    private string secretKey = "noworrypSecretKey"; // Edit this value and make sure it's the same as the one stored on the server
    private string addScoreURL = "http://noworryparking.online/addscore2.php?"; //be sure to add a ? to your url
    private string highscoreURL = "http://noworryparking.online/display.php";

    void Start()
    {
        print("Started");
        //StartCoroutine(PostScores("Testing2", 4321));
        StartCoroutine(GetScores());
        getUrl("Testin2", 3423);
    }
    private string getUrl(string name, int score)
    {
        string hash = Md5Sum(name + score + secretKey);

        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
        print(post_url);
        return post_url;
    }
    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores(string name, int score)
    {
       
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(name + score + secretKey);

        string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
        print(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        print(hs_post.url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }
    IEnumerator PostScoresUnityWeb(string name, int score)
    {
        WWWForm form = new WWWForm();
        string hash = Md5Sum(name + score + secretKey);
        form.AddField("name", name);
        form.AddField("score", score);
        form.AddField("hash", hash);
        UnityWebRequest www = UnityWebRequest.Post("http://noworryp.unaux.com/addscore2.php", form);
       
     
        yield return www.SendWebRequest();
        print("Done");
        if (www.responseCode != 201)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
        print("Loading scores");
        WWW hs_get = new WWW(highscoreURL);
        yield return hs_get;

        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            print(hs_get.text); // this is a GUIText that will display the scores in game.
        }
    }
    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}