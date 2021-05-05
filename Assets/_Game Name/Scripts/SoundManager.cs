using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager{
    public enum Sound {
        GameStarted,
        GameWon,
        GameOver,
        GenericError,
        GenericClick,
    }

    public enum Music {
        BGMFunGameplay01,
    }

    private static GameObject audioOneShotGO, musicPlayer;
    private static AudioSource audioSourceOneShot, audioSourceBGM;

    public static void PlaySoundOneShot(Sound sound) {

        if (!GameData.soundEnabled) return;

        if (audioOneShotGO == null) {
            audioOneShotGO = new GameObject("Audio One Shot");
            audioSourceOneShot = audioOneShotGO.AddComponent<AudioSource>();
        }
        audioSourceOneShot.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound) {
        foreach (var soundAudioClip in GameAssets.Instance.soundAudioClipArray) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError($"Sound clip {sound} not found!");
        return null;
    }
        
    public static void PlayBGM(Music music) {

        if (!GameData.musicEnabled) return;

        musicPlayer = new GameObject("Music Player");
        audioSourceBGM = musicPlayer.AddComponent<AudioSource>();
        audioSourceBGM.clip = GetMusicClip(music);
        audioSourceBGM.loop = true;
        audioSourceBGM.Play();
    }

    public static void StopBGM() {
        if (audioSourceBGM) {
            audioSourceBGM.Stop();
        }
    }

    private static AudioClip GetMusicClip(Music music) {
        foreach (var musicAudioClip in GameAssets.Instance.musicAudioClipArray) {
            if (musicAudioClip.music == music) {
                return musicAudioClip.audioClip;
            }
        }
        Debug.LogError($"Music clip {music} not found!");
        return null;
    }
}