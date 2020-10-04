using System;
using System.Collections;
using System.Reflection;
using Assets.Core.Scripts;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.InfoCard
{
    public class SatelliteInfoCardController : MonoBehaviour 
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

        private Satellite _model;

        private void Start()
        {
            GetComponent<Billboard>().TargetTransform = Camera.main.transform;
        }
        
        public void Init(Satellite model)
        {
            _model = model;

            FillContent(_model);
        }

        private void FillContent(Satellite model)
        {
            _title.text = model.ObjectName;

            if (!string.IsNullOrEmpty(model.ImageUrl))
            {
                StartCoroutine(LoadImage(model.ImageUrl));
            }

            FillDescription(_model);
        }

        private void FillDescription(Satellite model)
        {
            var t = model.GetType();
            foreach (var a in t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)) {
                try {
                    var field = Instantiate(_textFiled);

                    field.gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = a.Name;
                    field.gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = a.GetValue(model).ToString();

                    field.transform.SetParent(_content, false);
                } catch (Exception e) {
                    Debug.LogException(e);
                }
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