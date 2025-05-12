using UnityEngine;

namespace _TileJam.Scripts.ViewScripts
{
    public abstract class BaseView : MonoBehaviour
    {
        [Header("References")]
        public Canvas viewCanvas;
        
        [Header("Info - DO NOT CHANGE")]
        [SerializeField] private bool isOpen;
        
        public virtual void Start()
        {
        }

        public virtual bool OnOpen(int sortOrder)
        {
            if (isOpen) return false;
            
            viewCanvas.enabled = true;
            viewCanvas.sortingOrder = sortOrder;
            isOpen = true;
            return true;
        }

        public void OnClose()
        {
            if (isOpen)
            {
                viewCanvas.enabled = false;
                isOpen = false;
            }
        }
    }
}
