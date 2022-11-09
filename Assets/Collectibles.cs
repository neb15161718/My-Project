using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles : MonoBehaviour
{
    public static Collectibles Instance;
    int stars;
    public TextMeshProUGUI starDisplay;

    void Start()
    {
        Instance = this;
        stars = 0;
    }

    void Update()
    {
        starDisplay.text = ("Stars: " + stars.ToString());
    }

    public void AddStar()
    {
        stars = stars + 1;
    }
}
