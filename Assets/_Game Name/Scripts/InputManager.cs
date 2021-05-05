using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _previousMousePosition;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventManager.Instance.MouseButtonDown(Input.mousePosition);
        }

        else if (Input.GetMouseButton(0) && Input.mousePosition != _previousMousePosition) //Dragging
        {
            EventManager.Instance.MouseDragging(Input.mousePosition);
        }

        else if (Input.GetMouseButtonUp(0)) {
            EventManager.Instance.MouseButtonUp(Input.mousePosition);
        }

        _previousMousePosition = Input.mousePosition;
    }
}
