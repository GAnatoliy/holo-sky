using System.Linq;
using UnityEngine;


// ReSharper disable once UnusedType.Global
namespace Assets.Core.Scripts
{
    public class DataProvidersTestsSceneManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log($"nameof(DataProvidersTestsSceneManager) start");

            var dataObjectsProvider = new DataObjectsProvider();
            var groundStations = dataObjectsProvider.GetGroundStations();
        
            Debug.Log($"Number of ground stations {groundStations.Count}, id of the first one is {groundStations.First().Id}, location {groundStations.First().Location.Latitude} {groundStations.First().Location.Longitude}");

            var satellites = dataObjectsProvider.GetSatellites();

            Debug.Log($"Number of satellites {satellites.Count}, id of the first one is {satellites.First().NoradCatId}, {satellites.First().ObjectName}");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
