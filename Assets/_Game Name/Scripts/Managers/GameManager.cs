using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using CodeMonkey.Utils;

namespace com.DiracStudios.Dots
{
    [RequireComponent(typeof(InputManager))]
    [RequireComponent(typeof(EventManager))]
    [RequireComponent(typeof(GameAssets))]
    [RequireComponent(typeof(GUIManager))]
    [RequireComponent(typeof(PopupManager))]
    public class GameManager : MonoBehaviour
    {
        //Game Specific Variables
        [HideInInspector] public GameObjectPooler playerUnitsPool, enemyUnitsPool, unitDestroyedParticlesPool, bulletsPool, laserRaysPool, trailPool, unitDestroyedPaintSplash;

        //public Color playerColorLevel1 = new Color(0.25f, 0.45f, 1f, 1f);
        //public Color playerColorLevel2 = new Color(0.25f, 0.45f, 1f, 0.75f);
        //public Color playerColorLevel3 = new Color(0.25f, 0.45f, 1f, 0.75f);
        //public Color playerColorWeapon = new Color(0.25f, 0.45f, 1f, 0.75f);
        //public Color playerColorWeaponRange = new Color(0.25f, 0.45f, 1f, .15f);
        //public Color playerColorShieldRange = new Color(0.25f, 0.45f, 1f, .15f);
        //public Color playerColorSelected = new Color(0.25f, 0.25f, 1f, 0.5f);
        //public Color playerColorTargeted = new Color(1f, 0.25f, 0.25f, 1f);

        //public Color enemyColorLevel1 = new Color(1f, 0.25f, 0.25f, 1f);
        //public Color enemyColorLevel2 = new Color(1f, 0.25f, 0.25f, 0.75f);
        //public Color enemyColorLevel3 = new Color(1f, 0.25f, 0.25f, 0.75f);
        //public Color enemyColorWeapon = new Color(1f, 0.25f, 0.25f, 0.75f);
        //public Color enemyColorWeaponRange = new Color(1f, 0.25f, 0.25f, 0.15f);
        //public Color enemyColorShieldRange = new Color(0.25f, 0.45f, 1f, .15f);
        //public Color enemyColorSelected = new Color(1f, 0.25f, 0.25f, 0.5f);
        //public Color enemyColorTargeted = new Color(1f,1f,1f,1f);

        //public Color alienColorLevel1 = new Color(0.25f, 0.25f, 0.25f, 1f);
        //public Color alienColorLevel2 = new Color(0.25f, 0.25f, 0.25f, 0.75f);
        //public Color alienColorLevel3 = new Color(0.25f, 0.25f, 0.25f, 0.75f);
        //public Color alienColorWeapon = new Color(0.25f, 0.25f, 0.25f, 0.75f);
        //public Color alienColorWeaponRange = new Color(0.25f, 0.25f, 0.25f, .15f);
        //public Color alienColorShieldRange = new Color(0.25f, 0.45f, 1f, .15f);
        //public Color alienColorSelected = new Color(0.25f, 0.25f, 0.25f, 0.5f);
        //public Color alienColorTargeted = new Color(1f, 0.25f, 0.25f, 1f);

        //Private variables
        private GameObject _levelInstance;
        private GameObject _levelPrefab;
        public static GameManager Instance;
        private bool levelPrefabFound;

        private void OnEnable()
        {
            EventManager.StartListening(GameData.EventTypes.TowerAmountChanged, OnTowerAmountChanged);
            EventManager.StartListening(GameData.EventTypes.StartGame, OnButtonPlayPressed);
            EventManager.StartListening(GameData.EventTypes.GameWin, OnGameWin);

            //EventManager.TriggerEvent(GameData.EventTypes.TappedToPlay); //TODO - Just for tests
        }

        private void OnDisable() {
            EventManager.StopListening(GameData.EventTypes.TowerAmountChanged, OnTowerAmountChanged);
            EventManager.StopListening(GameData.EventTypes.StartGame, OnButtonPlayPressed);
            EventManager.StopListening(GameData.EventTypes.GameWin, OnGameWin);
        }

        void Awake()
        {
            GameData.SetGameState(GameData.GameStates.Initializing);

            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            GameData.LoadPlayerPrefs();

            Application.targetFrameRate = 90;
        }

