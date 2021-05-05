using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimImageScalePulse : MonoBehaviour
{
    private Image _imgToPulse;
    [SerializeField] private float _minScaleX = 0.9f;
    [SerializeField] private float _minScaleY = 0.9f;
    [SerializeField] private float _maxScaleX = 1f;
    [SerializeField] private float _maxScaleY = 1f;
    [SerializeField] private float _intervalInSecs = 1f;

    private void Awake()
    {
        _imgToPulse = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(ScalePulse());
    }

    private IEnumerator ScalePulse()
    {
        bool scalingToMinX = true;
        Vector3 minScale = new Vector3(_minScaleX, _minScaleY, _imgToPulse.transform.localScale.z);
        Vector3 maxScale = new Vector3(_maxScaleX, _maxScaleY, _imgToPulse.transform.localScale.z);

        while (true)
        {
            if(scalingToMinX)
            {
                _imgToPulse.transform.localScale -= minScale / (_intervalInSecs * 8f / Time.deltaTime);

                if (_imgToPulse.transform.localScale.x <= _minScaleX)
                {
                    _imgToPulse.transform.localScale = minScale;
                    scalingToMinX = false; //let's scale to max
                }
            }
            else
            {
                _imgToPulse.transform.localScale += maxScale / (_intervalInSecs * 8f / Time.deltaTime);

                if (_imgToPulse.transform.localScale.x >= _maxScaleX)
                {
                    _imgToPulse.transform.localScale = maxScale;
                    scalingToMinX = true;
                }
            }
            yield return null;
        }
    }
}
