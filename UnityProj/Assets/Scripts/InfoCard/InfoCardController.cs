using Assets.Core.Scripts.Dtos;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.InfoCard
{
    public class InfoCardController : MonoBehaviour
    {
        [SerializeField]
        private Transform _content;

        [SerializeField]
        private GameObject _textFiled;

        [SerializeField]
        private GameObject _descriptoin;

        [SerializeField]
        private TextMeshPro _title;

        [SerializeField]
        private Image _image;

        public UnityEvent OnCloseInfoCard = new UnityEvent();

        private GroundStation _model;

        private void Start()
        {
            GetComponent<Billboard>().TargetTransform = Camera.main.transform;
        }

        public void Init(GroundStation model)
        {
            _model = model;

            FillContent(_model);
        }

        private void FillContent(GroundStation model)
        {
            _title.text = model.Name;

            if (!string.IsNullOrEmpty(model.ImageUrl)) {
                StartCoroutine(LoadImage(model.ImageUrl));
            }

            FillDescription(_model.Description);
        }

        private void FillDescription(string description)
        {
            if (!string.IsNullOrEmpty(description)) {
                //var field = Instantiate(_textFiled);

                //field.gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = "Description";
                //field.gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = description;

                //field.transform.SetParent(_content, false);

                _descriptoin.transform.Find("Text").GetComponent<Text>().text = description;
                _descriptoin.gameObject.SetActive(true);
            }
        }

        private IEnumerator LoadImage(string url)
        {
            Texture2D tex;
            tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
            using (WWW www = new WWW(url))
            {
                yield return www;
                www.LoadImageIntoTexture(tex);
                _image.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                _image.gameObject.SetActive(true);
            }
        }

        public void OnCloseButtonClick()
        {
            OnCloseInfoCard.Invoke();
        }
    }
}