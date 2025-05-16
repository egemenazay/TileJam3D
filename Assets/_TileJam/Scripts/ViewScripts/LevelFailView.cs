using System.Collections.Generic;
using _TileJam.Scripts.ManagerScripts;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _TileJam.Scripts.ViewScripts
{
    public class LevelFailView : BaseView
    {
        [Header ("References")]
        [SerializeField] private Button restartButton;
        
        [SerializeField] private RectTransform bannerRectTransform;
        [SerializeField] private RectTransform buttonRectTransform;
        [SerializeField] private RectTransform starOneRectTransform;
        [SerializeField] private RectTransform starTwoRectTransform;
        [SerializeField] private RectTransform starThreeRectTransform;
        [SerializeField] private TMP_Text levelFailText;
        private Dictionary<LevelFailType, string> levelFailTypeList = new Dictionary<LevelFailType, string>();

        private void Awake()
        {
            AddFailTypesToList();
            
        }
        
        public override void Start()
        {
            restartButton.onClick.AddListener(OnRestartButton);
        }
        protected void OnDestroy()
        {
            restartButton.onClick.RemoveAllListeners();
        }
        public override bool OnOpen(int sortOrder)
        {
            var baseReturnValue = base.OnOpen(sortOrder);
            if (!baseReturnValue) return false;
            
            Refresh();
            PlayButtonAnimation();
            PlayBannerAnimation();
            PlayStarAnimation();
            SetLevelFailType();
            SoundManager.Instance.PlaySound(SoundType.LevelFailSound);
            return true;
        }
        private void Refresh()
        {
            bannerRectTransform.localScale = new Vector3(0f,2f,1f);
            buttonRectTransform.localScale = new Vector3(0f,0f,1f);
            //StarScale
            starOneRectTransform.localScale = new Vector3(0f,0f,1f);
            starTwoRectTransform.localScale = new Vector3(0f,0f,1f);
            starThreeRectTransform.localScale = new Vector3(0f,0f,1f);
            //StarPosition
            starOneRectTransform.anchoredPosition = new Vector2(0f, -200f);
            starTwoRectTransform.anchoredPosition = new Vector2(0f, -200f);
            starThreeRectTransform.anchoredPosition = new Vector2(0f, -200f);
        }
        private void PlayBannerAnimation()
        {
            bannerRectTransform.DOScaleX(2f, 1f);
        }

        private void PlayButtonAnimation()
        {
            buttonRectTransform.DOScale(new Vector3(2f, 2f, 1f), 0.4f);
        }
        private void PlayStarAnimation()
        {
            //Scale
            starOneRectTransform.DOScale(new Vector3(2f, 2f, 1f), 0.5f);
            starTwoRectTransform.DOScale(new Vector3(2f, 2f, 1f), 0.8f);
            starThreeRectTransform.DOScale(new Vector3(2f, 2f, 1f), 1f);
            //Position
            starOneRectTransform.DOAnchorPos(new Vector2(-200f, 250f), 0.5f);
            starTwoRectTransform.DOAnchorPos(new Vector2(0f, 300f), 0.8f);
            starThreeRectTransform.DOAnchorPos(new Vector2(200f, 250f), 1f);
        }

        private void OnRestartButton()
        {
            OnClose();
            LevelManager.Instance.LoadCurrentScene();
        }
        private void SetLevelFailType()
        {
            levelFailText.text = levelFailTypeList[GameManager.Instance.currentLevelFailType];
        }
        private void AddFailTypesToList()
        {
            levelFailTypeList.Add(LevelFailType.DebugType,"Debug Fail!");
            levelFailTypeList.Add(LevelFailType.DeadEnd,"Dead End!");
            levelFailTypeList.Add(LevelFailType.TimeOut,"Time Out!");
            levelFailTypeList.Add(LevelFailType.OutOfArea,"Out of Area");
        }
    }
}
