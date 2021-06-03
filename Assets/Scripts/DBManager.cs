using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    private string secretKey = "noworrypSecretKey"; // Edit this value and make sure it's the same as the one stored on the server
    private string insertParkingURL = "http://noworryparking.online/insertparking.php?"; //be sure to add a ? to your url if using WWW
    private string insertUserURL = "http://noworryparking.online/insertUser.php?"; //be sure to add a ? to your url if using WWW


    // remember to use StartCoroutine when calling this function!
    public IEnumerator InsertParking(ParkingSpot parking)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string id = parking.info["reference"].ToString();
        string locatie = parking.info["vicinity"].ToString();
        string name = parking.info["name"].ToString();
        string lng = parking.info["lng"].ToString();
        string lat = (string) parking.info["lat"];
        int nrloc = Random.Range(10, 200);
        string hash = Md5Sum(name + locatie + secretKey);

        string post_url = insertParkingURL + "id=" + WWW.EscapeURL(id) + "&name=" + WWW.EscapeURL(name) + "&lng=" + lng + "&lat=" + lat + "&nrloc=" + nrloc  +"&loc=" + WWW.EscapeURL(locatie) + "&hash=" + hash;
        print(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }
    public IEnumerator InsertUser(string nume, string prenume, string email, string parola)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(prenume + nume + email + secretKey);
        parola = Md5Sum(parola);

        string post_url = insertUserURL + "&nume=" + WWW.EscapeURL(nume) + "&prenume=" + WWW.EscapeURL(prenume) + "&parola=" + WWW.EscapeURL(parola) + "&email=" + WWW.EscapeURL(email) + "&hash=" + hash;
        print(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            print("There was an error posting the high score: " + hs_post.error);
        }
    }


    private string Md5Sum(string strToEncrypt)
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
