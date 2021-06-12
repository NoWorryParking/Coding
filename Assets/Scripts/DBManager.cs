using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;

public class DBManager : MonoBehaviour
{

    static private string secretKey = "noworrypSecretKey"; // Edit this value and make sure it's the same as the one stored on the server
    static string insertParkingURL = "http://noworryparking.online/insertparking.php?"; //be sure to add a ? to your url if using WWW
    static private string loginURL = "http://noworryparking.online/login.php?"; //Primeste email, hashed pass si hash (atentie cum se calculeaza), returneaza id daca exista
    static private string insertUserURL = "http://noworryparking.online/insertUser.php?";
    static private string reservationURL = "http://noworryparking.online/reservation.php?";
    static private string deleteReservationURL = "http://noworryparking.online/deletereservation.php?";
    public static IEnumerator LogIn(string email, string password, Action toDo, Action toDoFail)
    {
        string hashedPass = Md5Sum(password);
        string hash = Md5Sum(email + hashedPass + secretKey);
        string url = loginURL + "email="+ WWW.EscapeURL(email) + "&parola="+ hashedPass + "&hash=" + hash;
        Debug.Log("LogIn URL: "+url);
        WWW hs_post = new WWW(url);
        yield return hs_post; // Wait until the download is done


        if (hs_post.error != null)
        {
            Debug.Log("There was an error while trying to log in:" + hs_post.error);
            toDoFail();
            
        }
        Debug.Log(hs_post.text);
        if (hs_post.text == "1")
        { User.email = email;
            Debug.Log("Login reusit");
            toDo();
        }
        else
        {
            toDoFail();
        }
       
       
    }

    // remember to use StartCoroutine when calling this function!
    public static IEnumerator InsertParking(ParkingSpot parking)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string id = parking.info["reference"].ToString();
        string locatie = parking.info["vicinity"].ToString();
        string name = parking.info["name"].ToString();
        string lng = parking.info["lng"].ToString();
        string lat = (string) parking.info["lat"];
        int nrloc = UnityEngine.Random.Range(10, 200);
        string hash = Md5Sum(name + locatie + secretKey);

        string post_url = insertParkingURL + "id=" + WWW.EscapeURL(id) + "&name=" + WWW.EscapeURL(name) + "&lng=" + lng + "&lat=" + lat + "&nrloc=" + nrloc  +"&loc=" + WWW.EscapeURL(locatie) + "&hash=" + hash;
       Debug.Log(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error inserting the parking: " + hs_post.error);
        }
    }

    public static IEnumerator InsertUser(string nume, string prenume, string email, string parola, Action toDoSuccess, Action toDoFail)
    {
        Debug.Log("Called InsertUser");
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(prenume + nume + email + secretKey);
        parola = Md5Sum(parola);

        string post_url = insertUserURL + "&nume=" + WWW.EscapeURL(nume) + "&prenume=" + WWW.EscapeURL(prenume) + "&parola=" + WWW.EscapeURL(parola) + "&email=" + WWW.EscapeURL(email) + "&hash=" + hash;
        Debug.Log(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error registering" + hs_post.error);
        }
        else
        {
         if(hs_post.text == "ok")
            {
                User.email = email;
                toDoSuccess();
            }
                if(hs_post.text == "emailul exista deja")
            {
                toDoFail();
            }

        }
        
        
    }


    public static IEnumerator Rezerve(int zi, int luna, int an, int ora,int min,string nrOre, string inmatriculare, Action toDoSuccess, Action toDoFail)
    {
        Debug.Log("Called Rezerva");
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        var currentUser = User.email;
        string hash = Md5Sum(currentUser + secretKey);

        //Cache reservation for IAPManager
        LastReservation.zi = zi;
        LastReservation.luna = luna;
        LastReservation.ora = ora;
        LastReservation.minut = min;
        LastReservation.timpRezervat = nrOre;
        LastReservation.inmatriculare = inmatriculare;
        // add nrmaticulare exact asa
        string post_url = reservationURL + "&zi=" + zi + "&luna=" +luna + "&an=" +an + "&ora=" + ora + "&min=" + min+ "&nrore=" + nrOre+ "&email=" + WWW.EscapeURL(currentUser) + "&nrmatriculare="+WWW.EscapeURL(inmatriculare) + "&idParcare="+ User.intentParkingSpotId + "&hash=" + hash;
        Debug.Log(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error registering" + hs_post.error);
        }
        else
        {
            Debug.Log(hs_post.text);
            if (hs_post.text == "ok")
            {
               
                toDoSuccess();
            }
            else
                toDoFail();
            

        }


    }
    public static IEnumerator DeleteReservation(int zi, int luna, int an, int ora, int min, string nrOre, string inmatriculare, Action toDoSuccess, Action toDoFail)
    {
        Debug.Log("Called Rezerva");
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        var currentUser = User.email;
        string hash = Md5Sum(currentUser + secretKey);

        // add nrmaticulare exact asa
        string post_url = deleteReservationURL + "&zi=" + zi + "&luna=" + luna + "&an=" + an + "&ora=" + ora + "&min=" + min + "&nrore=" + nrOre + "&email=" + WWW.EscapeURL(currentUser) + "&nrmatriculare=" + WWW.EscapeURL(inmatriculare) + "&idParcare=" + User.intentParkingSpotId + "&hash=" + hash;
        Debug.Log(post_url);
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error deleting reservation" + hs_post.error);
        }
        else
        {
            Debug.Log(hs_post.text);
            if (hs_post.text == "ok")
            {

                toDoSuccess();
            }
            else
                toDoFail();


        }


    }
    static private string Md5Sum(string strToEncrypt)
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
