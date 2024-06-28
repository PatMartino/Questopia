using Enums;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class CameraSignals: MonoSingleton<CameraSignals>
    {
        public UnityAction<CollectibleToolsTypes> OnCameraManagement = delegate {  };    
        public UnityAction OnCameraDestroyer = delegate {  };
    }
}