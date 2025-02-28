using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public Action OnLevelComplete;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (OnLevelComplete != null)
            {
                OnLevelComplete();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FailLevel();
        }
    }

    private void CompleteLevel(object sender, EventArgs e)
    {
        Debug.Log("Level Complete");
    }

    private void FailLevel()
    {
        Debug.Log("Level Failed");
    }
}
