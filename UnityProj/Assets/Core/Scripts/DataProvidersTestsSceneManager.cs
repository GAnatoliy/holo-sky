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

            var groundStationsProvider = new GroundStationsProvider();
            var groundStations = groundStationsProvider.GetGroundStations();
        
            Debug.Log($"Number of ground stations {groundStations.Count}, id of the first one is {groundStations.First().Id}, location {groundStations.First().Location.Latitude} {groundStations.First().Location.Longitude}");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
