using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Objects
{
    public class CollectibleTools : MonoBehaviour
    {
        [SerializeField] private float cycleLength = 1f;
        [SerializeField] private CollectibleToolsTypes type;
        [SerializeField] private Vector3 moveVector;
        private void Start()
        {
            transform.DOLocalMove(moveVector, cycleLength).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (type == CollectibleToolsTypes.Axe)
                {
                    CoreGameSignals.Instance.OnHaveAxe?.Invoke();
                    UISignals.Instance.OnUIManagement?.Invoke(UITypes.AxeUI);
                    CameraSignals.Instance.OnCameraManagement?.Invoke(CollectibleToolsTypes.Axe);
                    CoreGameSignals.Instance.OnPausingGame?.Invoke();
                    Destroy(gameObject);
                }

                if (type == CollectibleToolsTypes.Sword)
                {
                    CoreGameSignals.Instance.OnHaveSword?.Invoke();
                    UISignals.Instance.OnUIManagement?.Invoke(UITypes.SwordUI);
                    CameraSignals.Instance.OnCameraManagement?.Invoke(CollectibleToolsTypes.Sword);
                    CoreGameSignals.Instance.OnPausingGame?.Invoke();
                    Destroy(gameObject);
                }
                
            }
        }
    }
}
