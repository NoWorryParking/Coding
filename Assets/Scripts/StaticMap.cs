using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.MiniJSON;
using Mapbox.Examples;

public class StaticMap : MonoBehaviour
{
    private string url = "";
    public bool loadAllResults;
    public enum mapType { roadMap, satellite, hybrid, terrain}
    public mapType mapSelected;
    private IEnumerator mapCoroutine;
    [SerializeField] private string APIkey = "AIzaSyAniMzEdl6VtCjCJmcUFENK26BtI1Zby3E";
    [SerializeField] private DeviceLocation deviceLocation;
    [SerializeField] private SpawnOnMap creator;
    private string keyUrl;
    public IEnumerator GetParkingSpots(double lat, double lon)
    //Gaseste locurile de parcare dintr-un cerc cu centrul in lat,lon (exista &rankby=distance care cere sa nu fie si radius pus, dar imi da ZERO_RESULTS for now)
    //Si apeleaza GetGoogleMap
    {
        Debug.Log("Started getting parkins");
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
                /*parking.lat =  float.Parse(locationDict["lat"].ToString());
                parking.lng = float.Parse(locationDict["lng"].ToString());
                parking.icon = (string) parkingDict["icon"];
                parking.name = (string) parkingDict["name"];
                parking.place_id = (string) parkingDict["place_id"];
                parking.reference = (string) parkingDict["reference"];*/
                parking.info = parkingDict;
                parking.info.Add("lat", float.Parse(locationDict["lat"].ToString()));
                parking.info.Add("lng", float.Parse(locationDict["lng"].ToString()));
                creator.AddLocation(parking);
              
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
                        /*parking.lat =  float.Parse(locationDict["lat"].ToString());
                 parking.lng = float.Parse(locationDict["lng"].ToString());
                 parking.icon = (string) parkingDict["icon"];
                 parking.name = (string) parkingDict["name"];
                 parking.place_id = (string) parkingDict["place_id"];
                 parking.reference = (string) parkingDict["reference"];*/
                        parking.info = parkingDict;
                        parking.info.Add("lat", float.Parse(locationDict["lat"].ToString()));
                        parking.info.Add("lng", float.Parse(locationDict["lng"].ToString()));
                        creator.AddLocation(parking);
                        
                    }
                }
            }
        }
<<<<<<< Updated upstream
=======
        else //Iau parcarile din propria baza de date
        {
            string post_url = "http://noworryparking.online/getparkings.php?" + "lng=" + lon + "&lat=" + lat;
            WWW webRequest = new WWW(post_url);
            yield return webRequest; // Wait until the download is done
            Debug.Log(webRequest.text);
            if (webRequest.error != null)
            {
                print("There was an error posting the high score: " + webRequest.error);
            }
            var parkingList = Json.Deserialize(webRequest.text) as List<object>; //Serverul imi da o lista de dictionare
            //Incep sa parsez raspunsul
            foreach (var iter in parkingList)
            {
                var parkingDict = (Dictionary<string, object>) (iter);
                var parsedDict = new Dictionary<string, object>();
                foreach (var key in parkingDict.Keys)
                    print(key + " " + parkingDict[key]);
                parsedDict.Add("name", parkingDict["nume"]); //Fac trecerea dintre coloane
                parsedDict.Add("lat", parkingDict["latitudine"]);
                parsedDict.Add("lng", parkingDict["logitudine"]);
                parsedDict.Add("vicinity", parkingDict["locatie"]);
                parsedDict.Add("locuriTotale", parkingDict["nrtotallocuri"]);
                /*if (parkingDict.ContainsKey("locuri_ocupate"))
                parsedDict.Add("locuriDisp", ((int) parkingDict["nrtotallocuri"])-(int) parkingDict["locuri_ocupate"]);*/
                parsedDict.Add("locuriDisp", parkingDict["nrlocurilibere"]);
                ParkingSpot parking = new ParkingSpot();
                parking.info = parsedDict;
                creator.AddLocation(parking);

            }
            
        }
>>>>>>> Stashed changes
        creator.InstantiateMarkers();
    }
    //// Incercare de a lua deviceLocation
    //public IEnumerator Start()
    //{
    //    mapHeight = Screen.width;
    //    mapWidth = Screen.height;
    //    keyUrl = "&key=" + APIkey;
    //    if (lat == 0 && lng == 0)
    //    {
    //        log.text = "Getting DeciveLocation component";
    //        deviceLocation = this.GetComponent<DeviceLocation>();
    //        log.text = "Started getting location";
    //        yield return StartCoroutine(WaitForLocation());
    //     lat = deviceLocation.GetLatitude();
    //    lng = deviceLocation.GetLongitude();
    //        if (lat == 0 && lng == 0)
    //            log.text = "Unable to determine location";
    //    }
    //    Debug.LogError("Latitude: " + lat + " Longitude: " + lng);
    //    mapCoroutine =GetParkingSpots(lat, lng);
    //    StartCoroutine(mapCoroutine);
    //}
    private void Start()
    {
        keyUrl = "&key=" + APIkey;
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
