using UnityEngine;
using Utility.GameFlow;

namespace Utility.Audio
{
    public class SoundCaster : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private string audioCode;
        
        [Header("Custom Parameters")]
        [SerializeField] private AudioSource customAudioSource;

        public void CastSingle()
        {
            ClipData clipData = AudioManager.GetClipData(audioCode);
            
            if(clipData == null) return;
            
            if(customAudioSource) AudioManager.PlayClipOneShotCustom(clipData, customAudioSource);
            else AudioManager.PlayClipOneShot(clipData);
        }

        public void CastLoop()
        {
            ClipData clipData = AudioManager.GetClipData(audioCode);
            
            if(clipData == null) return;
            
            if(customAudioSource) AudioManager.PlayClipCustom(clipData, customAudioSource);
            else AudioManager.PlayClip(clipData);
        }
    }
}
