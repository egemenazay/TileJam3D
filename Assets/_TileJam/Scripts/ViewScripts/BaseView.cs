using System;
using UnityEngine;
using DG.Tweening;

namespace _TileJam.Scripts.ViewScripts
{
    public abstract class BaseView : MonoBehaviour
    {
        [Header("References")]
        //these objects going to have animations
        //animations play when event is fired up
        //there is gonna be a method on childs of these baseview where they are gonna subscribe to levelcomplete/fail events
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform textRectTransform;
        [SerializeField] private RectTransform buttonRectTransform;
        [SerializeField] private RectTransform starOneRectTransform;
        [SerializeField] private RectTransform starTwoRectTransform;
        [SerializeField] private RectTransform starThreeRectTransform;
        public virtual void Start()
        {
        }

        protected void OnOpen()
        {
            Refresh();
            PlayButtonAnimation();
            PlayTextAnimation();
            PlayStarAnimation();
        }

        private void Refresh()
        {
            textRectTransform.localScale = new Vector3(0f,2f,1f);
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

        private void PlayTextAnimation()
        {
            textRectTransform.DOScaleX(2f, 1f);
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
    }
}
