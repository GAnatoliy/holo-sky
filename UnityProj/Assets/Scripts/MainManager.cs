using UnityEngine;


namespace Assets.Scripts
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _infoCardPrefab;

        private SputniksManager _sputnikManager;

        // Start is called before the first frame update
        void Start()
        {
            _sputnikManager = GetComponent<SputniksManager>();
            _sputnikManager.SputnikSelected(CreateInfoCard);
        }

        private void CreateInfoCard(int sputnikId)
        {
            Debug.Log($"Click to sputnik, id:{sputnikId}");
        }
    }
}
