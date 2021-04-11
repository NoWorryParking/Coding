using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.MiniJSON;

public class StaticMap : MonoBehaviour
{
    public Text log;
    private string url = "";
    public bool loadAllResults;
    public float lat= 44.9216512f;
    public float lng= 25.4345216f;
    public int zoom = 10;
    public int mapWidth = 640;
    public int mapHeight = 640;
    public enum mapType { roadMap, satellite, hybrid, terrain}
    public mapType mapSelected;
    public int scale=3;
    private bool loadingMap = false;
    private IEnumerator mapCoroutine;
    [SerializeField] private string APIkey = "AIzaSyAniMzEdl6VtCjCJmcUFENK26BtI1Zby3E";
    [SerializeField] private DeviceLocation deviceLocation;
    private string keyUrl;
    private List<ParkingSpot> parkingSpots = new List<ParkingSpot>();
    IEnumerator GetGoogleMap(float lat, float lon)
    {
        if (parkingSpots.Count == 0)
        {
            url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
              "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale + "&maptype=" + mapSelected + keyUrl;
        }
        else
        {
            print("Got parkingSpots");
            url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
                 "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale + "&maptype=" + mapSelected + "&markers=icon:" + parkingSpots[0].icon;
            foreach (var parkingSpot in parkingSpots)
            {
                url += "|" + parkingSpot.lat + "," + parkingSpot.lng;
            }
            url += keyUrl;
        }

        Debug.Log(url);
        loadingMap = true;
        WWW webRequest = new WWW(url);
        yield return webRequest;
        loadingMap = false;
        gameObject.GetComponent<RawImage>().texture = webRequest.texture;
        StopCoroutine(mapCoroutine);
    }
    IEnumerator GetParkingSpots(float lat, float lon)
    //Gaseste locurile de parcare dintr-un cerc cu centrul in lat,lon (exista &rankby=distance care cere sa nu fie si radius pus, dar imi da ZERO_RESULTS for now)
    //Si apeleaza GetGoogleMap
    {
        url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + lat + "," + lon + "&radius=150000&type=parking" + keyUrl;
        Debug.Log("GetParkingSpots URL:" + url);
        WWW webRequest = new WWW(url);
        yield return webRequest;
        print(webRequest.text);
        var dict = Json.Deserialize(webRequest.text) as Dictionary<string, object>;
        object localParkingSpots;
        if(dict.TryGetValue("results", out localParkingSpots))
        {
            var temp = (List<object>) (localParkingSpots);
            foreach(var iter in temp)
            {
                var parkingDict = (Dictionary<string, object>) (iter);
                ParkingSpot parking = new ParkingSpot();
                var geometryDict = (Dictionary<string, object>) parkingDict["geometry"];
                var locationDict = (Dictionary<string, object>) geometryDict["location"];
                parking.lat =  float.Parse(locationDict["lat"].ToString());
                parking.lng = float.Parse(locationDict["lng"].ToString());
                parking.icon = (string) parkingDict["icon"];
                parking.name = (string) parkingDict["name"];
                parking.place_id = (string) parkingDict["place_id"];
                parking.reference = (string) parkingDict["reference"];
                parkingSpots.Add(parking);
                print("New count " + parkingSpots.Count);
            }
        }
        if (loadAllResults)
        {
            object token;

            while (dict.TryGetValue("next_page_token", out token))
            {
                yield return new WaitForSecondsRealtime(1.5f); //Daca nu astepti intre requests, next page token nu devine valid si o sa dea bad request
                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?key=" + APIkey + "&pagetoken=" + token;
                Debug.Log("GetParkingSpots Next Token URL:" + url);
                WWW nextWebRequest = new WWW(url);
                yield return nextWebRequest;
                dict = Json.Deserialize(nextWebRequest.text) as Dictionary<string, object>;
                print("Got the next dict");
                print(nextWebRequest.text);
                if (dict.TryGetValue("results", out localParkingSpots))
                {
                    print("Got results");
                    var temp = (List<object>) (localParkingSpots);
                    foreach (var iter in temp)
                    {
                        var parkingDict = (Dictionary<string, object>) (iter);
                        ParkingSpot parking = new ParkingSpot();
                        var geometryDict = (Dictionary<string, object>) parkingDict["geometry"];
                        var locationDict = (Dictionary<string, object>) geometryDict["location"];
                        parking.lat = float.Parse(locationDict["lat"].ToString());
                        parking.lng = float.Parse(locationDict["lng"].ToString());
                        parking.icon = (string) parkingDict["icon"];
                        parking.name = (string) parkingDict["name"];
                        parking.place_id = (string) parkingDict["place_id"];
                        parking.reference = (string) parkingDict["reference"];
                        parkingSpots.Add(parking);
                        print("New count " + parkingSpots.Count);
                    }
                }
            }
        }
        StartCoroutine(GetGoogleMap(lat,lng));
    }
    // Start is called before the first frame update
    public IEnumerator Start()
    {
        mapHeight = Screen.width;
        mapWidth = Screen.height;
        keyUrl = "&key=" + APIkey;
        if (lat == 0 && lng == 0)
        {
            log.text = "Getting DeciveLocation component";
            deviceLocation = this.GetComponent<DeviceLocation>();
            log.text = "Started getting location";
            yield return StartCoroutine(WaitForLocation());
         lat = deviceLocation.GetLatitude();
        lng = deviceLocation.GetLongitude();
            if (lat == 0 && lng == 0)
                log.text = "Unable to determine location";
        }
        Debug.LogError("Latitude: " + lat + " Longitude: " + lng);
        mapCoroutine =GetParkingSpots(lat, lng);
        StartCoroutine(mapCoroutine);
    }

    IEnumerator WaitForLocation()
    {
        if (deviceLocation.GetLatitude() == 0 || deviceLocation.GetLongitude() == 0)
            yield return new WaitForSeconds(0.1f);
            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
