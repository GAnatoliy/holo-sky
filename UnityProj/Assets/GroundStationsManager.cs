using Assets.Core.Scripts;
using UnityEngine;

public class GroundStationsManager : MonoBehaviour
{
    public GameObject GroundStationPrefab;
    public Transform Container;

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
            gsInstance.AddComponent<GeoObject>().SetCoordinates(gs.Location.Latitude, gs.Location.Longitude);
        }        
    }
}
