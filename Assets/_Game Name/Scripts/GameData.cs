using System.Collections.Generic;
using UnityEngine;

    public static class GameData
    {
        public enum GameStates
        {
            Initializing,
            Loading,
            MainMenu,
            Playing,
            GameOver,
            GameWin
        }

        public static GameStates gameState { get; private set; }

        public static void SetGameState(GameStates gs)
        {
            gameState = gs;
        }

        //DATA VARIABLES - Must be reset when Scene Reloads
        //************************************************************************************


        //Player Data
        public static bool playedBefore;
        public static int currentLevel;
        public static int coins;
        public static int gems;

        public static bool vibrationEnabled;
        public static bool soundEnabled;
        public static bool musicEnabled;


        //************************************************************************************

        public static void ResetAllData()
        {
            
        }

        public static void LoadPlayerPrefs()
        {
            playedBefore = GabeTools.PlayerPrefsX.GetBool("playedBefore");
            currentLevel = Mathf.Max(PlayerPrefs.GetInt("currentLevel"), 1);
            coins = PlayerPrefs.GetInt("coins");
            gems = PlayerPrefs.GetInt("gems");

            if (playedBefore) {
                vibrationEnabled = GabeTools.PlayerPrefsX.GetBool("vibrationEnabled");
                soundEnabled = GabeTools.PlayerPrefsX.GetBool("soundEnabled");
                musicEnabled = GabeTools.PlayerPrefsX.GetBool("musicEnabled");
            }
            else { //first play
                vibrationEnabled = true;
                soundEnabled = true;
                musicEnabled = true;

                GabeTools.PlayerPrefsX.SetBool("playedBefore", true);
                GabeTools.PlayerPrefsX.SetBool("vibrationEnabled", vibrationEnabled);
                GabeTools.PlayerPrefsX.SetBool("soundEnabled", soundEnabled);
                GabeTools.PlayerPrefsX.SetBool("musicEnabled", musicEnabled);
            }
        }

        public static void SavePlayerPrefs()
        {
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            PlayerPrefs.SetInt("coins", coins);
            PlayerPrefs.SetInt("gems", gems);

            GabeTools.PlayerPrefsX.SetBool("vibrationEnabled", vibrationEnabled);
            GabeTools.PlayerPrefsX.SetBool("soundEnabled", soundEnabled);
            GabeTools.PlayerPrefsX.SetBool("musicEnabled", musicEnabled);
        }
    }
