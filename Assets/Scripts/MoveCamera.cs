using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    Actions playerInputActions;
    public new GameObject camera;

    void Start()
    {
        playerInputActions = new Actions();
        playerInputActions.Player.Enable();
    }

    void FixedUpdate()
    {
        Vector2 cameraVector = playerInputActions.Player.Look.ReadValue<Vector2>();
        camera.transform.position = transform.position + new Vector3(0, 5, -5);
        camera.transform.eulerAngles += new Vector3 (-cameraVector.y, cameraVector.x, 0);
    }
}
