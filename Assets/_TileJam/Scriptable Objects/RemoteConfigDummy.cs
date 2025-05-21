using UnityEngine;
using System.Linq;

namespace _TileJam.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "RemoteConfigDummy", menuName =("ScriptableObjects/RemoteConfigDummy"))]
    public class RemoteConfigDummy : ScriptableObject
    {
        public float loadingDuration;
        public string levels;
        public string levelTimers;
        
        public int[] GetParsedLevels()
        {
            return ParseStringToIntArray(levels);
        }

        public int[] GetParsedLevelTimers()
        {
            return ParseStringToIntArray(levelTimers);
        }

        private int[] ParseStringToIntArray(string input)
        {
            return input.Split(',')
                .Select(s => {
                    int.TryParse(s.Trim(), out int result);
                    return result;
                })
                .ToArray();
        }
    }
}
