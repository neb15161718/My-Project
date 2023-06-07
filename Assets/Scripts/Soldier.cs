using System.Collections;
using UnityEngine;

public class Soldier : Enemy
{
    new IEnumerator attackCoroutine;
    bool rotate;

    new void Start()
    {
        base.Start();
        attackCoroutine = Attack();
        StartCoroutine(Rotate());
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        if (!dead)
        {
            if (diff < 100 & diff >= 50)
            {
                transform.LookAt(target);
                transform.position = Vector3.MoveTowards(transform.position, target, 0.05f);
                animator.SetBool("Moving", true);
                moving = true;
            }
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
