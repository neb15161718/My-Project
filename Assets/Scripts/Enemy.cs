using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    Vector3 target;
    public Animator animator;
    public int health;
    public bool touchingPlayer;
    public bool dead;
    public IEnumerator attackCoroutine;
    public bool attacking;
    public bool hit;
    public string type;
    public GameObject projectile;
    float diff;
    bool rotate;
    public bool moving;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = Mathf.CeilToInt(5 * Attacking.enemyHealthMultiplier);
        touchingPlayer = false;
        dead = false;
        attackCoroutine = Attack();
        attacking = false;
        hit = false;
        moving = false;
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
            if (diff < 100 & (type != "soldier"))
            {
                transform.LookAt(target);
                transform.position = Vector3.MoveTowards(transform.position, target, 0.05f);
                animator.SetBool("Moving", true);
                moving = true;
            }
            else if (diff < 100 & diff >= 50 & type == "soldier")
            {
                transform.LookAt(target);
                transform.position = Vector3.MoveTowards(transform.position, target, 0.05f);
                animator.SetBool("Moving", true);
                moving = true;
            }
            else
            {
                animator.SetBool("Moving", false);
                moving = false;
            }
        }
        animator.SetBool("Moving", false);
        moving = false;
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
            if (rotate == true)
            {
                Vector3 direction = player.transform.position - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.02f);
            }
        }
        if (type == "mummy" & !dead)
        {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
            if (!attacking & (type != "soldier"))
            {
                attackCoroutine = Attack();
                StartCoroutine(attackCoroutine);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
            if (type != "soldier")
            {
                attacking = false;
            }
        }
    }

    IEnumerator Attack()
    {
        if (type == "grunt")
        {
            attacking = true;
            if (hit)
            {
                yield return new WaitForSeconds(3);
                hit = false;
            }
            while (!dead & touchingPlayer)
            {
                animator.SetTrigger("Attack");
                yield return new WaitForSeconds(1.1f);
                if (!dead & touchingPlayer)
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
            attacking = true;
            while (!dead & diff < 50)
            {
                if (!moving)
                {
                    animator.SetTrigger("Attack");
                    GameObject bullet = Instantiate(projectile, transform.position + new Vector3(0, 1.2f, 0), transform.rotation, transform);
                    bullet.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1000);
                }
                yield return new WaitForSeconds(1);
            }
        }
        else if (type == "mummy")
        {
            attacking = true;
            while (!dead & touchingPlayer)
            {
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
        attackCoroutine = Attack();
        hit = true;
        attacking = true;
        StartCoroutine(attackCoroutine);
    }

    IEnumerator Rotate()
    {
        while (!dead & !touchingPlayer)
        {
            rotate = true;
            yield return new WaitForSeconds(1);
            rotate = false;
            yield return new WaitForSeconds(2);
        }
    }
}
