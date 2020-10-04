using Assets.Core.Scripts;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class SatelliteObject : MonoBehaviour
    {
        private Satellite _model;

        private SatellitedSelectEvent _satelliteSelectedEvent = new SatellitedSelectEvent();

        private bool _launched;
        private float _angle;
        private Transform _earth;

        [SerializeField] private float _speed = 0.2f;

        [SerializeField] private float _radius = 50f;

        private void Start()
        {
            GetComponent<Interactable>().OnClick.AddListener(StationSelected);
        }

        public void Init(Satellite model, Transform earthTransform)
        {
            _model = model;
            _earth = earthTransform;

            _launched = true;
        }

        public void OnStationSelected(UnityAction<Satellite> handler)
        {
            _satelliteSelectedEvent.AddListener(handler);
        }

        private void StationSelected()
        {
            _satelliteSelectedEvent.Invoke(_model);
        }

        void Update()
        {
            if (_launched)
            {
                _angle += Time.deltaTime;

                var x = Mathf.Cos(_angle * _speed) * _radius;
                var z = Mathf.Sin(_angle * _speed) * _radius;
                transform.position = new Vector3(x, 0, z) + new Vector3(_earth.position.x, _earth.position.y, _earth.position.z);
            }
        }
    }
}