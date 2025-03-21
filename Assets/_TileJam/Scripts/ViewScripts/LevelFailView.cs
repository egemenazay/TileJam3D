namespace _TileJam.Scripts.ViewScripts
{
    public class LevelFailView : BaseView
    {
        public override void Start()
        {
            GameManager.Instance.OnLevelFail += OnLevelFail;
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.OnLevelFail += OnLevelFail;
        }

        private void OnLevelFail()
        {
            PlayTextAnimation();
            PlayButtonAnimation();
        }
    }
}
