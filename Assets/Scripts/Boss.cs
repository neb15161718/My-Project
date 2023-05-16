using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    public static Boss Instance;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = Mathf.CeilToInt(15 * Attacking.enemyHealthMultiplier);
        touchingPlayer = false;
        dead = false;
        attackCoroutine = Attack();
        attacking = false;
        hit = false;
        moving = false;
        Instance = this;
    }

    new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (!attacking)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = Attack();
            StartCoroutine(attackCoroutine);
        }
    }

    IEnumerator Attack()
    {
        // fix Attack() running twice, maybe separate different enemies into separate scripts
        attacking = true;
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
            yield return new WaitForSeconds(1.5f);
        }
    }

    new public void TakeDamage()
    {
        animator.SetTrigger("Damage");
    }
}
