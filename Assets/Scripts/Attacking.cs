using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Attacking : MonoBehaviour
{
    Animator animator;
    public int healthCap;
    public int health;
    public TextMeshProUGUI healthDisplay;
    Enemy enemy;
    public static Attacking Instance;
    Enemy closest;
    public bool dead;
    public TextMeshProUGUI deadText;
    public Button hubButton;
    public static float healthMultiplier;
    public static float enemyHealthMultiplier;
    IEnumerator attackingCooldown;
    bool attacking;

    void Awake()
    {
        Instance = this;
        if (MainMenu.playerInputActions == null)
        {
            healthMultiplier = 1;
            enemyHealthMultiplier = 1;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        healthCap = Mathf.CeilToInt(5 * healthMultiplier);
        health = healthCap;
        closest = null;
        dead = false;
        attackingCooldown = AttackCooldown();
        attacking = false;
    }

    void Update()
    {
        healthDisplay.text = ("Health: " + health.ToString());
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed & !Pausing.Instance.paused & !dead & !attacking)
        {
            attacking = true;
            attackingCooldown = AttackCooldown();
            StartCoroutine(attackingCooldown);
            animator.SetTrigger("Attack");
            Enemy[] enemyList = StarManager.Instance.allEnemies.GetComponentsInChildren<Enemy>(false);
            foreach (Enemy enemies in enemyList)
            {
                enemy = enemies.GetComponent<Enemy>();
                if (enemy.touchingPlayer & !enemy.dead & enemy is not Mummy)
                {
                    enemy.health--;
                    if (enemy is Boss)
                    {
                        Boss.Instance.TakeDamage();
                    }
                    else
                    {
                        enemy.TakeDamage();
                    }
                }
            }
            closest = FindClosestEnemy();
            if (closest != null)
            {
                transform.LookAt(closest.transform.position);
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1);
        attacking = false;
    }

    Enemy FindClosestEnemy()
    {
        closest = null;
        Enemy[] enemyList = StarManager.Instance.allEnemies.GetComponentsInChildren<Enemy>(false);
        foreach (Enemy enemies in enemyList)
        {
            float distance = Mathf.Infinity;
            Vector3 difference = new Vector3(enemies.transform.position.x, 0, enemies.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
            float diff = difference.sqrMagnitude;
            if (diff < distance & diff < 10 & !enemies.dead & enemies.type != "mummy")
            {
                closest = enemies;
                distance = diff;
            }
        }
        return closest;
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
        deadText.gameObject.SetActive(true);
        hubButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void TakeDamage()
    {
        animator.SetTrigger("Damage");
    }
}
