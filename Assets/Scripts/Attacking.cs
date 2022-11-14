using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Attacking : MonoBehaviour
{
    Animator animator;
    public int health;
    public TextMeshProUGUI healthDisplay;
    Actions playerInputActions;
    Enemy enemy;
    public static Attacking Instance;
    GameObject closest;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = 10;
        Instance = this;
        closest = null;
    }

    void Update()
    {
        healthDisplay.text = ("Health: " + health.ToString());
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Attack");
            foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy = enemies.GetComponent<Enemy>();
                if (enemy.touchingPlayer == true)
                {
                    enemy.health = enemy.health - 1;
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
}
