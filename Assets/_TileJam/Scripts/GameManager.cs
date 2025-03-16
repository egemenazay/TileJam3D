using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (currentGameState == GameState.Gameplay)
            {
                OnLevelComplete?.Invoke();
                ChangeGameState(GameState.LevelComplete);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentGameState == GameState.Gameplay)
            {
                OnLevelFail?.Invoke();
                ChangeGameState(GameState.LevelFail);
                SetLevelFail(LevelFailType.DebugType);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OnLevelRestart?.Invoke();
            Debug.Log("Level Restarted");
        }
    }

#endif
    
    private void SetLevelFail(LevelFailType levelFailType)
    {
        currentLevelFailType = levelFailType;
    }
    public void ChangeGameState(GameState newGameState)
    {
        previousGameState = currentGameState;
        currentGameState = newGameState;
    }
}
