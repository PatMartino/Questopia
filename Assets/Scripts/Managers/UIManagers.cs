using Signals;
using UnityEngine;
using Enums;

namespace Managers
{
    public class UIManagers : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private Transform canvas;

        #endregion

        #region OnEnable, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            UISignals.Instance.OnUIManagement += OnUIManagement;
        }

        private void OnUIManagement(UITypes types)
        {
            
            switch (types)
            {
                case UITypes.InGameUI:
                    UIDestroyer();
                    ActivateInGameUI();
                    break;
                case UITypes.AxeUI:
                    InActivateInGameUI();
                    Instantiate(Resources.Load<GameObject>("UI/AxeUI"), canvas, false);
                    break;
                case UITypes.SwordUI:
                    InActivateInGameUI();
                    Instantiate(Resources.Load<GameObject>("UI/SwordUI"), canvas, false);
                    break;
                
            }
        }

        private void InActivateInGameUI()
        {
            canvas.GetChild(0).gameObject.SetActive(false);
        }
        
        private void ActivateInGameUI()
        {
            canvas.GetChild(0).gameObject.SetActive(true);
        }

        private void UIDestroyer()
        {
            Destroy(canvas.GetChild(1).gameObject);
        }
        
        private void UnSubscribeEvents()
        {
            UISignals.Instance.OnUIManagement -= OnUIManagement;
        }

        

        #endregion
    }
}
