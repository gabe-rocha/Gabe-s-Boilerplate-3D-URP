using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupGenericMessage : MonoBehaviour
{
    public enum MessageType {
        Info,
        Warning,
        Error
    }
    public enum GenericMessageState {
        Hidden,
        Showing
    }

    [System.Serializable]
    public struct GenericMessage {
        public MessageType messageType;
        public string title;
        public string line1;
        public string line2;
        public string line3;
        public string line4;
        public string buttonOKText;
        public string buttonCancelText;
        public UnityAction callbackButtonOK;
        public UnityAction callbackButtonCancel;
        public float duration;
    }

    public TextMeshProUGUI txtGenericTitle, txtGenericLine1, txtGenericLine2, txtGenericLine3, txtGenericLine4;
    public Button btnGenericOK, btnGenericCancel;
    private UnityEvent _evtButtonOk, _evtButtonCancel;
    private List<GenericMessage> _listGenericMessages;
    private GenericMessageState _genericMessageState = GenericMessageState.Hidden;

    // Start is called before the first frame update
    private void Start()
    {
        _listGenericMessages = new List<GenericMessage>();
        _evtButtonCancel = new UnityEvent();
        _evtButtonOk = new UnityEvent();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (_genericMessageState)
        {
            case GenericMessageState.Hidden:
                if (_listGenericMessages.Count > 0)
                {
                    DisplayAnotherGenericMessage();
                }
                break;

            case GenericMessageState.Showing:
                break;

            default:
                break;
        }
    }

    private void DisplayAnotherGenericMessage()
    {
        ShowMessage(_listGenericMessages[0]);
        _listGenericMessages.RemoveAt(0);
    }

    public void ShowMessage(GenericMessage genericMessage)
    {
        //DISPLAY 1 MESSAGE AT A TIME
        if (_genericMessageState == GenericMessageState.Showing)
        {
            //We are already showing a message. Add to the list of messages and display later
            _listGenericMessages.Add(genericMessage);
            return;
        }

        //MESSAGE TYPE
        switch (genericMessage.messageType)
        {
            case MessageType.Info:
                //change image
                break;

            case MessageType.Warning:
                //change image
                break;

            case MessageType.Error:
                //change image
                break;

            default:
                break;
        }

        //TEXTS
        txtGenericTitle.text = genericMessage.title;
        txtGenericLine1.text = genericMessage.line1;
        txtGenericLine2.text = genericMessage.line2;
        txtGenericLine3.text = genericMessage.line3;
        txtGenericLine4.text = genericMessage.line4;

        //BUTTONS
        btnGenericOK.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = genericMessage.buttonOKText;
        btnGenericOK.gameObject.SetActive(genericMessage.buttonOKText != "");
        btnGenericCancel.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = genericMessage.buttonCancelText;
        btnGenericCancel.gameObject.SetActive(genericMessage.buttonCancelText != "");
        if (genericMessage.callbackButtonCancel != null)
        {
            _evtButtonCancel.AddListener(genericMessage.callbackButtonCancel);
        }
        if (genericMessage.callbackButtonOK != null)
        {
            _evtButtonOk.AddListener(genericMessage.callbackButtonOK);
        }

        //DURATION
        if (genericMessage.duration > 0)
        {
            StartCoroutine(DismissGenericMessageAfterSeconds(genericMessage.duration));
        }

        //OTHER
        _genericMessageState = GenericMessageState.Showing;
    }

    public void OnButtonOK()
    {
        _evtButtonOk.Invoke();
        _evtButtonOk.RemoveAllListeners();

        float thisInstant = 0f;
        StartCoroutine(DismissGenericMessageAfterSeconds(thisInstant));
    }

    public void OnButtonCancel()
    {
        _evtButtonCancel.Invoke();
        _evtButtonCancel.RemoveAllListeners();

        float thisInstant = 0f;
        StartCoroutine(DismissGenericMessageAfterSeconds(thisInstant));
    }

    private IEnumerator DismissGenericMessageAfterSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);

        ////DISMISS THE MESSAGE
        //GetComponent<Animator>().SetTrigger("Hide");

        //float animDuration = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        //yield return new WaitForSeconds(animDuration);

        ////ANIMATION DONE, MESSAGE DISMISSED
        //_genericMessageState = GenericMessageState.Hidden;
    }
}