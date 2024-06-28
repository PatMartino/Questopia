using System.Collections;
using Signals;
using UI;
using UnityEngine;

namespace Objects
{
    public class SectionCollider : MonoBehaviour
    {
        #region Serialized Field

        public SectionController stageController;

        #endregion

        #region Private Field

        private bool _canGiveResource = true;

        #endregion

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(IncreaseResource());
            }
        }

        private IEnumerator IncreaseResource()
        {
            if (!_canGiveResource) yield break;
            _canGiveResource = false;
            stageController.OnIncreaseResourceAmount.Invoke();
            var waitTime = ResourcesSignals.Instance.OnGetResourcesCooldown.Invoke();
            yield return new WaitForSeconds(waitTime);
            _canGiveResource = true;
        }
    }
}
