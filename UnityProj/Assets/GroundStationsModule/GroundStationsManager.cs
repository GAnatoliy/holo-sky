using Assets.Core.Scripts;
using Assets.Core.Scripts.Dtos;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;


public class GroundStationsManager : MonoBehaviour
{
    public GameObject GroundStationPrefab;
    public Transform Container;
    public SphereCollider EarthCollider;

    private GroundStationSelectedEvent _onGroundStationSelectedEvent = new GroundStationSelectedEvent();

    
    // Start is called before the first frame update
    void Start()
    {
        var gsProvider = new DataObjectsProvider();
        var groundStations = gsProvider.GetGroundStations();

        foreach (var gs in groundStations)
        {
            var gsInstance = Instantiate(GroundStationPrefab);
            gsInstance.name = gs.Id;
            gsInstance.transform.SetParent(Container, false);

            var geoObj = gsInstance.AddComponent<GeoObject>();
            geoObj.EarthObjectRadius = EarthCollider.radius;
            geoObj.SetCoordinates(gs.Location.Latitude, gs.Location.Longitude);
            geoObj.GetComponent<GroundStationObject>().Init(gs);
            geoObj.GetComponent<GroundStationObject>().OnStationSelected(StationSelected);
        }
    }

    public void OnStationSelected(UnityAction<GroundStation> handler)
    {
        _onGroundStationSelectedEvent.AddListener(handler);
    }

    private void StationSelected(GroundStation model)
    {
        _onGroundStationSelectedEvent.Invoke(model);
    }
}
