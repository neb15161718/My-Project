using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    public Transform player;
    Vector3 target;
    Animator animator;
    public int health;
    public bool touchingPlayer;
    public bool dead;
    IEnumerator attackCoroutine;
    bool attacking;
    bool hit;
    public string type;
    public GameObject projectile;
    float diff;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = Mathf.CeilToInt(5 * Attacking.enemyHealthMultiplier);
        touchingPlayer = false;
        dead = false;
        attackCoroutine = Attack();
        attacking = false;
        hit = false;
        if (type == "soldier")
        {
            StartCoroutine(Rotate());
        }
    }

    void FixedUpdate()
    {
        if (!touchingPlayer & !dead)
        {
            target = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            Vector3 difference = target - new Vector3(transform.position.x, 0, transform.position.z);
            diff = difference.sqrMagnitude;
            if (diff < 100 & type == "grunt")
            {
                transform.LookAt(target);
                transform.position = Vector3.MoveTowards(transform.position, target, 0.05f);
                animator.SetBool("Moving", true);
            }
            else if (diff < 100 & diff >= 50 & type == "soldier")
            {
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
        if (type == "soldier")
        {
            Vector3 difference = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(player.transform.position.x, 0, player.transform.position.z);
            diff = difference.sqrMagnitude;
            if (diff < 50 & !attacking)
            {
                attackCoroutine = Attack();
                StartCoroutine(attackCoroutine);
                attacking = true;
            }
            else if (diff >= 50)
            {
                StopCoroutine(attackCoroutine);
                attacking = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
            if (!attacking & type == "grunt")
            {
                attackCoroutine = Attack();
                StartCoroutine(attackCoroutine);
                attacking = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
            if (type == "grunt")
            {
                attacking = false;
            }
        }
    }

    IEnumerator Attack()
    {
        if (type == "grunt")
        {
            if (hit)
            {
                yield return new WaitForSeconds(3);
                hit = false;
            }
            while (!dead & touchingPlayer)
            {
                animator.SetTrigger("Attack");
                yield return new WaitForSeconds(1.1f);
                if (touchingPlayer)
                {
                    if (Attacking.Instance.health > 0)
                    {
                        Attacking.Instance.health--;
                        Attacking.Instance.TakeDamage();
                    }
                    if (Attacking.Instance.health <= 0)
                    {
                        Attacking.Instance.Die();
                    }
                }
                yield return new WaitForSeconds(3);
            }
        }
        else if (type == "soldier")
        {
            while (!dead & diff < 50)
            {
                GameObject bullet = Instantiate(projectile, transform.position + new Vector3(0, 1.2f, 0), transform.rotation);
                bullet.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1000);
                yield return new WaitForSeconds(1);
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

    IEnumerator Rotate()
    {
        while (!dead & !touchingPlayer)
        {
            Vector3 direction = player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime);
            yield return new WaitForSeconds(3);
        }
    }
}
