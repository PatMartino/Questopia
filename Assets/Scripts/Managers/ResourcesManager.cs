using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ResourcesManager : MonoBehaviour
    {
        #region Private Field

        private int _wood;
        private int _timber;

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            SubscribeEvents();
        }

        #endregion

        #region Functions

        private void SubscribeEvents()
        {
            ResourcesSignals.Instance.OnIncreaseWood += OnIncreaseWood;
            ResourcesSignals.Instance.OnGetWood += OnGetWood;
            ResourcesSignals.Instance.OnDecreaseWood += OnDecreaseWood;
            ResourcesSignals.Instance.OnExchangeResource += OnExchangeResources;
            ResourcesSignals.Instance.OnGetTimber += OnGetTimber;
            ResourcesSignals.Instance.OnDecreaseTimber += OnDecreaseTimber;
        }

        private void OnIncreaseWood()
        {
            _wood++;
            UISignals.Instance.OnSetWoodText?.Invoke();
        }
        
        private void OnDecreaseWood()
        {
            _wood--;
            UISignals.Instance.OnSetWoodText?.Invoke();
        }
        
        private void OnDecreaseTimber()
        {
            _timber--;
            UISignals.Instance.OnSetTimberText?.Invoke();
        }

        private int OnGetWood()
        {
            return _wood;
        }

        private int OnGetTimber()
        {
            return _timber;
        }

        private void OnExchangeResources(ResourceType gType, ResourceType rType, int gAmount, int rAmount)
        {
            switch (gType)
            {
                case ResourceType.Wood:
                    _wood -= gAmount;
                    UISignals.Instance.OnSetWoodText?.Invoke();
                    break;
            }

            switch (rType)
            {
                case ResourceType.Timber:
                    _timber += rAmount;
                    UISignals.Instance.OnSetTimberText?.Invoke();
                    break;
            }
        }

        #endregion
    }
}
