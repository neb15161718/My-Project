using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public Vector3 direction;
    public GameObject player;
    Rigidbody characterRigidbody;
    bool playerContact;

    void Start()
    {
        characterRigidbody = player.GetComponent<Rigidbody>();
        playerContact = false;
    }

    void FixedUpdate()
    {
        if (playerContact)
        {
            characterRigidbody.position += direction * 0.2f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            playerContact = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            playerContact = false;
        }
    }
}
