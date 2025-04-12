using UnityEngine;

namespace _TileJam.Scripts.ManagerScripts
{
    public class DonDestroyOnLoadManager : MonoBehaviour
    {
        public static DonDestroyOnLoadManager Instance;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
