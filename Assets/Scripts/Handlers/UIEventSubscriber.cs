using UnityEngine;
using UnityEngine.UI;
using Enums;
using Signals;

namespace Handlers
{
    public class UIEventSubscriber : MonoBehaviour
    {
         #region Serialized Field

        [SerializeField] private UIEventSubscriptionTypes type;

        #endregion
        
        #region Private Field

        private Button _button;

        #endregion
        
        #region OnEnable, Start, OnDisable

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        #endregion
        
        #region Private Functions

        private void SubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.AxeClose:
                {
                    _button.onClick.AddListener(() => UISignals.Instance.OnUIManagement?.Invoke(UITypes.InGameUI));
                    _button.onClick.AddListener(() => CameraSignals.Instance.OnCameraDestroyer?.Invoke());
                    _button.onClick.AddListener(() => CoreGameSignals.Instance.OnResumingGame?.Invoke());
                    break;
                }
                
            }
        }

        #endregion
    }
}