        private void Update()
        {
            //I won't use a complex state machine for a simple game

            switch (GameData.gameState)
            {
                case GameData.GameStates.Initializing:
                    
                    //Wait until all managers are Instantiated
                    if (GameAssets.Instance /* && other manager && etc*/) { 
                        GameData.SetGameState(GameData.GameStates.Loading);
                    }

                    break;

                case GameData.GameStates.Loading:
                    
                    //Show loading screen
                    //_loadingScreen.setActive(true);

                    //Instantiate Level
                    Destroy(_levelInstance); //destroy old level
                    var levelName = $"Level{GameData.currentLevel}";
                    _levelPrefab = Resources.Load<GameObject>($"Levels/{levelName}");
                    if(_levelPrefab != null)
                    {
                        _levelInstance = Instantiate(_levelPrefab);
                        levelPrefabFound = true;
                    }
                    else
                    {
                        levelPrefabFound = false;
                        Debug.Log($"Prefab {levelName} Not Found!");
                    }

                    EventManager.TriggerEvent(GameData.EventTypes.GameReady);
                    GameData.SetGameState(GameData.GameStates.MainMenu);
                    SoundManager.PlayBGM(SoundManager.Music.BGMFunGameplay01);

                    break;
                case GameData.GameStates.MainMenu:
                    break;
                case GameData.GameStates.Playing:
                    break;
                case GameData.GameStates.GameOver:
                    break;
                case GameData.GameStates.GameWin:
                    break;
                default:
                    break;
            }
        }


        private void OnButtonPlayPressed()
        {
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(0.1f);

            if (!levelPrefabFound) {
                var genericMessage = new PopupGenericMessage.GenericMessage() {
                    title = "More Levels Soon!",
                    line2 = "Please check back in a while",
                    buttonCancelText = "",
                    buttonOKText = "OK",
                    duration = 0f,
                    messageType = PopupGenericMessage.MessageType.Info
                };
                PopupManager.Instance.ShowGenericMessage(genericMessage);
                yield break;
            }

            GameData.SetGameState(GameData.GameStates.Playing);
            EventManager.TriggerEvent(GameData.EventTypes.GameStarted);
            SoundManager.PlaySoundOneShot(SoundManager.Sound.GameStart);

            StartCoroutine(KeepTabOnTowerAmounts()); //redundant check, but needed because sometimes the list of towers don't reflect reality
        }

        private IEnumerator KeepTabOnTowerAmounts()
        {
            yield return new WaitForSeconds(1f); //wait a bit for the lists to be filled

            while (GameData.gameState != GameData.GameStates.GameWin &&
                    GameData.gameState != GameData.GameStates.GameOver)
            {
                if (GameData.listOfEnemyBasicTowers.Count <= 0 &&
                    GameData.listOfEnemyAttackTowers.Count <= 0 &&
                    GameData.listOfEnemyStatusEffectTowers.Count <= 0 &&
                    GameData.listOfEnemyShieldTowers.Count <= 0 &&
                    GameData.gameState == GameData.GameStates.Playing)
                {
                    GameData.numEnemyUnits = 0;
                    GameData.SetGameState(GameData.GameStates.GameWin);
                    EventManager.TriggerEvent(GameData.EventTypes.UnitsAmountChanged);
                    EventManager.TriggerEvent(GameData.EventTypes.GameWin);
                }
                else if (GameData.listOfPlayerBasicTowers.Count <= 0 &&
                    GameData.listOfPlayerAttackTowers.Count <= 0 &&
                    GameData.listOfPlayerStatusEffectTowers.Count <= 0 &&
                    GameData.listOfPlayerShieldTowers.Count <= 0 &&
                    GameData.gameState == GameData.GameStates.Playing)
                {
                    GameData.numPlayerUnits = 0;
                    GameData.SetGameState(GameData.GameStates.GameOver);
                    EventManager.TriggerEvent(GameData.EventTypes.UnitsAmountChanged);
                    EventManager.TriggerEvent(GameData.EventTypes.GameOver);
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        private void OnTowerAmountChanged()
        {
            if (GameData.numEnemyTowers <= 0)
            {
                if (GameData.gameState == GameData.GameStates.Playing)
                {
                    GameData.SetGameState(GameData.GameStates.GameWin);
                    EventManager.TriggerEvent(GameData.EventTypes.GameWin);
                    SoundManager.StopBGM();
                    SoundManager.PlaySoundOneShot(SoundManager.Sound.GameWin);
                }
            }
            else if (GameData.numPlayerTowers <= 0)
            {
                if (GameData.gameState == GameData.GameStates.Playing)
                {
                    GameData.SetGameState(GameData.GameStates.GameOver);
                    EventManager.TriggerEvent(GameData.EventTypes.GameOver);
                    SoundManager.StopBGM();
                    SoundManager.PlaySoundOneShot(SoundManager.Sound.GameOver);
                }
            }
        }

        private void OnGameWin() {
            //GameData.currentLevel += 1;
            GameData.SavePlayerPrefs();
        }

        public void ReloadScene()
        {
            //reset stuff from GameData
            GameData.ResetAllData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}