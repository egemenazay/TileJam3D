using UnityEngine;
using DG.Tweening;

namespace _TileJam.Scripts.ViewScripts
{
    public class LevelCompleteView : BaseView
    {
        public override void Start()
        {
            GameManager.Instance.OnLevelComplete += OnOpen; //open'a abone ol
        }

        protected void OnDestroy()
        {
            GameManager.Instance.OnLevelComplete -= OnOpen;
        }
    }
}