namespace _TileJam.Scripts.ViewScripts
{
    public class LevelCompleteView : BaseView
    {
        public override void Start()
        {
            GameManager.Instance.OnLevelComplete += OnLevelComplete;
        }

        private void OnLevelComplete()
        {
            TextAnimation();
            ButtonAnimation();
        }
    }
}