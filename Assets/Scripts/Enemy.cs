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
    IEnumerator attackCoroutine;
    bool attacking;
    bool hit;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = 3;
        touchingPlayer = false;
        dead = false;
        attackCoroutine = Attack();
        attacking = false;
        hit = false;
    }

    void FixedUpdate()
    {
        if (touchingPlayer == false & dead == false)
        {
            target = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            Vector3 difference = target - new Vector3(transform.position.x, 0, transform.position.z);
            float diff = difference.sqrMagnitude;
            if (diff < 100)
            {
                transform.LookAt(target);
                transform.position = Vector3.MoveTowards(transform.position, target, 0.05f);
                animator.SetBool("Moving", true);
            }
            else
            {
                animator.SetBool("Moving", false);
            }
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            if (attacking == false)
            {
                StartCoroutine(attackCoroutine);
                attacking = true;
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            attacking = false;
        }
    }

    IEnumerator Attack()
    {
        if (hit == true)
        {
            yield return new WaitForSeconds(3);
            hit = false;
        }
        while (dead == false & touchingPlayer == true)
        {
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(1.1f);
            if (touchingPlayer == true)
            {
                if (Attacking.Instance.health != 0)
                {
                    Attacking.Instance.health = Attacking.Instance.health - 1;
                    Attacking.Instance.TakeDamage();
                }
                if (Attacking.Instance.health == 0)
                {
                    Attacking.Instance.Die();
                }
            }
            yield return new WaitForSeconds(3);
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

    public void TakeDamage()
    {
        animator.SetTrigger("Damage");
        StopCoroutine(attackCoroutine);
        attackCoroutine = null;
        attackCoroutine = Attack();
        hit = true;
        attacking = true;
        StartCoroutine(attackCoroutine);
    }
}
