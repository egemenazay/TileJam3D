using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using _TileJam.Scripts.KeyScripts;


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
        [Header("--Views--")]
        [SerializeField] private Canvas levelCompleteView;
        [SerializeField] private Canvas levelFailView;
        [SerializeField] private Canvas gameplayView;
        [SerializeField] private Canvas loadingView;
        [Header("Info")]
        [SerializeField] private int currentLevelIndex;
        private Scene scene;
    
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
            scene = SceneManager.GetActiveScene();
            currentLevelIndex = scene.buildIndex;
            if (currentLevelIndex == 0)   //This "if" used when game starts normally from loading scene
            {
                if (PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex) == 0)  //when game first launched if 0 level saved this "if" starts game at level1
                {
                    currentLevelIndex++;
                    PlayerPrefs.SetInt(PlayerPrefKeys.LevelIndex ,currentLevelIndex);
                }
                currentLevelIndex = PlayerPrefs.GetInt(PlayerPrefKeys.LevelIndex);
                StartCoroutine(LoadSceneCoroutine(currentLevelIndex));   
            }
            GameManager.Instance.OnLevelComplete += () => OnLoadView(ViewType.LevelComplete);
            GameManager.Instance.OnLevelFail += () => OnLoadView(ViewType.LevelFail);
        }
    
    
        private void OnSceneLoaded(Scene sceneRef, LoadSceneMode mode)
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
        
        private IEnumerator LoadSceneCoroutine(int sceneBuildIndex) //this goes to loadingView script
        {
            if (scene.buildIndex != 0)
            {
                SceneManager.LoadScene(SceneKeys.LoadScene);
            }
            yield return new WaitForSeconds(1f);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
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