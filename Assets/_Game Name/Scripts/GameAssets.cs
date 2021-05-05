using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour {

    public static GameAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public SoundAudioClip[] soundAudioClipArray;
    public MusicAudioClip[] musicAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    [System.Serializable]
    public class MusicAudioClip {
        public SoundManager.Music music;
        public AudioClip audioClip;
    }
}
