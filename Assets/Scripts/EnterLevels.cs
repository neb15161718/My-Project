using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnterLevels : MonoBehaviour
{
    public TMP_Text alertText;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlainPlainsEntrance")
        {
            SceneManager.LoadScene("PlainPlains");
        }
        else if (other.gameObject.name == "CloudyCloudsEntrance")
        {
            if (Collectibles.stars >= 2)
            {
                SceneManager.LoadScene("CloudyClouds");
            }
            else
            {
                alertText.text = "You need 2 stars to enter this level";
                StartCoroutine(DisplayAlert());
            }
        }
        else if (other.gameObject.name == "FutureFacilityEntrance")
        {
            if (Collectibles.stars >= 4)
            {
                SceneManager.LoadScene("FutureFacility");
            }
            else
            {
                alertText.text = "You need 4 stars to enter this level";
                StartCoroutine(DisplayAlert());
            }
        }
    }

    IEnumerator DisplayAlert()
    {
        alertText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        alertText.gameObject.SetActive(false);
    }
}
