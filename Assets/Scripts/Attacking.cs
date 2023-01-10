using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Attacking : MonoBehaviour
{
    Animator animator;
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
        health = Mathf.CeilToInt(5 * healthMultiplier);
        closest = null;
        dead = false;
    }

    void Update()
    {
        healthDisplay.text = ("Health: " + health.ToString());
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed & !Pausing.Instance.paused & !dead)
        {
            animator.SetTrigger("Attack");
            Enemy[] enemyList = StarManager.Instance.allEnemies.GetComponentsInChildren<Enemy>(false);
            foreach (Enemy enemies in enemyList)
            {
                enemy = enemies.GetComponent<Enemy>();
                if (enemy.touchingPlayer)
                {
                    enemy.health--;
                    enemy.TakeDamage();
                    if (enemy.health <= 0)
                    {
                        enemy.Die();
                        StarManager.Instance.EnemyDied(int.Parse(enemy.name.Substring(enemy.name.Length - 2)));
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

    Enemy FindClosestEnemy()
    {
        closest = null;
        Enemy[] enemyList = StarManager.Instance.allEnemies.GetComponentsInChildren<Enemy>(false);
        foreach (Enemy enemies in enemyList)
        {
            float distance = Mathf.Infinity;
            Vector3 difference = new Vector3(enemies.transform.position.x, 0, enemies.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
            float diff = difference.sqrMagnitude;
            if (diff < distance & diff < 10 & !enemies.dead)
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
