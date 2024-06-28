using System.Collections;
using Signals;
using UnityEngine;
using UnityEngine.Events;

namespace Objects
{
    public class ResourcesObjects : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private Transform[] resourcesParts = new Transform[4];

        #endregion
        
        #region Private Field

        private byte _resourcesAmount = 4;
        private Coroutine _regenCoroutine;

        #endregion

        #region Public Field

        public UnityAction OnDecreaseHealth = delegate {  };

        #endregion

        #region OnEnable

        private void OnEnable()
        {
            OnDecreaseHealth += DecreaseResourcesAmount;
        }
        
        private void OnDisable()
        {
            OnDecreaseHealth -= DecreaseResourcesAmount;
        }

        #endregion

        #region Functions

        private void DecreaseResourcesAmount()
        {
            if (_resourcesAmount > 0)
            {
                _resourcesAmount -= 1;
                StartCoroutine(DecreaseResourcesTransform());
                Debug.Log("Health: " + _resourcesAmount);
                if (_regenCoroutine != null)
                {
                    StopCoroutine(_regenCoroutine);
                }
                _regenCoroutine = StartCoroutine(RegenHealth());
            }
        }

        private IEnumerator DecreaseResourcesTransform()
        {
            var treePart = resourcesParts[_resourcesAmount].gameObject;
            yield return new WaitForSeconds(0.5f);
            ResourcesSignals.Instance.OnIncreaseWood?.Invoke();
            treePart.SetActive(false);
            
        }

        public bool CanAttacked()
        {
            if (_resourcesAmount > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private IEnumerator RegenHealth()
        {
            yield return new WaitForSeconds(5f);
            _resourcesAmount = 4;
            for (int i = 0; i < resourcesParts.Length; i++)
            {
                resourcesParts[i].gameObject.SetActive(true);
            }
            _regenCoroutine = null;
        }
        

        #endregion
    }
}
