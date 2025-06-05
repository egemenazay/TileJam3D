using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using _TileJam.Scripts.KeyScripts;
using _TileJam.Scripts.UIScripts.ViewScripts;


namespace _TileJam.Scripts.ManagerScripts
{
    public enum ViewType
    {
        LevelComplete,
        LevelFail,
        Gameplay,
        Loading,
        Setting
    }
    public class UIManager : MonoBehaviour
    {
        [Header("References")]
        [Header("--Views--")]
        [SerializeField] private LevelCompleteView levelCompleteView;
        [SerializeField] private LevelFailView levelFailView;
        [SerializeField] private GameplayView gameplayView;
        [SerializeField] private LoadingView loadingView;
        [SerializeField] private SettingView settingView;
        private Scene currentScene; 
        private Dictionary<ViewType, BaseView> viewList = new Dictionary<ViewType, BaseView>();

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
            AddViewsToList();
        }
    
        private void Start()
        {
            currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == SceneKeys.LoadScene)   //This "if" used when game starts normally from loading scene
            {
                OnLoadView(ViewType.Loading,2);
            }
        }
        public void OnLoadView(ViewType type, int sortOrder) //add sort order
        {
            viewList[type].OnOpen(sortOrder);
        }
        private void AddViewsToList()
        {
            viewList.Add(ViewType.Gameplay, gameplayView);
            viewList.Add(ViewType.LevelComplete, levelCompleteView);
            viewList.Add(ViewType.LevelFail, levelFailView);
            viewList.Add(ViewType.Loading, loadingView);
            viewList.Add(ViewType.Setting, settingView);
        }
    }
}