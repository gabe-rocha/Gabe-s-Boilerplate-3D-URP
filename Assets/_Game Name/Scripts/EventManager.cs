using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    public Action GameReady;
    public Action GameStarted;
    public Action GamePaused;
    public Action GameWon;
    public Action GameOver;

    public Action<Vector3> MouseDragging;
    public Action<Vector3> MouseButtonUp;
    public Action<Vector3> MouseButtonDown;

    public static EventManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
}
