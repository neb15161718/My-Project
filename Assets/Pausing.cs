using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pausing : MonoBehaviour
{
    PlayerInput playerInput;
    Actions playerInputActions;
    GameObject player;
    public bool paused = false;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new Actions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Pause.performed += Pause;
    }

    void Update()
    {

    }
        
    void Pause(InputAction.CallbackContext context)
    {
        if (paused == false)
        {
            Time.timeScale = 0;
            paused = true;
        }
        else
        {   
            Time.timeScale = 1;
            paused = false;
        }
    }
}
