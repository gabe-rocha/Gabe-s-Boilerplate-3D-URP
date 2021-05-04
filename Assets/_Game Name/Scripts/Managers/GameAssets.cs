using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.DiracStudios.Dots {
    public class GameAssets : MonoBehaviour {

        public static GameAssets Instance { get; private set; }

        [SerializeField] private List<TowerSpriteSetSO> towerSpriteSets;
        [SerializeField] private List<ColorSetSO> colorSets;
        [SerializeField] private GameObject basicTowerPrefab, attackTowerPrefab, shieldTowerPrefab, statusEffectTowerPrefab;

        private void Awake() {
            if (Instance == null) Instance = this;
        }

        public TowerSpriteSetSO GetSpriteSet(int id) {
            if (towerSpriteSets.Count > 0) {

                foreach(TowerSpriteSetSO towerSpriteSet in towerSpriteSets) {
                    if(towerSpriteSet.towerSpriteSetID == id) {
                        return towerSpriteSet;
                    }
                }
                return null;
            }
            else {
                return null;
            }
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

        public GameObject GetBasicTowerPrefab() {
            return basicTowerPrefab;
        }
        public GameObject GetAttackTowerPrefab() {
            return attackTowerPrefab;
        }
        public GameObject GetShieldTowerPrefab() {
            return shieldTowerPrefab;
        }
        public GameObject GetStatusEffectTowerPrefab() {
            return statusEffectTowerPrefab;
        }

        public Color GetAttackTowerRangeColor(int id) {
            if (colorSets.Count > 0) {
                foreach (ColorSetSO colorSet in colorSets) {
                    if (colorSet.colorSetId == id) {
                        return colorSet.attackTowerRangeColor;
                    }
                }
                return Color.white;
            }
            else {
                return Color.white;
            }
        }

        public Color GetShieldTowerRangeColor(int id) {
            if (colorSets.Count > 0) {
                foreach (ColorSetSO colorSet in colorSets) {
                    if (colorSet.colorSetId == id) {
                        return colorSet.shieldTowerRangeColor;
                    }
                }
                return Color.white;
            }
            else {
                return Color.white;
            }
        }

        public Color GetStatusEffectTowerRangeColor(int id) {
            if (colorSets.Count > 0) {
                foreach (ColorSetSO colorSet in colorSets) {
                    if (colorSet.colorSetId == id) {
                        return colorSet.statusEffectTowerRangeColor;
                    }
                }
                return Color.white;
            }
            else {
                return Color.white;
            }
        }

    }
}