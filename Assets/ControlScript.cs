using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    Vector2 rotation;

    void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        rotation.x -= Input.GetAxis("Mouse X") / 4;
        rotation.y += Input.GetAxis("Mouse Y") / 4;
        rotation.x = Mathf.Clamp(rotation.x, -20, 20);
        rotation.y = Mathf.Clamp(rotation.y, -20, 20);

        transform.rotation = Quaternion.Euler(rotation.y,0,rotation.x);
    }
}
