using System;
using System.Collections;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


namespace Assets.Scripts
{
    public class SatellitsController : MonoBehaviour
    {

        [SerializeField] private float _speed;

        [SerializeField] private float _radius = 1.5f;

        //public SatellitedSelectEvent OnSputnikClickedEvent = new IntegerEvent();

        private Transform _earth;
        private int _id;
        private bool _launched;
        private float _angle;

        private void Awake()
        {
            var renderer = GetComponent<Renderer>();
            GetComponent<Interactable>().OnClick.AddListener(OnSputnikClicked);
        }

        public void Init(int id, Transform earthTransform)
        {
            _earth = earthTransform ?? throw new ArgumentNullException(nameof(earthTransform));
            _id = id;

            _launched = true;
        }
        
        void Update()
        {
            if (_launched) {
                _angle += Time.deltaTime;

                var x = Mathf.Cos(_angle * _speed) * _radius;
                var z = Mathf.Sin(_angle * _speed) * _radius;
                transform.position = new Vector3(x, 0, z) + new Vector3(_earth.position.x, _earth.position.y, _earth.position.z);
            }
        }

        private void OnSputnikClicked()
        {
            //OnSputnikClickedEvent.Invoke(_id);
        }
    }
}
