using UnityEngine;

public class Star : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player"))
        {
            Collectibles.Instance.AddStar(gameObject.name);
            gameObject.SetActive(false);
            if (int.Parse((this.gameObject.name.Substring(this.gameObject.name.Length - 2))) % 3 == 0)
            {
                Pausing.Instance.objectiveText1.SetText("\u2713 " + Pausing.Instance.objectiveText1.text);
            }
            if (int.Parse((this.gameObject.name.Substring(this.gameObject.name.Length - 2))) % 3 == 1)
            {
                Pausing.Instance.objectiveText2.SetText("\u2713 " + Pausing.Instance.objectiveText2.text);
            }
            if (int.Parse((this.gameObject.name.Substring(this.gameObject.name.Length - 2))) % 3 == 2)
            {
                Pausing.Instance.objectiveText3.SetText("\u2713 " + Pausing.Instance.objectiveText3.text);
            }
        }
    }
}
