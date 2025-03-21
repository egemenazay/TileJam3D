using System;
using UnityEngine;
using DG.Tweening;

namespace _TileJam.Scripts.ViewScripts
{
    public abstract class BaseView : MonoBehaviour
    {
        [Header("References")] 
        // canvas tut
        //these objects going to have animations
        //animations play when event is fired up
        //there is gonna be a method on childs of these baseview where they are gonna subscribe to levelcomplete/fail events
        [SerializeField] private RectTransform textRectTransform;
        [SerializeField] private RectTransform buttonRectTransform;
        [SerializeField] private GameObject stars;

        public virtual void Start()
        {
            textRectTransform.localScale = new Vector3(0f,2f,1f); //bunu refresh methoduna taşı
        }

        protected virtual void OnDestroy()
        {
            
        }

        //open methodu
        
        //close methodu
        
        //refresh methodu (open'da çağır, animasyonlardan önce kendini sıfırlasın)

        protected void TextAnimation()
        {
            textRectTransform.localScale = new Vector3(0f,2f,1f);
            textRectTransform.DOScaleX(2f, 1f);
        }

        protected void ButtonAnimation()
        {
            buttonRectTransform.localScale = new Vector3(0f,0f,1f);
            buttonRectTransform.DOScale(new Vector3(2f, 2f, 1f), 0.4f);
        }
    }
}
