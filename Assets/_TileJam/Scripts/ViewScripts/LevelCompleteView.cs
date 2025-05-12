using _TileJam.Scripts.ManagerScripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace _TileJam.Scripts.ViewScripts
{
    public class LevelCompleteView : BaseView
    {
        [Header ("References")]
        [SerializeField] private Button continueButton;
        
        [SerializeField] private RectTransform bannerRectTransform;
        [SerializeField] private RectTransform buttonRectTransform;
        [SerializeField] private RectTransform starOneRectTransform;
        [SerializeField] private RectTransform starTwoRectTransform;
        [SerializeField] private RectTransform starThreeRectTransform;
        public override void Start()
        {
            continueButton.onClick.AddListener(OnContinueButton);
        }
        protected void OnDestroy()
        {
            continueButton.onClick.RemoveAllListeners();
        }
        public override bool OnOpen(int sortOrder)
        {
            var baseReturnValue = base.OnOpen(sortOrder);
            if (!baseReturnValue) return false;
            
            Refresh();
            PlayButtonAnimation();
            PlayBannerAnimation();
            PlayStarAnimation();

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
        private void OnContinueButton()
        {
            OnClose();
            LevelManager.Instance.LoadCurrentScene();
        }
    }
}