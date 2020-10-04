using Assets.Core.Scripts;
using Assets.Core.Scripts.Dtos;
using Assets.Scripts.InfoCard;
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

        // Start is called before the first frame update
        void Start()
        {
            satellitManager = GetComponent<SatellitsManager>();
            satellitManager.SatelliteSelected(CreateInfoCard);

            _groundStationManager = GetComponent<GroundStationsManager>();
            _groundStationManager.OnStationSelected(CreateGroundStationInfoCard);

            _camera = Camera.main.transform;
        }

        private void CreateInfoCard(Satellite sputnikId)
        {
            Debug.Log($"Click to sputnik, id:{sputnikId.ObjectId}");

            _instantedInfoCard = Instantiate(_infoCardPrefab);

            _instantedInfoCard.transform.position = _camera.position + _camera.forward * 5;
        }

        private void CreateGroundStationInfoCard(GroundStation model)
        {
            Debug.Log($"Click to ground statin, id:{model.Id}");

            //if (string.IsNullOrEmpty(model.ImageUrl) && string.IsNullOrEmpty(model.Description)) {
            //    return;
            //}

            _instantedInfoCard = Instantiate(_groundObjectInfoCardPrefab);

            _instantedInfoCard.transform.position = _camera.position + new Vector3(_camera.forward.x, 0f, _camera.forward.z) * 0.5f;

            var infoCard = _instantedInfoCard.GetComponent<InfoCardController>();
            infoCard.Init(model);
            infoCard.OnCloseInfoCard.AddListener(()=> Destroy(_instantedInfoCard.gameObject));
        }
    }
}
