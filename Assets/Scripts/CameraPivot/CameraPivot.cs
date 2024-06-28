using UnityEngine;

namespace CameraPivot
{
    public class CameraPivot : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private float smoothTime = 0.3f;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Vector3 _offset; 
        
        #endregion

        #region Private Field

        private Vector3 _velocity = Vector3.zero;

        #endregion

        #region LateUpdate

        private void LateUpdate()
        {
            FollowPlayer();
        }

        #endregion

        #region Functions

        private void FollowPlayer()
        {
            Vector3 targetPosition = playerTransform.position + _offset;
            
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }

        #endregion


    }
}
