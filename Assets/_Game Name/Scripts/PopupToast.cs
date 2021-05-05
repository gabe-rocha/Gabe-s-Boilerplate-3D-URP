using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupToast : MonoBehaviour
{
    public enum ToastState {
        Ready,
        FadingIn,
        Showing,
        FadingOut
    }

    public TextMeshProUGUI txtToastMessage;
    public float toastAnimationSpeed = 2f; //2f
    public float toastDisplayDurationInSeconds = 2f; //2f
    public float toastPosYInPixels = 48f; //48f

    private List<string> _listToasts = new List<string>();
    private ToastState toastState = ToastState.Ready;
    private float _toastStartTime;
    private float _initialToastPosY;

    private void Awake() {
        _initialToastPosY = transform.position.y;
    }

    private void Update()
    {
        switch (toastState)
        {
            case ToastState.Ready:
                if (_listToasts.Count > 0)
                {
                    ShowAToast();
                }
                break;

            case ToastState.FadingIn:
                //life's good
                break;

            case ToastState.Showing:

                if (Time.unscaledTime > _toastStartTime + toastDisplayDurationInSeconds)
                {
                    DismissAToast();
                }
                break;

            case ToastState.FadingOut:
                //life's good
                break;
        }
    }

    private void DismissAToast()
    {
        StartCoroutine(DismissAToastCoRo());
    }

    public void ShowToast(string toastMsg)
    {
        if (!_listToasts.Contains(toastMsg))
            _listToasts.Add(toastMsg);
    }

    private IEnumerator DismissAToastCoRo()
    {
        toastState = ToastState.FadingOut;

        while (transform.position.y > _initialToastPosY)
        {
            transform.position += Vector3.down * toastAnimationSpeed;
            yield return null;
        }

        toastState = ToastState.Ready;
    }

    private void ShowAToast()
    {
        txtToastMessage.text = _listToasts[0]; //FIFO
        _listToasts.RemoveAt(0);

        StartCoroutine(ShowAToastCoRo());
    }

    private IEnumerator ShowAToastCoRo()
    {
        toastState = ToastState.FadingIn;

        while (transform.position.y < toastPosYInPixels)
        {
            transform.position += Vector3.up * toastAnimationSpeed;
            yield return null;
        }

        _toastStartTime = Time.unscaledTime;
        toastState = ToastState.Showing;
    }
}