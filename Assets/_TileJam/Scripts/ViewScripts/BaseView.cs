using UnityEngine;

namespace _TileJam.Scripts.ViewScripts
{
    public abstract class BaseView : MonoBehaviour
    {
        [Header("References")]
        public Canvas viewCanvas;
        
        public virtual void Start()
        {
        }

        protected virtual void OnOpen()
        {
        }
    }
}
