using System.Collections;
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
        if (!permanentStarDisplay)
        {
            starDisplay.gameObject.SetActive(false);
        }
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
        StartCoroutine(DisplayStars());
    }

    IEnumerator DisplayStars()
    {
        starDisplay.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        starDisplay.gameObject.SetActive(false);
    }
}
