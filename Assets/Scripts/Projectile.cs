using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
        if (other.gameObject != transform.parent.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
