using System;
using Enums;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction OnHaveAxe = delegate {  };
        public UnityAction OnPausingGame = delegate {  };
        public UnityAction OnResumingGame = delegate {  };
        public UnityAction<GameStates> OnChangeGameState = delegate {  };
        public Func<GameStates> OnGetGameState = () => GameStates.Game;
        public UnityAction OnHaveSword = delegate {  };
        public UnityAction<int> OnTakeDamage = delegate {  };
        public UnityAction OnSetHealthToMax = delegate {  };
    }
}