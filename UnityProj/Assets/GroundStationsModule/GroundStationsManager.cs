using Assets.Core.Scripts;
using Assets.Core.Scripts.Dtos;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GroundStationsManager : MonoBehaviour
{
    public GameObject GroundStationPrefab;
    public Transform Container;
    public SphereCollider EarthCollider;

    private GroundStationSelectedEvent _onGroundStationSelectedEvent = new GroundStationSelectedEvent();

    private Dictionary<string, GroundStationObject> _groundObjects = new Dictionary<string, GroundStationObject>();


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

            _groundObjects.Add(gs.Id, geoObj.GetComponent<GroundStationObject>());
        }
    }
    
    public void OnStationSelected(UnityAction<GroundStation> handler)
    {
        _onGroundStationSelectedEvent.AddListener(handler);
    }

    public void UndohilightObject(string id)
    {
        _groundObjects[id]?.UndohilightObject();
    }

    public void HilightObject(string id)
    {
        _groundObjects[id]?.HilightObject();
    }

    private void StationSelected(GroundStation model)
    {
        _onGroundStationSelectedEvent.Invoke(model);
    }
}
