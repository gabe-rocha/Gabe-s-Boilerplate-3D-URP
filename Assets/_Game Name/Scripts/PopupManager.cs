using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupManager : MonoBehaviour {
    public static PopupManager Instance { get; private set; }

    private GameObject popupToast, popupGenericMessage;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        popupToast = Resources.Load("Popups/Popup Toast") as GameObject;
        popupGenericMessage = Resources.Load("Popups/Popup Generic Message") as GameObject;
    }

    public void ShowGenericMessage(PopupGenericMessage.GenericMessage message) {
        var popup = Instantiate(popupGenericMessage, Vector3.zero, Quaternion.identity, FindObjectOfType<Canvas>().transform);
        popup.transform.position = Camera.main.WorldToScreenPoint(Vector3.zero);
        popup.GetComponent<PopupGenericMessage>().ShowMessage(message); 
    }

    public void ShowToast(string message) {
        var popup = Instantiate(popupToast, FindObjectOfType<Canvas>().transform);
        popup.GetComponent<PopupToast>().ShowToast(message);
    }
}
