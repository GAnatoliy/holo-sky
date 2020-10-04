using Assets.Core.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts
{
    public class SatellitsManager : MonoBehaviour
    {
        public List<Sputnik> Sputniks;

        public GameObject Sputnik;

        public Transform EarthCenter;

        private SatellitedSelectEvent _sputnikSelectedEvent = new SatellitedSelectEvent();

        private DataObjectsProvider _dataObjectsProvider;

        private void Start()
        {
            int c = 0;
            var gsProvider = new DataObjectsProvider();
            var groundStations = gsProvider.GetSatellites();

            foreach (var gs in groundStations)
            {
                var gsInstance = Instantiate(Sputnik);
                gsInstance.name = gs.ObjectId;
                //var geoObject = gsInstance.AddComponent<GeoObject>();
                //geoObject.SetCoordinates(gs.po.Latitude, gs.Location.Longitude);
                var satellite = gsInstance.AddComponent<SatelliteObject>();
                satellite.Init(gs, EarthCenter);
                satellite.OnStationSelected(OnSatelliteSelected);

                c++;

                if (c > 100) {
                    break;
                }

                //LaunchSputnik(gs);
            }
        }

        private void OnSatelliteSelected(Satellite satellite)
        {
            _sputnikSelectedEvent.Invoke(satellite);
        }

        public void SatelliteSelected(UnityAction<Satellite> handler)
        {
            _sputnikSelectedEvent.AddListener(handler);
        }

        //private void LaunchSputnik(Satellite satellite)
        //{
        //    var sputnik = Instantiate(Sputnik);

        //    var sputnikController = sputnik.GetComponent<SatelliteObject>();

        //    sputnikController.Init(satellite, EarthCenter);
        //    sputnikController.OnStationSelected(_sputnikSelectedEvent.Invoke);
        //}
    }
}
