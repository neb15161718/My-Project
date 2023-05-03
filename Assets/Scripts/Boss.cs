using UnityEngine;

public class Boss : Enemy
{
    bool fightStarted;

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

    void Update()
    {
        if (player.position.z >= 20)
        {
            fightStarted = true;
        }
    }
}
