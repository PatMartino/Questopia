using DG.Tweening;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SectionController : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private ResourceType type;
        [SerializeField] private int necessaryResourceAmount;
        [SerializeField] private TextMeshProUGUI necessaryResourceAmountText;
        [SerializeField] private Transform image;
        [SerializeField] private TextMeshProUGUI ResourceAmountText;
        [SerializeField] private Transform resourcePoint;
        [SerializeField] private Transform resourcePoint2;
        [SerializeField] private GameObject newSection;

        #endregion

        #region Private Field

        private int _resourceAmount=0;
        

        #endregion

        #region Public Field

        public UnityAction OnIncreaseResourceAmount;

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            Init();
            OnIncreaseResourceAmount += IncreaseResourceAmount;
        }

        #endregion

        #region Functions

        private void Init()
        {
            switch (type)
            {
                case ResourceType.Wood:
                    if (image.GetComponent<Image>() != null)
                    {
                        image.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/wood");
                    }
                    break;
                case ResourceType.Timber:
                    if (image.GetComponent<Image>() != null)
                    {
                        image.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/timber");
                    }
                    break;
                    
            }

            necessaryResourceAmountText.text = necessaryResourceAmount.ToString();
        }

        private void IncreaseResourceAmount()
        {
            switch (type)
            {
                case ResourceType.Wood:
                    if (ResourcesSignals.Instance.OnGetWood?.Invoke()>=1)
                    {
                        _resourceAmount++;
                        ResourcesSignals.Instance.OnDecreaseWood?.Invoke();
                        var wood = Instantiate(Resources.Load<GameObject>("Objects/Wood"), resourcePoint, false);
                        wood.transform.DOMove(new Vector3(resourcePoint2.position.x, resourcePoint2.position.y,
                            resourcePoint2.position.z),0.5f);
                        Destroy(wood.gameObject,1);
                        CheckEnoughResource();
                        ResourceAmountText.text = _resourceAmount.ToString();
                    }
                    break;
                case ResourceType.Timber:
                    if (ResourcesSignals.Instance.OnGetTimber?.Invoke()>=1)
                    {
                        _resourceAmount++;
                        ResourcesSignals.Instance.OnDecreaseTimber?.Invoke();
                        var wood = Instantiate(Resources.Load<GameObject>("Objects/Wood"), resourcePoint, false);
                        wood.transform.DOMove(new Vector3(resourcePoint2.position.x, resourcePoint2.position.y,
                            resourcePoint2.position.z),0.5f);
                        Destroy(wood.gameObject,1);
                        CheckEnoughResource();
                        ResourceAmountText.text = _resourceAmount.ToString();
                    }
                    break;
            }
        }

        private void CheckEnoughResource()
        {
            if (_resourceAmount >= necessaryResourceAmount)
            {
                DestroySectionUI();
                if (newSection != null)
                {
                    ActivateNewSection();
                }
            }
        }

        private void DestroySectionUI()
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }

        private void ActivateNewSection()
        {
            newSection.SetActive(true);
        }

        #endregion
    }
}
