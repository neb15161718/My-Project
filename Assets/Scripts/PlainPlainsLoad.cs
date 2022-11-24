using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainPlainsLoad : MonoBehaviour
{
    public GameObject allStars;

    void Start()
    {
        Star[] starList = allStars.gameObject.GetComponentsInChildren<Star>(true);
        foreach (Star stars in starList)
        {
            stars.gameObject.SetActive(true);
            if (Collectibles.starList != null)
            {
                if (Collectibles.starList[int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))] == "1")
                {
                    if (int.Parse((stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))) == 0)
                    {
                        Pausing.Instance.objectiveText1.SetText("\u2713 " + Pausing.Instance.objectiveText1.text);
                    }
                    if (int.Parse((stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))) == 1)
                    {
                        Pausing.Instance.objectiveText2.SetText("\u2713 " + Pausing.Instance.objectiveText3.text);
                    }
                    if (int.Parse((stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))) == 2)
                    {
                        Pausing.Instance.objectiveText3.SetText("\u2713 " + Pausing.Instance.objectiveText3.text);
                    }
                    stars.gameObject.SetActive(false);
                }
            }
        }
        if (Collectibles.starList == null)
        {
            Collectibles.starList = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        }
    }


    void Update()
    {
        
    }
}
