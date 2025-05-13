using System;
using UnityEngine;

namespace _TileJam.Scripts.ManagerScripts
{
    public enum GameState
    {
        Gameplay,
        LevelComplete,
        LevelFail,
        UI
    }
    public enum LevelFailType
    {
        TimeOut,
        OutOfArea,
        DeadEnd,
        DebugType
    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
    
        public event Action OnLevelComplete;
        public event Action OnLevelFail;
        public event Action OnLevelRestart;
    
        [Header("Info - Do not change")]
        [SerializeField] private GameState currentGameState;
        [SerializeField] private GameState previousGameState;
        public GameState CurrentGameState => currentGameState;
        public GameState PreviousGameState => previousGameState;
    
        public LevelFailType currentLevelFailType;
    
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            currentGameState = GameState.Gameplay;
        }

#if  UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                CompleteLevel();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                FailLevel(LevelFailType.DebugType);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                FailLevel(LevelFailType.TimeOut);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
               RestartLevel();
            }
        }

#endif
        public void CompleteLevel()
        {
            if (currentGameState != GameState.Gameplay) return;
            
            ChangeGameState(GameState.LevelComplete);
            OnLevelComplete?.Invoke();
        }

        public void FailLevel(LevelFailType levelFailType)
        {
            if (currentGameState != GameState.Gameplay) return;
            
            ChangeGameState(GameState.LevelFail);
            currentLevelFailType = levelFailType;
            OnLevelFail?.Invoke();
        }

        public void RestartLevel()
        {
            OnLevelRestart?.Invoke();
        }

        public void ChangeGameState(GameState newGameState)
        {
            previousGameState = currentGameState;
            currentGameState = newGameState;
        }
        
        
    }
}