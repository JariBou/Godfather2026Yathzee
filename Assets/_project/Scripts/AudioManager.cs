using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private RandomSoundPlayer _diceSoundPlayer = new();

        public RandomSoundPlayer DiceSoundPlayer => _diceSoundPlayer;

        private void Awake()
        {
            DiceSoundPlayer.Setup(_audioSource);
        }


        [Serializable]
        public class RandomSoundPlayer
        {
            [SerializeField] private List<AudioClip> _audioClips = new();
            private AudioSource _audioSource;
            private AudioClip _lastClip;

            public void Setup(AudioSource audioSource)
            {
                _audioSource = audioSource;
            }
            
            public void PlayRandomSound()
            {
                if (_audioClips.Count == 0)
                {
                    Debug.LogWarning("No audio clips found");
                    return;
                }
                
                AudioClip clip = _audioClips[Random.Range(0, _audioClips.Count)];
                while (clip == _lastClip)
                {
                    clip = _audioClips[Random.Range(0, _audioClips.Count)];
                }
                
                _audioSource.PlayOneShot(clip);
                _lastClip = clip;
            }
            
        }
    }
}