using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance;
    public GameObject allEnemies;
    public GameObject allStars;
    int count;

    void Start()
    {
        Instance = this;
        count = 0;
        Star[] starList = allStars.gameObject.GetComponentsInChildren<Star>(true);
        foreach (Star stars in starList)
        {
            if (int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2)) == 0)
            {
                stars.gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        
    }

    public void EnemyDied(int number)
    {
        if (number >= 0 & number <= 2)
        {
            count = count + 1;
        }
        Star[] starList = allStars.gameObject.GetComponentsInChildren<Star>(true);
        foreach (Star stars in starList)
        {
            if (count == 3)
            {
                if (int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2)) == 0)
                {
                    stars.gameObject.SetActive(true);
                }
            }
        }
    }
}
