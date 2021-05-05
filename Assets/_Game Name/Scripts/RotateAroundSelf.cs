using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundSelf : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = -90f;

    private void Start() {
        if(rotationSpeed == 0) {
            rotationSpeed = -90f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
