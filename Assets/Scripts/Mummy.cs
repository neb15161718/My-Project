using System.Collections;
using UnityEngine;

public class Mummy : Enemy
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
