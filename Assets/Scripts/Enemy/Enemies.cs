using System;
using System.Collections;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Enemy
{
    public class Enemies : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private int health = 100;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float detectionRange = 10f;
        [SerializeField] private float AttackRange = 2f;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform normalTransform;
        [SerializeField] float knockbackForce = 10f;
        [SerializeField] private Image healthBarSprite;
        [SerializeField] private Image healthBarSprite1;

        #endregion

        #region Private Field

        private NavMeshAgent _agent;
        private bool isChasing;
        private bool _canAttack = true;
        private Rigidbody _rigidBody;
        private bool _attacked;
        private Camera _cam;

        #endregion

        #region Public Field

        public UnityAction<int> OnAttackByPlayer = delegate {  };

        #endregion

        #region Awake

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _rigidBody = GetComponent<Rigidbody>();
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            OnAttackByPlayer += AttackedByPlayer;
        }

        private void Update()
        {
            MoveToPlayer();
            AttackToPlayer();
            HealthBarRotation();
        }
        
        private void OnDisable()
        {
            OnAttackByPlayer -= AttackedByPlayer;
        }

        #endregion

        #region Function

        private void MoveToPlayer()
        {
            
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            if (distanceToPlayer <= detectionRange && !_attacked)
            {
                isChasing = true;
                _agent.SetDestination(playerTransform.position);
                _agent.speed = 4;
            }
            else
            {
                isChasing = false;
                _agent.SetDestination(normalTransform.position);
            }
           
        }

        private void AttackToPlayer()
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            if (distanceToPlayer <= AttackRange && _canAttack )
            {
                _canAttack = false;
                CoreGameSignals.Instance.OnTakeDamage?.Invoke(attackDamage);
                Debug.Log("Attack");
                StartCoroutine(WaitForAttack());
            }
        }

        private IEnumerator WaitForAttack()
        {
            yield return new WaitForSeconds(attackSpeed);
            _canAttack = true;
        }

        private void AttackedByPlayer(int aD)
        {
            _attacked = true;
            isChasing = false;
            _agent.speed = 0;
            //_agent.ResetPath(); 
            StartCoroutine(AttackedByPlayerCoroutine(aD));
        }

        private IEnumerator AttackedByPlayerCoroutine(int aD)
        {
            yield return new WaitForSeconds(0.4f);
            Debug.Log("ATTACKBYPLAYER");
            health -= aD;
            CheckDead();
            healthBarSprite.gameObject.SetActive(true);
            healthBarSprite1.gameObject.SetActive(true);
            healthBarSprite.fillAmount = (float)health / 100;
            _rigidBody.AddForce(-transform.forward * knockbackForce, ForceMode.Impulse);
            Invoke("EnableNavMeshAgent", 0.2f); 
        }

        private void CheckDead()
        {
            if (health<=0)
            {
                Destroy(gameObject);
            }
        }

        private void EnableNavMeshAgent()
        {
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = Vector3.zero;
            isChasing = true;
            _attacked = false;
            _agent.speed = 4;
        }

        private void HealthBarRotation()
        {
            healthBarSprite.transform.rotation = _cam.transform.rotation;
            healthBarSprite1.transform.rotation = _cam.transform.rotation;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }

        #endregion
    }
}
