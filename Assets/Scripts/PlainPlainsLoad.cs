using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainPlainsLoad : MonoBehaviour
{
    public GameObject allStars;

    void Start()
    {
        Transform[] starList = allStars.gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform stars in starList)
        {
            if (stars.gameObject.name != ("Stars"))
            {
                stars.gameObject.SetActive(true);
                if (Collectibles.starList[int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))] == "1")
                {
                    stars.gameObject.SetActive(false);
                }
            }
        }
    }

    void Update()
    {
        
    }
}
