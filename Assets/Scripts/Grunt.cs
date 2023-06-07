using System.Collections;
using UnityEngine;

public class Grunt : Enemy
{
    new IEnumerator attackCoroutine;

    new void Start()
    {
        base.Start();
        attackCoroutine = Attack();
    }

    new void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (!attacking)
        {
            attackCoroutine = Attack();
            StartCoroutine(attackCoroutine);
        }
    }

    IEnumerator Attack()
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

    new public void TakeDamage()
    {
        if (health <= 0)
        {
            StartCoroutine(Die());
            StarManager.Instance.EnemyDied(int.Parse(name.Substring(name.Length - 2)));
        }
        animator.SetTrigger("Damage");
        StopCoroutine(attackCoroutine);
        attackCoroutine = Attack();
        hit = true;
        attacking = true;
        StartCoroutine(attackCoroutine);
    }

    IEnumerator Die()
    {
        dead = true;
        animator.SetTrigger("Die");
        animator.SetBool("Moving", false);
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
