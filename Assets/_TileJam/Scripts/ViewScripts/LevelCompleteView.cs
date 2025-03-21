namespace _TileJam.Scripts.ViewScripts
{
    public class LevelCompleteView : BaseView
    {
        public override void Start()
        {
            GameManager.Instance.OnLevelComplete += OnLevelComplete; //open'a abone ol
        }

        protected override void OnDestroy()
        {
            GameManager.Instance.OnLevelComplete -= OnLevelComplete;
        }

        private void OnLevelComplete()  //open olacak
        {
            TextAnimation();
            ButtonAnimation();
        }
    }
}