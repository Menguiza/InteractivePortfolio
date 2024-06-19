using UnityEngine;

namespace Utility.GameFlow
{
    public enum GameStates
    {
        Unload,
        Menu,
        Entrance,
        Map
    }
    
    public class GameManager : MonoBehaviour
    {
        public delegate void StateChange(GameStates newState);
        public static event StateChange OnStateChange;
        
        public GameStates CurrentState { get; private set; }

        private void Awake()
        {
            CurrentState = GameStates.Unload;
        }

        private void Start()
        {
            ChangeState(GameStates.Menu);
        }

        private void ChangeState(GameStates newState)
        {
            if(CurrentState == newState) return;
            
            CurrentState = newState;
            OnStateChange?.Invoke(CurrentState);
        }
    }
}
