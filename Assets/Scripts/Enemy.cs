using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Vector3 target;
    public Animator animator;
    public int health;
    public bool touchingPlayer;
    public bool dead;
    public IEnumerator attackCoroutine;
    public bool attacking;
    public bool hit;
    public string type;
    public GameObject projectile;
    public float diff;
    public bool moving;
    public Grunt grunt;
    public Soldier soldier;

    public void Start()
    {
        animator = GetComponent<Animator>();
        health = Mathf.CeilToInt(5 * Attacking.enemyHealthMultiplier);
        touchingPlayer = false;
        dead = false;
        attacking = false;
        hit = false;
        moving = false;
    }

    public void FixedUpdate()
    {
        if (!touchingPlayer & !dead)
        {
            target = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            Vector3 difference = target - new Vector3(transform.position.x, 0, transform.position.z);
            diff = difference.sqrMagnitude;
            if (diff < 100 & (this is not Soldier))
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
        if (!dead)
        {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
            if (this is not Soldier)
            {
                attacking = false;
            }
        }
    }

    public void TakeDamage()
    {
        if (this is Grunt)
        {
            grunt.TakeDamage();
        }
        if (this is Soldier)
        {
            soldier.TakeDamage();
        }
    }
}
