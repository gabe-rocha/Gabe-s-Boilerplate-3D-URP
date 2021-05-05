using UnityEngine;
using UnityEngine.UI;

public class TopLeftMenu : MonoBehaviour
{
    private GameObject topLeftVerticalButtons;
    [SerializeField] private Image imageButtonVibration;

    private void Awake() {
        topLeftVerticalButtons = transform.Find("Top Left Vertical Buttons").gameObject;
    }

    private void Start() {
        topLeftVerticalButtons.SetActive(false);
    }

    public void OnButtonSettingsPressed() {
        topLeftVerticalButtons.SetActive(!topLeftVerticalButtons.activeSelf);
    }

    public void OnButtonVibrationPressed() {
        GameData.vibrationEnabled = !GameData.vibrationEnabled;

        if (GameData.vibrationEnabled) {
            imageButtonVibration.sprite = GameAssets.Instance.imageButtonVibrationOn;
        }
        else {
            imageButtonVibration.sprite = GameAssets.Instance.imageButtonVibrationOff;
        }
    }

    public void OnButtonSoundEffectsPressed() {
        GameData.soundEnabled = !GameData.soundEnabled;

        //if (GameData.soundEnabled) {
        //    imagebutton.sprite = GameAssets.Instance.imageButtonVibrationOn.sprite;
        //}
        //else {
        //    imageButtonVibration.sprite = GameAssets.Instance.imageButtonVibrationOff.sprite;
        //}
    }

    public void OnButtonMusicPressed() {

    }

    public void OnButton01Pressed() {

    }

    public void OnButton02Pressed() {

    }

    public void OnButton03Pressed() {

    }
}
