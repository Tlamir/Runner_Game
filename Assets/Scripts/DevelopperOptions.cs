using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DevelopperOptions : MonoBehaviour
{
    public Button increaseHealth;
    public Button decraseHealth;
    public Button increaseDiamondMultipler;
    public Button decraseDiamondMultipler;
    // Start is called before the first frame update
    private GameObject Player;

    public bool Activate_Developper_Settings=false;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (!Activate_Developper_Settings)
        {
            increaseHealth.gameObject.SetActive(false);
            decraseHealth.gameObject.SetActive(false);
            increaseDiamondMultipler.gameObject.SetActive(false);
            decraseDiamondMultipler.gameObject.SetActive(false);
        }
    }
   public void IncreaseHealth()
    {
        Player.GetComponent<Health>().health++;
    }

    public void IncreaseDiamondMulitpler()
    {
        Player.GetComponent<PlayerController>().powerUpMultipler++;
    }

    public void DecraseHealth()
    {
        Player.GetComponent<Health>().health--;
    }

    public void DecraseDiamondMulitpler()
    {
        Player.GetComponent<PlayerController>().powerUpMultipler--;
    }
}
