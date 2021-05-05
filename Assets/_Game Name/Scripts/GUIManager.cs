using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private Image imageLoading, imageLoadingText;
    [SerializeField] private TextMeshProUGUI textLoading;
    [SerializeField] private GameObject popupGameWin, popupGameOver;

    private void OnEnable() {
        EventManager.Instance.GameReady += OnGameReady;
        EventManager.Instance.GameWon += OnGameWon;
        EventManager.Instance.GameOver += OnGameOver;
    }

    private void OnDisable() {
        EventManager.Instance.GameReady -= OnGameReady;
        EventManager.Instance.GameWon -= OnGameWon;
        EventManager.Instance.GameOver -= OnGameOver;
    }

    private void Awake()
    {
    }

    private void OnGameReady() {
        StartCoroutine(LoadingScreenFadeOutCo());
    }

    private IEnumerator LoadingScreenFadeOutCo() {
        yield return new WaitForSeconds(2f);
        float fadeOutDuration = 0.5f;
        float fadeOutStartTime = Time.time;

        while (Time.time < fadeOutStartTime + fadeOutDuration) {
            imageLoading.CrossFadeAlpha(0f, fadeOutDuration, true);
            //imageLoadingText.CrossFadeAlpha(0f, fadeOutDuration, true);

            var textLoadingColor = textLoading.color;
            textLoadingColor.a -= fadeOutDuration * 4f * Time.deltaTime;
            textLoading.color = textLoadingColor;

            yield return null;
        }

        Destroy(imageLoading.gameObject);
    }

    private void OnGameWon()
    {
        Instantiate(popupGameWin);
    }

    private void OnGameOver()
    {
        Instantiate(popupGameOver);
    }
}
