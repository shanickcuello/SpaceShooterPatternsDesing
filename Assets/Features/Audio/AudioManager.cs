using System;
using UnityEngine;

namespace Features.Audio
{
    public class AudioManager : MonoBehaviour
    {
        static AudioManager _instance;
        public static AudioManager Instance => _instance;

        [SerializeField]
        public Sound[] sounds;

        void Awake()
        {
            if (Instance == null)
                _instance = this;
            foreach (Sound sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.loop = sound.loop;
            }
        }
        public void Play(string soundKey)
        {
            Sound sound = Array.Find(sounds, item => item.name == soundKey);
            if (sound == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            if(!sound.source.isPlaying)
                sound.Play();
        }
        public void Stop(string soundKey)
        {
            Sound sound = Array.Find(sounds, item => item.name == soundKey);
            if (sound == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            sound.Stop();
        }
        [System.Serializable]
        public class Sound
        {
            public string name;

            [Range(0f, 1f)]
            public float volume = 0.5f;
            [Range(0.5f, 1.5f)]
            public float pitch = 1f;

            public AudioClip clip;
            public bool loop = false;

            [HideInInspector]
            public AudioSource source;

            public void Play()
            {
                source.volume = volume;
                source.pitch = pitch;
                source.Play();
            }
            public void Stop()
            {
                source.Stop();
            }
        }
    }
}
