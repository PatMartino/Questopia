using Enums;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction OnSetWoodText = delegate {  };
        public UnityAction OnSetTimberText = delegate {  };
        public UnityAction<UITypes> OnUIManagement = delegate {  }; 
        public UnityAction<int> OnReducePlayerHealthUI = delegate {  };

    }
}