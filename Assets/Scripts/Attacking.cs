using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class Attacking : MonoBehaviour
{
    Animator animator;
    public int health;
    public TextMeshProUGUI healthDisplay;
    Actions playerInputActions;
    Enemy enemy;
    public static Attacking Instance;
    GameObject closest;
    public bool dead;
    Rigidbody characterRigidBody;
    public TextMeshProUGUI deadText;
    public Button hubButton;
    Pausing pausing;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = 5;
        Instance = this;
        closest = null;
        dead = false;
        characterRigidBody = GetComponent<Rigidbody>();
        deadText.gameObject.SetActive(false);
        pausing = GetComponent<Pausing>();
    }

    void Update()
    {
        healthDisplay.text = ("Health: " + health.ToString());
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed & !pausing.paused & !dead)
        {
            animator.SetTrigger("Attack");
            foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy = enemies.GetComponent<Enemy>();
                if (enemy.touchingPlayer == true)
                {
                    enemy.health = enemy.health - 1;
                    enemy.TakeDamage();
                    if (enemy.health == 0)
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

    GameObject FindClosestEnemy()
    {
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float distance = Mathf.Infinity;
            Vector3 difference = new Vector3(enemies.transform.position.x, 0, enemies.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
            float diff = difference.sqrMagnitude;
            if (diff < distance & diff < 10)
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
