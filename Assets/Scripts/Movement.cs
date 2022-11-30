using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class Movement : MonoBehaviour
{
    Rigidbody characterRigidbody;
    Actions playerInputActions;
    Animator animator;
    GameObject player;
    new Collider collider;
    bool grounded;
    bool sprinting;
    float jumpDirection;
    Pausing pausing;
    Attacking attacking;
    public new GameObject camera;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        characterRigidbody = GetComponent<Rigidbody>();
        playerInputActions = MainMenu.Instance.playerInputActions;
        playerInputActions.Player.Enable();
        pausing = GetComponent<Pausing>();
        attacking = GetComponent<Attacking>();
        grounded = true;
        sprinting = false;
        jumpDirection = 0;
    }

    void FixedUpdate()
    {
        if (!attacking.dead)
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
            if (!grounded & (transform.eulerAngles.y >= jumpDirection + 90 || transform.eulerAngles.y <= jumpDirection - 90))
            {
                characterRigidbody.AddForce (movementVector.normalized * 100f, ForceMode.Force);
            }
            else
            {
                characterRigidbody.AddForce(movementVector.normalized * 250f, ForceMode.Force);
            }
            float tempY = characterRigidbody.velocity.y;
            characterRigidbody.velocity = new Vector3(characterRigidbody.velocity.x, 0, characterRigidbody.velocity.z);
            if (sprinting)
            {
                if (characterRigidbody.velocity.magnitude > 12f)
                {
                    characterRigidbody.velocity = characterRigidbody.velocity.normalized * 12f;
                }
            }
            else
            {
                if (characterRigidbody.velocity.magnitude > 8f)
                {
                    characterRigidbody.velocity = characterRigidbody.velocity.normalized * 8f;
                }
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
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed & grounded & !pausing.paused & !attacking.dead)
        {
           characterRigidbody.AddForce(Vector3.up * 25f, ForceMode.Impulse);
            animator.SetBool("Jumping", true);
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sprinting = true;
            animator.SetBool("Sprinting", true);
        }
        else if (context.canceled)
        {
            sprinting = false;
            animator.SetBool("Sprinting", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            grounded = true;
            animator.SetBool("Grounded", true);
            animator.SetBool("Jumping", false);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            grounded = false;
            animator.SetBool("Grounded", false);
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            jumpDirection = transform.eulerAngles.y;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Death Plane")
        {
            attacking.dead = true;
            attacking.deadText.gameObject.SetActive(true);
            attacking.hubButton.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }
}
