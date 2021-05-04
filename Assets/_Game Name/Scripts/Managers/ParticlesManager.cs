using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.DiracStudios.Dots {
    public class ParticlesManager : MonoBehaviour
    {
        public static ParticlesManager Instance;

        void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private GameObjectPooler _unitDestroyedPool, _unitDestroyedPaintSplashPool;

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => GameManager.Instance.unitDestroyedParticlesPool != null && GameManager.Instance.unitDestroyedPaintSplash != null);
            _unitDestroyedPool = GameManager.Instance.unitDestroyedParticlesPool;
            _unitDestroyedPaintSplashPool = GameManager.Instance.unitDestroyedPaintSplash;
        }

        public void PlayUnitDestroyedParticles(Vector2 position, Color color)
        {
            StartCoroutine(PlayUnitDestroyedParticlesAtCo(position, color));
        }

        private IEnumerator PlayUnitDestroyedParticlesAtCo(Vector2 position, Color color)
        {
            var particleGO = _unitDestroyedPool.GetAPooledObject();
            particleGO.transform.position = position;
            particleGO.SetActive(true);
            particleGO.GetComponent<ParticleSystem>().Clear();
            particleGO.GetComponent<ParticleSystem>().Play();
            var main = particleGO.GetComponent<ParticleSystem>().main;
            main.startColor = color;

            //var splashGO = _unitDestroyedPaintSplashPool.GetAPooledObject();
            //splashGO.transform.position = position;
            //var splashColor = color;
            //splashColor.a = 0.5f;

            //splashGO.GetComponent<SpriteRenderer>().color = splashColor;
            //splashGO.SetActive(true);

            yield return new WaitForSeconds(3);
            _unitDestroyedPool.ReturnUsedObject(particleGO);
            //Splash wont be returned i guess
        }
    }
}