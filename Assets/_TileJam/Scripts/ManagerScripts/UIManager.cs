using System.Collections.Generic;
using _TileJam.Scripts.KeyScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace _TileJam.Scripts.ManagerScripts
{
    public enum ViewType
    {
        LevelComplete,
        LevelFail,
        Gameplay,
        Loading
    }
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Canvas levelCompleteView;
        [SerializeField] private Canvas levelFailView;
        [SerializeField] private Canvas gameplayView;
        [SerializeField] private Canvas loadingView;
    
        private Dictionary<ViewType, Canvas> viewList = new Dictionary<ViewType, Canvas>();

        public static UIManager Instance;
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
            SceneManager.sceneLoaded += OnSceneLoaded;
            AddViewsToList();
        }
    
        private void Start()
        {
            GameManager.Instance.OnLevelComplete += () => OnLoadView(ViewType.LevelComplete);
            GameManager.Instance.OnLevelFail += () => OnLoadView(ViewType.LevelFail);
        }
    
    
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            viewList[ViewType.LevelComplete].enabled = false;
            viewList[ViewType.Loading].enabled = false;
            viewList[ViewType.LevelFail].enabled = false;
            viewList[ViewType.Gameplay].enabled = true;
            Debug.Log("Scene Loaded");
        }

        private void OnLoadView(ViewType type)
        {
            viewList[ViewType.Gameplay].enabled = false;
            switch (type)
            {
                case ViewType.LevelComplete:
                    viewList[ViewType.LevelComplete].enabled = true;
                    break;
                case ViewType.LevelFail:
                    viewList[ViewType.LevelFail].enabled = true;
                    break;
            }
        }
    
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
            GameManager.Instance.OnLevelComplete -= () => OnLoadView(ViewType.LevelComplete);
            // ReSharper disable once EventUnsubscriptionViaAnonymousDelegate
            GameManager.Instance.OnLevelFail -= () => OnLoadView(ViewType.LevelFail);
        }
        
        private void AddViewsToList()
        {
            viewList.Add(ViewType.Gameplay, gameplayView);
            viewList.Add(ViewType.LevelComplete, levelCompleteView);
            viewList.Add(ViewType.LevelFail, levelFailView);
            viewList.Add(ViewType.Loading, loadingView);
        }
    }
}