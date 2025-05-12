using _TileJam.Scripts.ManagerScripts;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace _TileJam.Scripts.ViewScripts
{
    public class LoadingView : BaseView
    {
        [SerializeField] private Image sliderBarImage;
        
        [Header("Parameters")] //add duration and ease here
        [SerializeField] private float duration;

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
                sliderBarImage.DOFillAmount(1f, duration)
                    .SetEase(Ease.OutSine)
                    .OnComplete(OnLoadComplete);
            }
        }

        private void OnLoadComplete()
        {
            LevelManager.Instance.LoadCurrentScene();
            OnClose();
        }
    }
}
