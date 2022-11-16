using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    Vector3 target;
    Animator animator;
    public int health;
    public bool touchingPlayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = 3;
        touchingPlayer = false;
    }

    void FixedUpdate()
    {
        target = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target, 0.05f);
        if (transform.position == target)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = true;
            StartCoroutine(Attack());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = false;
        }
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1);
        if (touchingPlayer == true)
        {
            Attacking.Instance.health = Attacking.Instance.health - 1;
        }
    }
}
