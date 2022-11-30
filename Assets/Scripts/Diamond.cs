using UnityEngine;

public class Diamond : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StarManager.Instance.DiamondCollected();
            gameObject.SetActive(false);
        }
    }
}
