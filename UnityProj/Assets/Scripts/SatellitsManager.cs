using Assets.Core.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts
{
    public class SatellitsManager : MonoBehaviour
    {
        public GameObject Sputnik;

        public Transform EarthCenter;

        private SatellitedSelectEvent _sputnikSelectedEvent = new SatellitedSelectEvent();

        private Dictionary<string, SatelliteObject> _statellits = new Dictionary<string, SatelliteObject>();

        private void Start()
        {
            int c = 0;
            var gsProvider = new DataObjectsProvider();
            var groundStations = gsProvider.GetSatellites();

            foreach (var gs in groundStations) {
                var gsInstance = Instantiate(Sputnik);
                gsInstance.name = gs.ObjectId;

                var satellite = gsInstance.AddComponent<SatelliteObject>();
                satellite.Init(gs, EarthCenter);
                satellite.OnStationSelected(OnSatelliteSelected);

                _statellits.Add(gs.ObjectId, satellite);
            }
        }

        public void UndohilightObject(string id)
        {
            _statellits[id]?.UndohilightObject();
        }

        public void HilightObject(string id)
        {
            _statellits[id]?.HilightObject();
        }

        private void OnSatelliteSelected(Satellite satellite)
        {
            _sputnikSelectedEvent.Invoke(satellite);
        }

        public void SatelliteSelected(UnityAction<Satellite> handler)
        {
            _sputnikSelectedEvent.AddListener(handler);
        }
    }
}
