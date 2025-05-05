using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

namespace _TileJam.Scripts.ViewScripts
{
    public class LoadingView : BaseView
    {
        [SerializeField] private Image sliderBarImage;

        public override void Start()
        {
            base.Start();
            MoveSlider();
        }
        private void MoveSlider()
        {
            if (viewCanvas.enabled)
            {
                sliderBarImage.fillAmount = 0;
                sliderBarImage.DOFillAmount(1f, 1f);
            }
        }
    }
}
