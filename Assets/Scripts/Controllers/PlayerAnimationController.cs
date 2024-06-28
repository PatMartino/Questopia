using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Private Fields

        private Animator _animator;
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int Attack = Animator.StringToHash("Attack");

        #endregion

        #region Awake, OnEnable, OnDisable

        private void Awake()
        {
            Init();
        }

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

        private void Init()
        {
            _animator = GetComponent<Animator>();
        }

        private void SubscribeEvents()
        {
            AnimationSignals.Instance.OnPlayingPlayerAnimations += OnPlayingPlayerAnimations;
        }

        private void OnPlayingPlayerAnimations(AnimationStates state)
        {
            switch (state)
            {
                case AnimationStates.Idle:
                    _animator.SetBool(Run,false);
                    break;
                case AnimationStates.Run:
                    _animator.SetBool(Run, true);
                    break;
                case AnimationStates.Attack:
                    _animator.SetTrigger(Attack);
                    break;
            }
        }
        
        private void UnSubscribeEvents()
        {
            AnimationSignals.Instance.OnPlayingPlayerAnimations += OnPlayingPlayerAnimations;
        }

        #endregion
    }
}
