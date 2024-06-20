using System.Linq;
using UnityEngine;

namespace Utility.Audio
{
    [CreateAssetMenu(fileName = "New AudioClipsBank", menuName = "Audio/AudioClipsBank")]
    public class AudioClipBank : ScriptableObject
    {
        [SerializeField] private ClipData[] clips;

        public ClipData GetClipDataReferenceByCode(string code)
        {
            return clips.FirstOrDefault(clipData => clipData.code == code);
        }
    }
} 
