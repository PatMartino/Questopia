using System.Collections;
using Signals;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHealthBarUI : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private Image healthBarUI;
        [SerializeField] private Image healthBarUI1;

        #endregion

        #region Private Field

        private Camera _camera;
        private Coroutine _coroutine;

        #endregion

        #region Awake, OnEnable Update, OnDisable

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Update()
        {
            HealthBarRotation();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            UISignals.Instance.OnReducePlayerHealthUI += OnReducePlayerHealthUI;
        }

        private void OnReducePlayerHealthUI(int health)
        {
            healthBarUI.gameObject.SetActive(true);
            healthBarUI1.gameObject.SetActive(true);
            healthBarUI.fillAmount = (float)health / 100;
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(InActivatePlayerHealthBar());
        }

        private void HealthBarRotation()
        {
            healthBarUI.transform.rotation = _camera.transform.rotation;
            healthBarUI1.transform.rotation = _camera.transform.rotation;
        }

        private IEnumerator InActivatePlayerHealthBar()
        {
            yield return new WaitForSeconds(4f);
            healthBarUI.gameObject.SetActive(false);
            healthBarUI1.gameObject.SetActive(false);
            CoreGameSignals.Instance.OnSetHealthToMax?.Invoke();
        }
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnReducePlayerHealthUI -= OnReducePlayerHealthUI;
        }

        #endregion
        
    }
}