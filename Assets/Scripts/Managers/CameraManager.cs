using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private Transform cameraHolder;

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
            CameraSignals.Instance.OnCameraManagement += OnCameraManagement;
            CameraSignals.Instance.OnCameraDestroyer += OnCameraDestroyer;
        }

        private void OnCameraManagement(CollectibleToolsTypes type)
        {
            switch (type)
            {
                case CollectibleToolsTypes.Axe:
                    Instantiate(Resources.Load<GameObject>("Camera/AxeCamera"), cameraHolder, false);
                    break;
                case CollectibleToolsTypes.Sword:
                    Instantiate(Resources.Load<GameObject>("Camera/SwordCamera"), cameraHolder, false);
                    break;
            }
        }

        private void OnCameraDestroyer()
        {
            Destroy(cameraHolder.GetChild(0).gameObject);
        }
        
        private void UnSubscribeEvents()
        {
            CameraSignals.Instance.OnCameraManagement -= OnCameraManagement;
            CameraSignals.Instance.OnCameraDestroyer -= OnCameraDestroyer;
        }

        #endregion
        
    }
}
