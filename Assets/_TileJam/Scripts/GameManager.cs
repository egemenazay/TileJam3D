using System;
using UnityEngine;

public enum GameState
{
    PreviousState,
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
    
    private GameState currentGameState;
    public GameState CurrentGameState {get {return currentGameState;} set {currentGameState = value;}}
    
    private GameState previousGameState;
    public GameState PreviousGameState {get {return previousGameState;} set {previousGameState = value;}}

    private LevelFailType currentLevelFailType;
    
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentGameState == GameState.Gameplay)
            {
                OnLevelComplete?.Invoke();
                currentGameState = GameState.LevelComplete;
                previousGameState = GameState.Gameplay;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentGameState == GameState.Gameplay)
            {
                OnLevelFail?.Invoke();
                currentGameState = GameState.LevelFail;
                previousGameState = GameState.Gameplay;
                LevelFail(LevelFailType.DebugType);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OnLevelRestart?.Invoke();
            Debug.Log("Level Restarted");
        }
    }

#endif
    private void LevelFail(LevelFailType levelFailType) //Purpose: Show why game failed in UI
    {
        currentLevelFailType = levelFailType;
        Debug.LogWarning(currentGameState);
    }
}
