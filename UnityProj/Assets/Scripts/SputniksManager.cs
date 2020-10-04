using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts
{
    public class SputniksManager : MonoBehaviour
    {
        public List<Sputnik> Sputniks;

        public GameObject Sputnik;

        public Transform EarthCenter;

        private IntegerEvent _sputnikSelectedEvent = new IntegerEvent();

        private void Awake()
        {
            //InvokeRepeating(nameof(LaunchSputnik), 0f, 10f);
            LaunchSputnik();
        }

        public void SputnikSelected(UnityAction<int> handler)
        {
            _sputnikSelectedEvent.AddListener(handler);
        }

        private void LaunchSputnik()
        {
            var sputnik = Instantiate(Sputnik);

            var sputnikController = sputnik.GetComponent<SputnikController>();

            sputnikController.Init(1, EarthCenter);
            sputnikController.OnSputnikClickedEvent.AddListener(id => _sputnikSelectedEvent.Invoke(id));
        }
    }
}
