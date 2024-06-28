using Signals;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResourcesTexts : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private TextMeshProUGUI woodText;
        [SerializeField] private TextMeshProUGUI timberText;

        #endregion

        #region OnEnable, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            UISignals.Instance.OnSetWoodText += OnSetWoodText;
            UISignals.Instance.OnSetTimberText += OnSetTimberText;
        }

        private void OnSetWoodText()
        {
            woodText.text = ResourcesSignals.Instance.OnGetWood?.Invoke().ToString();
        }
        
        private void OnSetTimberText()
        {
            timberText.transform.parent.gameObject.SetActive(true);
            timberText.text = ResourcesSignals.Instance.OnGetTimber?.Invoke().ToString();
        }

        #endregion
    }
}