using System.Collections;
using UnityEngine;

public class Boss : Enemy
{   
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
    }

    IEnumerator Attack()
    {
        int random = Random.Range(0, 2);
        print(random);
        yield return null;
    }
}
