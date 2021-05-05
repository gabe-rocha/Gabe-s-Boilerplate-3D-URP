using System;
using UnityEngine;
using UnityEngine.UI;

public class TopLeftMenu : MonoBehaviour
{
    private GameObject topLeftVerticalButtons;
    [SerializeField] private Image imageButtonVibration, imageButtonSound, imageButtonMusic;

    private void Awake() {
        topLeftVerticalButtons = transform.Find("Top Left Vertical Buttons").gameObject;
    }

    private void Start() {
        topLeftVerticalButtons.SetActive(false);
        RefreshSprites();
    }

    private void RefreshSprites() {

        if (GameData.vibrationEnabled) {
            imageButtonVibration.sprite = GameAssets.Instance.imageButtonVibrationOn;
        }
        else {
            imageButtonVibration.sprite = GameAssets.Instance.imageButtonVibrationOff;
        }

        if (GameData.soundEnabled) {
            imageButtonSound.sprite = GameAssets.Instance.imageButtonSoundEffectsOn;
        }
        else {
            imageButtonSound.sprite = GameAssets.Instance.imageButtonSoundEffectsOff;
        }

        if (GameData.musicEnabled) {
            SoundManager.PlayBGM(SoundManager.Music.BGMFunGameplay01);
            imageButtonMusic.sprite = GameAssets.Instance.imageButtonMusicOn;
        }
        else {
            SoundManager.StopBGM();
            imageButtonMusic.sprite = GameAssets.Instance.imageButtonMusicOff;
        }
    }

    public void OnButtonSettingsPressed() {
        topLeftVerticalButtons.SetActive(!topLeftVerticalButtons.activeSelf);
    }

    public void OnButtonVibrationPressed() {
        GameData.vibrationEnabled = !GameData.vibrationEnabled;
        GameData.SavePlayerPrefs();
        RefreshSprites();
    }

    public void OnButtonSoundEffectsPressed() {
        GameData.soundEnabled = !GameData.soundEnabled;
        GameData.SavePlayerPrefs();
        RefreshSprites();
    }

    public void OnButtonMusicPressed() {
        GameData.musicEnabled = !GameData.musicEnabled;
        GameData.SavePlayerPrefs();
        RefreshSprites();
    }

    public void OnButton01Pressed() {

    }

    public void OnButton02Pressed() {

    }

    public void OnButton03Pressed() {

    }
}
