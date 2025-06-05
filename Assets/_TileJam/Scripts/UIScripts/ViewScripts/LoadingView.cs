using _TileJam.Scripts.ManagerScripts;
using _TileJam.Scripts.RemoteConfig;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _TileJam.Scripts.UIScripts.ViewScripts
{
    public class LoadingView : BaseView
    {
        [Header("References")] 
        [SerializeField] private RemoteConfigDummy remoteConfig;
        [SerializeField] private Image sliderBarImage;
        [Header("Parameters")] //add duration and ease here
        [SerializeField] private float loadingTime;


        public override void Start()
        {
            loadingTime = remoteConfig.loadingDuration;
        }
        public override bool OnOpen(int sortOrder)
        {
            var baseReturnValue = base.OnOpen(sortOrder);
            if (!baseReturnValue) return false;
            LoadLevel();
            return true;
        }

        private void LoadLevel()
        {
            if (viewCanvas.enabled)
            {
                sliderBarImage.fillAmount = 0;
                sliderBarImage.DOFillAmount(1f, loadingTime)
                    .SetEase(Ease.OutSine)
                    .OnComplete(OnLoadComplete);
            }
        }

        private async void OnLoadComplete()
        {
            LevelManager.Instance.LoadCurrentScene();
            await UniTask.WaitForSeconds(0.1f);
            OnClose();
        }
    }
}
