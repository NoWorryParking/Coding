namespace Mapbox.Examples
{
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;

    public class SpawnOnMap : MonoBehaviour
    {
        [SerializeField]
        public AbstractMap _map;

        [SerializeField]
        [Geocode]
        List<ParkingSpot> _parkingSpots = new List<ParkingSpot>();
        public Vector2d[] _locations;

        [SerializeField]
        public float _spawnScale = 1f;

        [SerializeField]
        public GameObject _markerPrefab;

        List<GameObject> _spawnedObjects;
        private void Start()
        {
            InstantiateMarkers();
        }
        private void Update()
        {
            if (_parkingSpots.Count > 0)
            {
                int count = _spawnedObjects.Count;
                for (int i = 0; i < count; i++)
                {
                    var spawnedObject = _spawnedObjects[i];
                    var location = _locations[i];
                    spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                    spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                }
            }
        }
        public void AddLocation(ParkingSpot parkingSpot)
        {
            _parkingSpots.Add(parkingSpot);
          
        }
        public void InstantiateMarkers()
        {
            _locations = new Vector2d[_parkingSpots.Count];
            _spawnedObjects = new List<GameObject>();
            for (int i = 0; i < _parkingSpots.Count; i++) // Iterez prin parcari
            {
                var parkingSpot = _parkingSpots[i].info; // Iau informatia de la fiecare
                _locations[i] = Conversions.StringToLatLon(parkingSpot["lat"] + "," + parkingSpot["lng"]);
                var instance = Instantiate(_markerPrefab); // Instantiez marker-ul
                instance.GetComponent<PoiLabelTextSetter>().Set(parkingSpot); // Pun informatia
                instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true); //Il pun la locul potrivit in "world"
                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                _spawnedObjects.Add(instance);
            }
        }
    }
}