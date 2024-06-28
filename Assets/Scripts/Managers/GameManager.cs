using UnityEngine;
using Enums;
using Signals;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Serialized Field

        [SerializeField] private GameStates states;

        #endregion
        
        #region OnEnable, OnDisable
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Private Functions
        
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.OnChangeGameState += OnChangeGameState;
            CoreGameSignals.Instance.OnPausingGame += OnPausingGame;
            CoreGameSignals.Instance.OnResumingGame += OnResumingGame;
            CoreGameSignals.Instance.OnGetGameState += OnGetGameState;
            
        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.OnChangeGameState -= OnChangeGameState;
            CoreGameSignals.Instance.OnPausingGame -= OnPausingGame;
            CoreGameSignals.Instance.OnResumingGame -= OnResumingGame;
            CoreGameSignals.Instance.OnGetGameState -= OnGetGameState;
            
        }
        private void OnPausingGame()
        {
            OnChangeGameState(GameStates.Pause);
            Time.timeScale = 0;
            Debug.Log("Pause");
        }
        private void OnResumingGame()
        {
            OnChangeGameState(GameStates.Game);
            Time.timeScale = 1;
            Debug.Log("Game");
        }
        
        private void OnChangeGameState(GameStates state)
        {
            states = state;
        }
        
        private GameStates OnGetGameState()
        {
            return states;
        }

        #endregion
    }
}
