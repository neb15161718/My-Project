using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectibles : MonoBehaviour
{
    public static Collectibles Instance;
    int stars;
    Text starDisplay;

    void Start()
    {
        Instance = this;
        stars = 0;
        starDisplay.text = "H";
    }

    void Update()
    {
        Debug.Log(stars);
        starDisplay.text = stars.ToString();
    }

    public void AddStar()
    {
        stars = stars + 1;
    }
}
