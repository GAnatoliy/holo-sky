using Assets.Core.Scripts;
using Assets.Core.Scripts.Dtos;
using Assets.Scripts.InfoCard;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using UnityEngine;


namespace Assets.Scripts
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _groundObjectInfoCardPrefab;

        [SerializeField]
        private GameObject _infoCardPrefab;

        private SatellitsManager satellitManager;
        private GroundStationsManager  _groundStationManager;

        private GameObject _instantedInfoCard;

        private Transform _camera;

        public GameObject Earth;

        // Start is called before the first frame update
        void Start()
        {
            satellitManager = GetComponent<SatellitsManager>();
            satellitManager.SatelliteSelected(CreateInfoCard);

            _groundStationManager = GetComponent<GroundStationsManager>();
            _groundStationManager.OnStationSelected(CreateGroundStationInfoCard);

            _camera = Camera.main.transform;
        }

        private void CreateInfoCard(Satellite model)
        {
            _instantedInfoCard = Instantiate(_infoCardPrefab);
            _instantedInfoCard.transform.position = _camera.position + new Vector3(_camera.forward.x, 0f, _camera.forward.z) * 0.5f;

            var infoCard = _instantedInfoCard.GetComponent<SatelliteInfoCardController>();

            infoCard.Init(model);
            infoCard.OnCloseInfoCard.AddListener(() => {
                satellitManager.UndohilightObject(model.ObjectId);
                Destroy(_instantedInfoCard.gameObject);
            });

            //satellitManager.HilightObject(model.ObjectId);
        }

        private void CreateGroundStationInfoCard(GroundStation model)
        {
            if (string.IsNullOrEmpty(model.ImageUrl) && string.IsNullOrEmpty(model.Description)) {
                return;
            }

            _instantedInfoCard = Instantiate(_groundObjectInfoCardPrefab);
            _instantedInfoCard.transform.position = _camera.position + new Vector3(_camera.forward.x, 0f, _camera.forward.z) * 0.5f;

            var infoCard = _instantedInfoCard.GetComponent<InfoCardController>();

            infoCard.Init(model);
            infoCard.OnCloseInfoCard.AddListener(()=> {
                _groundStationManager.UndohilightObject(model.Id);
                Destroy(_instantedInfoCard.gameObject);
            });

            //_groundStationManager.HilightObject(model.Id);
        }

        public void OnRotationButtonClick()
        {
            var state = !Earth.GetComponent<BoundsControl>().enabled;

            Earth.GetComponent<BoundsControl>().enabled = state;
            Earth.transform.Find("Pivot").GetComponent<BoxCollider>().enabled = state;
        }
    }
}
