using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Android;

public class DeviceLocation : MonoBehaviour
{
    private float lon = 0f;
    private float lat = 0f;
    public Text text;
    public float GetLongitude()
    {
        return lon;
    }

    public float GetLatitude()
    {
        return lat;
    }
    private void Awake()
    {
        while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
      //  StartCoroutine(GetDeviceLocation());
    }
    IEnumerator GetDeviceLocation()
    {
        
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            text.text = "No location service enabled";
            Debug.LogError("No location service enabled");
            yield break;
        }
        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            text.text = "Time out";
            Debug.LogError("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            text.text = "Unable to determine location";
            Debug.LogError("Unable to determine device location");
            yield break;
        }
        else
        {
            lon = Input.location.lastData.longitude;
            lat = Input.location.lastData.latitude;
            // Access granted and location value could be retrieved
            text.text = lat + " " + lon;
            Debug.LogError("Location: " + lat + " " + lon + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
}