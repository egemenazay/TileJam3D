using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    //Current ve Previous Game State tutmak.
    //Level Complete ile Level Fail methodlarına ve eventlerine sahip olmak.
    //Game State (enum olmalı)   GameState; Gameplay, LevelComplete, LevelFail, UI

    public static GameManager Instance;
    public event Action OnLevelComplete;
    public event Action OnLevelFail;
    
    public enum GameState
    {
        Gameplay,
        LevelComplete,
        LevelFail,
        UI
    }

    public GameState gameState;
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
        gameState = GameState.Gameplay;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (OnLevelComplete != null && gameState == GameState.Gameplay)
            {
                OnLevelComplete();
                gameState = GameState.LevelComplete;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (OnLevelFail != null && gameState == GameState.Gameplay)
            {
                OnLevelFail();
                gameState = GameState.LevelFail;
            }
        }
        Debug.Log(gameState);
    }
}
