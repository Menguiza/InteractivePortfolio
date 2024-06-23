using System;
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
        public static Action OnMoveOn;
        public static Action OnBackUp;

        public static Action<bool> Pause;

        public delegate void StateChange(GameStates newState);
        public static event StateChange OnStateChanged;

        [Header("Parameters")]
        [SerializeField] private GameStates initialState;

        public GameStates CurrentState { get; private set; }

        private void Awake()
        {
            OnMoveOn += NextState;
            OnBackUp += LastState;

            CurrentState = GameStates.Unload;
        }

        private void Start()
        {
            ChangeState(initialState);
            
            AudioManager.PlayClip(AudioManager.GetClipData("Theme"));
        }

        private void ChangeState(GameStates newState)
        {
            if(CurrentState == newState) return;
            
            CurrentState = newState;
            
            OnStateChanged?.Invoke(CurrentState);
        }

        private void ChangeState(int stateIndex)
        {
            if (Enum.IsDefined(typeof(GameStates), stateIndex))
            {
                ChangeState((GameStates)stateIndex);
            }
            else
            {
                ChangeState(default(GameStates));
            }
        }

        private void NextState()
        {
            ChangeState((int)CurrentState + 1);
        }

        private void LastState()
        {
            ChangeState((int)CurrentState - 1);
        }
    }
}
