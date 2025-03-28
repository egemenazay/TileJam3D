namespace _TileJam.Scripts.ViewScripts
{
    public class LevelFailView : BaseView
    {
        public override void Start()
        {
            GameManager.Instance.OnLevelFail += OnOpen;
        }

        protected void OnDestroy()
        {
            GameManager.Instance.OnLevelFail -= OnOpen;
        }

    }
}
