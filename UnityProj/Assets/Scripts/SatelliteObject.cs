using Assets.Core.Scripts;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class SatelliteObject : MonoBehaviour
    {
        public Satellite _model;

        private SatellitedSelectEvent _satelliteSelectedEvent = new SatellitedSelectEvent();

        private bool _launched;
        private float _angle;

        [SerializeField] private float _speed = 0.2f;

        [SerializeField] private float _radius = 50f;

        private Color _defaultColor;

        private void Aweke()
        {
            _defaultColor = GetComponent<Renderer>().material.color;
        }

        private void Start()
        {
            GetComponent<Interactable>().OnClick.AddListener(StationSelected);
        }

        public void Init(Satellite model)
        {
            _model = model;
        }

        public void OnStationSelected(UnityAction<Satellite> handler)
        {
            _satelliteSelectedEvent.AddListener(handler);
        }

        public void UndohilightObject()
        {
            //GetComponent<Renderer>().material.color = _defaultColor;
        }        
        
        public void HilightObject()
        {
            //GetComponent<Renderer>().material.color = Color.red;
        }

        private void StationSelected()
        {
            _satelliteSelectedEvent.Invoke(_model);
        }
    }
}