using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    internal static object current;
    public Transform player;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 5, -5);
    }
}
