using UnityEngine;

public class Heart : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Attacking.Instance.health < Attacking.Instance.healthCap)
            {
                gameObject.SetActive(false);
                Attacking.Instance.health++;
            }
        }
    }
}
