using System;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerHealthController : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private int health = 100;

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
            CoreGameSignals.Instance.OnTakeDamage += OnTakeDamage;
            CoreGameSignals.Instance.OnSetHealthToMax += OnSetHealthToMax;
        }

        private void OnTakeDamage(int aD)
        {
            health -= aD;
            Debug.Log(health);
            UISignals.Instance.OnReducePlayerHealthUI?.Invoke(health);
        }

        private void OnSetHealthToMax()
        {
            health = 100;
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnTakeDamage -= OnTakeDamage;
            CoreGameSignals.Instance.OnSetHealthToMax -= OnSetHealthToMax;
        }

        #endregion
        
    }
}
