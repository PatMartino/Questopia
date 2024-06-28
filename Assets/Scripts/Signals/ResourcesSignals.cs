using System;
using Enums;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class ResourcesSignals : MonoSingleton<ResourcesSignals>
    {
        public UnityAction OnIncreaseWood = delegate {  }; 
        public Func<int> OnGetWood = () => 0;
        public UnityAction OnDecreaseWood = delegate {  };
        public UnityAction OnDecreaseTimber = delegate {  };
        public Func<float> OnGetResourcesCooldown = () => 0; 
        public UnityAction<ResourceType, ResourceType,int,int> OnExchangeResource = delegate {  };
        public Func<int> OnGetTimber = () => 0;
    }
}