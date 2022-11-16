using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles : MonoBehaviour
{
    public static Collectibles Instance;
    public int stars;
    public TextMeshProUGUI starDisplay;
    public bool[] starList;

    void Start()
    {
        Instance = this;
        stars = 0;
        starList = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false };
    }

    void Update()
    {
        starDisplay.text = ("Stars: " + stars.ToString());
    }

    public void AddStar(string name)
    {
        stars = stars + 1;
        starList[int.Parse(name.Substring(name.Length - 2))] = true;
        Debug.Log(starList[0]);
        Debug.Log(starList[1]);
    }
}
