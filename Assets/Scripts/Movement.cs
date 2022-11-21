using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    Rigidbody characterRigidbody;
    Actions playerInputActions;
    Animator animator;
    GameObject player;
    new Collider collider;
    bool isGrounded = true;
    float jumpDirection = 0;
    Pausing pausing;
    public new GameObject camera;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        collider.material.dynamicFriction = 3;
        collider.material.staticFriction = 3;
        characterRigidbody = GetComponent<Rigidbody>();
        playerInputActions = new Actions();
        playerInputActions.Player.Enable();
        pausing = GetComponent<Pausing>();
    }

    void FixedUpdate()
    {
        if (playerInputActions == null)
        {
            return;
        }
        if (Attacking.Instance.dead == false)
        {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            Vector3 movementVector = new Vector3(inputVector.x, 0, inputVector.y);
            movementVector = camera.transform.TransformDirection(movementVector);
            movementVector = new Vector3(movementVector.x, 0, movementVector.z);

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
                characterRigidbody.AddForce (movementVector.normalized * 100f, ForceMode.Force);
            }
            else
            {
                characterRigidbody.AddForce(movementVector.normalized * 250f, ForceMode.Force);
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

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed & isGrounded == true & pausing.paused == false & Attacking.Instance.dead == false)
        {
           characterRigidbody.AddForce(Vector3.up * 18f, ForceMode.Impulse);
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
