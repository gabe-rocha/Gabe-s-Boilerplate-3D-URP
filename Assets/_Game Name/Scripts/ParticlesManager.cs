using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public static ParticlesManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    private GameObjectPooler _unitDestroyedPool, _unitDestroyedPaintSplashPool;

    private void Start()
    {
        //yield return new WaitUntil(() => GameManager.Instance.unitDestroyedParticlesPool != null);
        //_unitDestroyedPool = GameManager.Instance.unitDestroyedParticlesPool;
    }

    ////////////////
    //Example Only
    ////////////////
    //public void PlayUnitDestroyedParticles(Vector2 position, Color color)
    //{
    //    StartCoroutine(PlayUnitDestroyedParticlesAtCo(position, color));
    //}

    //private IEnumerator PlayUnitDestroyedParticlesAtCo(Vector2 position, Color color)
    //{
    //    var particleGO = _unitDestroyedPool.GetAPooledObject();
    //    particleGO.transform.position = position;
    //    particleGO.SetActive(true);
    //    particleGO.GetComponent<ParticleSystem>().Clear();
    //    particleGO.GetComponent<ParticleSystem>().Play();
    //    var main = particleGO.GetComponent<ParticleSystem>().main;
    //    main.startColor = color;

    //    yield return new WaitForSeconds(3);
    //    _unitDestroyedPool.ReturnUsedObject(particleGO);
    //}
}
