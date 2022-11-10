using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody characterRigidbody;
    PlayerInput playerInput;
    Actions playerInputActions;
    Animator animator;
    GameObject player;
    new Collider collider;
    bool isGrounded = true;
    float jumpDirection = 0;
    Pausing pausing;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        collider.material.dynamicFriction = 3;
        collider.material.staticFriction = 3;
        characterRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new Actions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        pausing = GetComponent<Pausing>();
    }

    void FixedUpdate()
    {
        if (playerInputActions == null)
        {
            return;
        }
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 movementVector = new Vector3(inputVector.x, 0, inputVector.y);
        if (inputVector != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementVector);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.fixedDeltaTime);
            if (inputVector.x != 0 || inputVector.y != 0)
            {
                characterRigidbody.MoveRotation(targetRotation);
            }
        }
        if (isGrounded == false & (transform.eulerAngles.y >= jumpDirection + 90 || transform.eulerAngles.y <= jumpDirection - 90))
        {
            characterRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y).normalized * 100f, ForceMode.Force);
        }
        else
        {
            characterRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y).normalized * 250f, ForceMode.Force);
        }
        float tempY = characterRigidbody.velocity.y;
        characterRigidbody.velocity = new Vector3(characterRigidbody.velocity.x, 0, characterRigidbody.velocity.z);
        if (characterRigidbody.velocity.magnitude > 8f)
        {
            characterRigidbody.velocity = characterRigidbody.velocity.normalized * 6f;
        }
        characterRigidbody.velocity = new Vector3(characterRigidbody.velocity.x, tempY, characterRigidbody.velocity.z);
        if (inputVector.x != 0 || inputVector.y != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        if (isGrounded == false)
        {
            collider.material.dynamicFriction = 0;
            collider.material.staticFriction = 0;
        }
        if (isGrounded == true)
        {
            collider.material.dynamicFriction = 3;
            collider.material.staticFriction = 3;
        }
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (context.performed & isGrounded == true & pausing.paused == false)
        {
           characterRigidbody.AddForce(Vector3.up * 9f, ForceMode.Impulse);
            animator.SetBool("Jumping", true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            isGrounded = true;
            animator.SetBool("Grounded", true);
            animator.SetBool("Jumping", false);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            isGrounded = false;
            animator.SetBool("Grounded", false);
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            jumpDirection = transform.eulerAngles.y;
        }
    }
}
