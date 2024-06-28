using System;
using System.Collections;
using Enemy;
using Enums;
using Objects;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerCollectAndAttackController : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange = 0.5f;
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private float attackSpeed = 1f;
        [SerializeField] private int attackDamage = 40;

        #endregion

        #region Private Field

        private bool _canAttack = true;
        private bool _availableTree;
        private bool _haveAxe;
        private bool _haveSword;

        #endregion

        #region OnEnable, Update, OnDisable


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void Update()
        {
            CollectAndAttack();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
        
        #region Functions

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnHaveAxe += OnHaveAxe;
            CoreGameSignals.Instance.OnHaveSword += OnHaveSword;
        }

        private void CollectAndAttack()
        {
            
            if (!_canAttack)
            {
                return;
            }
            Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider enemy in hitObjects)
            {
                if (enemy.CompareTag("CollectibleObjects"))
                {
                    if (enemy.gameObject.GetComponent<ResourcesObjects>().CanAttacked() && _haveAxe)
                    {
                        _availableTree = true;
                    }
                }

                if (enemy.CompareTag("Enemy") && _haveSword)
                {
                    _availableTree = true;
                }
            }

            if (!_availableTree)
            {
                return;
            }
            AnimationSignals.Instance.OnPlayingPlayerAnimations?.Invoke(AnimationStates.Attack);

            _canAttack = false;
            StartCoroutine(WaitForAttack());
            
            foreach (Collider enemy in hitObjects)
            {
                if (enemy.CompareTag("CollectibleObjects") && _haveAxe)
                {
                    enemy.gameObject.GetComponent<ResourcesObjects>().OnDecreaseHealth.Invoke();
                }

                if (enemy.CompareTag("Enemy") && _haveSword)
                {
                    enemy.gameObject.GetComponent<Enemies>().OnAttackByPlayer.Invoke(attackDamage);
                }
                
            }
        }

        private IEnumerator WaitForAttack()
        {
            yield return new WaitForSeconds(attackSpeed);
            _canAttack = true;
            _availableTree = false;
        }

        private void OnHaveAxe()
        {
            _haveAxe = true;
        }
        
        private void OnHaveSword()
        {
            _haveSword = true;
        }
        
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnHaveAxe -= OnHaveAxe;
            CoreGameSignals.Instance.OnHaveSword -= OnHaveSword;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(attackPoint.position,attackRange);
        }

        #endregion
    }
}
