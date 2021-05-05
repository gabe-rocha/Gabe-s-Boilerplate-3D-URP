using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(EventManager))]
[RequireComponent(typeof(GameAssets))]
[RequireComponent(typeof(GUIManager))]
[RequireComponent(typeof(PopupManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Game Specific Variables
    [HideInInspector] public GameObjectPooler bulletsPool;

    //Private variables
    private GameObject _levelInstance;
    private GameObject _levelPrefab;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }

        GameData.SetGameState(GameData.GameStates.Initializing);
        GameData.LoadPlayerPrefs();
        Application.targetFrameRate = 90;
    }

    private void Start() {
        EventManager.Instance.GameWon += OnGameWon;
        EventManager.Instance.GameOver += OnGameOver;
        EventManager.Instance.MouseDragging += OnMouseDrag;
    }

    private void OnMouseDrag(Vector3 e) {
        Debug.Log("Mouse Dragging: " + e.ToString());
    }

    private void OnDestroy() {
        EventManager.Instance.GameWon -= OnGameWon;
        EventManager.Instance.GameOver -= OnGameOver;
    }

    private void Update()
    {
        //I won't use complex state machines for a simple game

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

                //Level Loaded, inform everybody
                EventManager.Instance.GameReady?.Invoke();
                SoundManager.PlayBGM(SoundManager.Music.BGMFunGameplay01);

                GameData.SetGameState(GameData.GameStates.MainMenu);

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
        StartGame();
    }

    private void StartGame()
    {
        SoundManager.PlaySoundOneShot(SoundManager.Sound.GameStarted);

        GameData.SetGameState(GameData.GameStates.Playing);
        EventManager.Instance.GameStarted?.Invoke();
    }

    private void OnGameWon() {
        //GameData.currentLevel += 1;
        GameData.SavePlayerPrefs();
    }

    private void OnGameOver() {

    }

    public void ReloadScene()
    {
        //reset stuff from GameData
        GameData.ResetAllData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
