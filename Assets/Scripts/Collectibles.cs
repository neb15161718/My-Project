using UnityEngine;
using TMPro;

public class Collectibles : MonoBehaviour
{
    public static Collectibles Instance;
    public static int stars;
    public TextMeshProUGUI starDisplay;
    public static string[] starList;
    public static bool permanentStarDisplay;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (starDisplay != null)
        {
            starDisplay.text = ("Stars: " + stars.ToString());
        }
    }

    public void AddStar(string name)
    {
        stars = stars + 1;
        starList[int.Parse(name.Substring(name.Length - 2))] = "1";
    }
}
