using UnityEngine;

namespace UI
{
    public class ResourcesUIRotation : MonoBehaviour
    {
        private Camera _camera;
    
        private void Awake()
        {
            _camera =Camera.main;
        }

        private void Update()
        {
            transform.rotation = _camera.transform.rotation;
        }
    }
}
