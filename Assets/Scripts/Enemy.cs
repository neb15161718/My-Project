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
    bool dead;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = 3;
        touchingPlayer = false;
        dead = false;
    }

    void FixedUpdate()
    {
        if (touchingPlayer == false & dead == false)
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
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            StartCoroutine(Attack());
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
        }
    }

    IEnumerator Attack()
    {
        if (dead == false)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(1.1f);
            if (touchingPlayer == true)
            {
                Attacking.Instance.health = Attacking.Instance.health - 1;
                if (Attacking.Instance.health == 0)
                {
                    Attacking.Instance.Die();
                }
            }
        }    
    }

    public void Die()
    {
        StartCoroutine(Died());
    }

    IEnumerator Died()
    {
        dead = true;
        animator.SetTrigger("Die");
        animator.SetBool("Moving", false);
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
