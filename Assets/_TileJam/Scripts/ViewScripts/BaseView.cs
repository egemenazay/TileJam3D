using UnityEngine;

namespace _TileJam.Scripts.ViewScripts
{
    public abstract class BaseView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Canvas viewCanvas;
        
        public virtual void Start()
        {
        }

        protected virtual void OnOpen()
        {
        }
    }
}
