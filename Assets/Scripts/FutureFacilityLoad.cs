using UnityEngine;

public class FutureFacilityLoad : MonoBehaviour
{
    void Start()
    {
        Star[] starList = StarManager.Instance.allStars.gameObject.GetComponentsInChildren<Star>(true);
        foreach (Star stars in starList)
        {
            if (int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2)) == 6)
            {
                stars.gameObject.SetActive(false);
            }
            else
            {
                stars.gameObject.SetActive(true);
            }
            if (Collectibles.starList != null)
            {
                if (Collectibles.starList[int.Parse(stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))] == "1")
                {
                    if (int.Parse((stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))) % 3 == 0)
                    {
                        Pausing.Instance.objectiveText1.SetText("\u2713 " + Pausing.Instance.objectiveText1.text);
                    }
                    if (int.Parse((stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))) % 3 == 1)
                    {
                        Pausing.Instance.objectiveText2.SetText("\u2713 " + Pausing.Instance.objectiveText2.text);
                    }
                    if (int.Parse((stars.gameObject.name.Substring(stars.gameObject.name.Length - 2))) % 3 == 2)
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
}
