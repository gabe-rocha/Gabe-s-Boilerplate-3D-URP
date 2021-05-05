using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimImageAlphaPulse : MonoBehaviour
{
    private Image _imgToPulse;
    [SerializeField] private float _minAlpha = 0f;
    [SerializeField] private float _maxAlpha = 1f;
    [SerializeField] private float _interval = 1f;


    private void Awake()
    {
        _imgToPulse = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(AlphaPulse());
    }

    private IEnumerator AlphaPulse()
    {
        bool breathingIn = false; //In is alpha _toAlpha, out is alpha _fromAlpha
        while (true)
        {
            if(breathingIn)
            {
                _imgToPulse.CrossFadeAlpha(_maxAlpha, _interval, true);
            }
            else
            {
                _imgToPulse.CrossFadeAlpha(_minAlpha, _interval, true);
            }
            yield return new WaitForSeconds(_interval);

            breathingIn = !breathingIn;
        }
    }
}
