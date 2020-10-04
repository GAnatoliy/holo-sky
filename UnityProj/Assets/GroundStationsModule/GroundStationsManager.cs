using Assets.Core.Scripts;
using UnityEngine;

public class GroundStationsManager : MonoBehaviour
{
    public GameObject GroundStationPrefab;
    public Transform Container;
    public SphereCollider EarthCollider;

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
        }        
    }
}
