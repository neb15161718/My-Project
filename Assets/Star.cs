using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player"))
        {
            Collectibles.Instance.AddStar();
        }
    }
}
