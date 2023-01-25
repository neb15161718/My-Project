using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance;
    public GameObject allEnemies;
    public GameObject allStars;
    int enemyCount;
    int diamondCount;

    void Start()
    {
        Instance = this;
        enemyCount = 0;
        diamondCount = 0;
        Star[] starList = allStars.gameObject.GetComponentsInChildren<Star>(true);
    }

    public void EnemyDied(int number)
    {
        if (number >= 0 & number <= 2)
        {
            enemyCount += 1;
        }
        Star[] starList = allStars.gameObject.GetComponentsInChildren<Star>(true);
        foreach (Star stars in starList)
        {
            if (enemyCount == 3)
            {
                if (int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2)) == 0 & Collectibles.starList[0] == "0")
                {
                    stars.gameObject.SetActive(true);
                }
                if (int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2)) == 6 & Collectibles.starList[6] == "0")
                {
                    stars.gameObject.SetActive(true);
                }
            }
        }
    }

    public void DiamondCollected()
    {
        diamondCount += 1;
        Star[] starList = allStars.gameObject.GetComponentsInChildren<Star>(true);
        foreach (Star stars in starList)
        {
            if (diamondCount == 5)
            {
                if (int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2)) == 3 & Collectibles.starList[3] == "0")
                {
                    stars.gameObject.SetActive(true);
                }
            }
        }
    }
}
