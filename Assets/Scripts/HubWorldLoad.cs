using UnityEngine;

public class HubWorldLoad : MonoBehaviour
{
    void Start()
    {
        if (Collectibles.starList == null)
        {
            Collectibles.starList = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        }
    }
}
