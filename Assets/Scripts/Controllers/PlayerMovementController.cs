using Enums;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private Joystick joystick;
        [SerializeField] private float speed;
        [SerializeField] private float rotationSpeed = 0.5f;
        [SerializeField] private float resourcesCooldown = 0.2f;
        

        #endregion

        #region Private Field

        private float _horizontalMovement;
        private float _verticalMovement;
        private Vector3 _movementDirection; 
        private const int _rotationAngle = 45;

        #endregion

        #region OnEnable, OnDisable Update
        
        private void OnEnable()
        {

            ResourcesSignals.Instance.OnGetResourcesCooldown += OnGetResourceCooldown;
        }

        private void Update()
        {
           MovementInput();
           Movement();     
           
        }

        private void OnDisable()
        {
            ResourcesSignals.Instance.OnGetResourcesCooldown -= OnGetResourceCooldown;
        }

        #endregion

        #region Functions


        private void MovementInput()
        {
            if (CoreGameSignals.Instance.OnGetGameState?.Invoke() == GameStates.Game)
            {
                _horizontalMovement = joystick.Horizontal;
                _verticalMovement = joystick.Vertical;
            }
            else
            {
                _horizontalMovement = 0f;
                _verticalMovement = 0f;
                _movementDirection = Vector3.zero;
            }
            
        }

        private void Movement()
        {
            if (CoreGameSignals.Instance.OnGetGameState?.Invoke() == GameStates.Game)
            {
                _movementDirection = new Vector3(_horizontalMovement, 0f, _verticalMovement);
                Quaternion rotation = Quaternion.Euler(0f, _rotationAngle, 0f);
                _movementDirection = rotation * _movementDirection;
                if (CoreGameSignals.Instance.OnGetGameState?.Invoke() == GameStates.Game)
                {

                    if (_movementDirection != Vector3.zero)
                    {
                        if (_movementDirection.magnitude >= 0.1f)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
                            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                                Time.fixedDeltaTime * rotationSpeed);
                            AnimationSignals.Instance.OnPlayingPlayerAnimations?.Invoke(AnimationStates.Run);
                        }

                    }
                    else
                    {
                        AnimationSignals.Instance.OnPlayingPlayerAnimations?.Invoke(AnimationStates.Idle);
                    }

                    Vector3 velocity = _movementDirection * speed;
                    transform.position += velocity * Time.deltaTime;

                    transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                }
            }
            else
            {
                _movementDirection = Vector3.zero;
            }
        }

        private float OnGetResourceCooldown()
        {
            return resourcesCooldown;
        }

        #endregion
    }
}
