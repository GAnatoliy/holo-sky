using Assets.Core.Scripts.Dtos;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class GroundStationObject : MonoBehaviour
    {
        private GroundStation _model;

        private GroundStationSelectedEvent _groundStationSelectedEvent = new GroundStationSelectedEvent();

        private Color _defaultColor;

        private void Aweke()
        {
            //_defaultColor = GetComponent<Renderer>().material.color;
            _defaultColor = Color.grey;
        }

        private void Start()
        {
            GetComponent<Interactable>().OnClick.AddListener(StationSelected);
        }

        public void Init(GroundStation model)
        {
            _model = model;
        }

        public void UndohilightObject()
        {
            //GetComponent<Renderer>().material.color = _defaultColor;
        }

        public void HilightObject()
        {
            //GetComponent<Renderer>().material.color = Color.red;
        }

        public void OnStationSelected(UnityAction<GroundStation> handler)
        {
            _groundStationSelectedEvent.AddListener(handler);
        }

        private void StationSelected()
        {
            _groundStationSelectedEvent.Invoke(_model);
        }
    }
}