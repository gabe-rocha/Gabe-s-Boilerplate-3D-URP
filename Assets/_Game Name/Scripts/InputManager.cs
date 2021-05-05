using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _previousMousePosition;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.Instance.MouseButtonDown?.Invoke(Input.mousePosition);
        }

        else if (Input.GetMouseButton(0) && Input.mousePosition != _previousMousePosition) //Dragging
        {
            //EventManager.Instance.MouseDragging?.Invoke(this, Input.mousePosition);
            EventManager.Instance.MouseDragging?.Invoke(Input.mousePosition);
        }

        else if (Input.GetMouseButtonUp(0)) {
            EventManager.Instance.MouseButtonUp?.Invoke(Input.mousePosition);
        }

        _previousMousePosition = Input.mousePosition;
    }
}