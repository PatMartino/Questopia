using Enums;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class ResourceExchanger : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private ResourceType givenResourceType;
        [SerializeField] private ResourceType receivedResourceType;
        [SerializeField] private int givenResourceAmount;
        [SerializeField] private int receivedResourceAmount;
        [SerializeField] private GameObject resourceChangerUI;
        [SerializeField] private GameObject swapButton;
        [SerializeField] private Image givenResourceImage;
        [SerializeField] private Image receivedResourceImage;
        [SerializeField] private TextMeshProUGUI givenResourceText;
        [SerializeField] private TextMeshProUGUI receivedResourceText;

        #endregion

        #region Private Field

        private Camera _camera;

        #endregion

        #region Awake, OnEnable

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            resourceChangerUI.transform.rotation = _camera.transform.rotation;
        }

        #endregion

        #region Functions

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Init();
            }
        }

        public void OnResourceExchange()
        {
            ResourcesSignals.Instance.OnExchangeResource?.Invoke(givenResourceType,receivedResourceType,givenResourceAmount,receivedResourceAmount);
        }

        private void Init()
        {
            resourceChangerUI.SetActive(true);
            givenResourceText.text = givenResourceAmount.ToString();
            receivedResourceText.text = receivedResourceAmount.ToString();
            switch (givenResourceType)
            {
                case ResourceType.Wood:
                    givenResourceImage.sprite = Resources.Load<Sprite>("UI/wood");
                    if (ResourcesSignals.Instance.OnGetWood?.Invoke() >= givenResourceAmount)
                    {
                        swapButton.SetActive(true);
                    }
                    else
                    {
                        swapButton.SetActive(false);
                    }
                    break;
                case ResourceType.Timber:
                    givenResourceImage.sprite = Resources.Load<Sprite>("UI/timber");
                    if (ResourcesSignals.Instance.OnGetTimber?.Invoke() >= givenResourceAmount)
                    {
                        swapButton.SetActive(true);
                    }
                    else
                    {
                        swapButton.SetActive(false);
                    }
                    break;
            }

            switch (receivedResourceType)
            {
                case ResourceType.Wood:
                    receivedResourceImage.sprite = Resources.Load<Sprite>("UI/wood");
                    break;
                case ResourceType.Timber:
                    receivedResourceImage.sprite = Resources.Load<Sprite>("UI/timber");
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                resourceChangerUI.SetActive(false);      
            }
        }

        #endregion
    }
}
