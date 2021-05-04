using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace com.DiracStudios.Dots
{
    public class GUIManager : MonoBehaviour
    {
        [SerializeField] private Image imageLoading, imageLoadingText;
        [SerializeField] private TextMeshProUGUI textLoading;
        [SerializeField] private Slider _slider;
        [SerializeField] private GameObject _popupGameWin, _popupGameOver;
        [SerializeField] private TextMeshProUGUI _textPlayerUnits, _textEnemyUnits;
        private RectTransform _textPlayerUnitsRect;
        private RectTransform _textEnemyUnitsRect;

        private void OnEnable() {
            EventManager.StartListening(GameData.EventTypes.GameReady, OnGameReady);
            EventManager.StartListening(GameData.EventTypes.UnitsAmountChanged, OnUnitsAmountChanged);
            EventManager.StartListening(GameData.EventTypes.GameWin, OnGameWin);
            EventManager.StartListening(GameData.EventTypes.GameOver, OnGameOver);
        }

        private void OnDisable() {

            EventManager.StopListening(GameData.EventTypes.GameReady, OnGameReady);
            EventManager.StopListening(GameData.EventTypes.UnitsAmountChanged, OnUnitsAmountChanged);
            EventManager.StopListening(GameData.EventTypes.GameWin, OnGameWin);
            EventManager.StopListening(GameData.EventTypes.GameOver, OnGameOver);
        }

        private void Awake()
        {
            _textPlayerUnitsRect = _textPlayerUnits.transform.GetComponent<RectTransform>();
            _textEnemyUnitsRect = _textEnemyUnits.transform.GetComponent<RectTransform>();
        }

        private void OnGameReady() {
            StartCoroutine(FadeOutCo());
        }

        private IEnumerator FadeOutCo() {
            yield return new WaitForSeconds(2f);
            float fadeOutDuration = 0.5f;
            float fadeOutStartTime = Time.time;

            SoundManager.PlaySoundOneShot(SoundManager.Sound.PageFlip);
            while (Time.time < fadeOutStartTime + fadeOutDuration) {
                imageLoading.CrossFadeAlpha(0f, fadeOutDuration, true);
                //imageLoadingText.CrossFadeAlpha(0f, fadeOutDuration, true);
                textLoading.color = new Color(textLoading.color.r, textLoading.color.g, textLoading.color.b, textLoading.color.a - (fadeOutDuration * 4f * Time.deltaTime));

                yield return null;
            }

            Destroy(imageLoading.gameObject);
        }

        private void OnUnitsAmountChanged()
        {
            //Slider goes from 0 to 1
            //Slider is 0 when Player has 0 units
            //Slider is 1 when Enemy has 0 units
            //Slider value is playerUnits / (playerUnits + enemyUnits)

            var totalPlayerUnits = GetTotalPlayerUnits();
            var totalEnemyUnits = GetTotalEnemyUnits();

            float totalUnits = totalPlayerUnits + totalEnemyUnits;
            if (totalUnits > 0)
            {
                _slider.value = totalPlayerUnits / totalUnits;
            }

            //Set text
            _textPlayerUnits.text = totalPlayerUnits.ToString();
            _textEnemyUnits.text = totalEnemyUnits.ToString();

            //Set position
            _textPlayerUnitsRect.anchoredPosition = new Vector2(_slider.GetComponent<RectTransform>().rect.width * _slider.value / 2, _textPlayerUnitsRect.anchoredPosition.y);
            _textEnemyUnitsRect.anchoredPosition = new Vector2(_slider.GetComponent<RectTransform>().rect.width * (1f - _slider.value) / -2, _textEnemyUnitsRect.anchoredPosition.y);
        }

        private static int GetTotalPlayerUnits()
        {
            int totalPlayerUnits = 0;

            foreach (var tower in GameData.listOfPlayerBasicTowers)
            {
                totalPlayerUnits += tower.unitsAmount;
            }
            foreach (var tower in GameData.listOfPlayerAttackTowers)
            {
                totalPlayerUnits += tower.unitsAmount;
            }
            foreach (var tower in GameData.listOfPlayerShieldTowers)
            {
                totalPlayerUnits += tower.unitsAmount;
            }
            foreach (var tower in GameData.listOfPlayerStatusEffectTowers)
            {
                totalPlayerUnits += tower.unitsAmount;
            }

            return totalPlayerUnits;
        }

        private static int GetTotalEnemyUnits()
        {
            int totalEnemyUnits = 0;

            foreach (var tower in GameData.listOfEnemyBasicTowers)
            {
                totalEnemyUnits += tower.unitsAmount;
            }
            foreach (var tower in GameData.listOfEnemyAttackTowers)
            {
                totalEnemyUnits += tower.unitsAmount;
            }
            foreach (var tower in GameData.listOfEnemyShieldTowers)
            {
                totalEnemyUnits += tower.unitsAmount;
            }
            foreach (var tower in GameData.listOfEnemyStatusEffectTowers)
            {
                totalEnemyUnits += tower.unitsAmount;
            }

            return totalEnemyUnits;
        }

        private void OnGameWin()
        {
            Instantiate(_popupGameWin);
        }

        private void OnGameOver()
        {
            Instantiate(_popupGameOver);
        }
    }
}