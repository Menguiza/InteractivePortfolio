using System;
using UnityEngine;
using UnityEngine.Audio;
using Utility.Audio;
using AudioType = Utility.Audio.AudioType;

namespace Utility.GameFlow
{
    public class AudioManager : MonoBehaviour
    {
        //Editor assignable variables
        [Header("References")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioClipBank bank;
        [SerializeField] private AudioSource musicAudioSource, sfxAudioSource;

        //Static variables
        private static Func<string, ClipData> _GetAudioClipHelper;

        private static Action<ClipData> _PlayClipHelper;

        private static Action<ClipData> _PlayClipOneShotHelper;
        
        private static Action<ClipData, AudioSource> _PlayClipCustomHelper;

        private static Action<ClipData, AudioSource> _PlayClipOneShotCustomHelper;
        
        private static Action<string, float> _ChangeVolumeHelper;

        private void Awake()
        {
            //Clip related associations
            _GetAudioClipHelper += bank.GetClipDataReferenceByCode;

            _PlayClipHelper += Play;

            _PlayClipOneShotHelper += PlayOneShot;

            //Mixer Related
            _ChangeVolumeHelper += ChangeMixerVolume;
        }

        #region Behaviors

        public static ClipData GetClipData(string code)
        {
            return _GetAudioClipHelper(code);
        }

        public static void PlayClip(ClipData clipData)
        {
            _PlayClipHelper?.Invoke(clipData);
        }
        
        public static void PlayClipOneShot(ClipData clipData)
        {
            _PlayClipOneShotHelper?.Invoke(clipData);
        }
        
        public static void PlayClipCustom(ClipData clipData, AudioSource targetAudioSource)
        {
            _PlayClipCustomHelper?.Invoke(clipData, targetAudioSource);
        }
        
        public static void PlayClipOneShotCustom(ClipData clipData, AudioSource targetAudioSource)
        {
            _PlayClipOneShotCustomHelper?.Invoke(clipData, targetAudioSource);
        }

        public static void ChangeVolume(string channelName, float volume)
        {
            _ChangeVolumeHelper?.Invoke(channelName, volume);
        }

        private void Play(ClipData clipData)
        {
            Play(clipData, SetClipToAudioSource(clipData));
        }

        private void PlayOneShot(ClipData clipData)
        {
            PlayOneShot(clipData, SetClipToAudioSource(clipData));
        }
        
        private void Play(ClipData clipData, AudioSource targetAudioSource)
        {
            if (!targetAudioSource) return;

            targetAudioSource.clip = clipData.clip;
            targetAudioSource.Play();
        }

        private void PlayOneShot(ClipData clipData, AudioSource targetAudioSource)
        {
            if (!targetAudioSource) return;

            targetAudioSource.PlayOneShot(clipData.clip);
        }

        private void ChangeMixerVolume(string channelName, float volume)
        {
            if (!audioMixer) return;

            audioMixer.SetFloat(channelName, Mathf.Log10(volume == 0 ? 0.000001f : volume) * 20);
        }

        #endregion

        #region Utility
        
        private AudioSource SetClipToAudioSource(ClipData clipData)
        {
            switch (clipData.type)
            {
                case AudioType.Music:
                    return musicAudioSource;
                case AudioType.SFX:
                    return sfxAudioSource;
                default:
                    return null;
            }
        }

        #endregion
    }
}