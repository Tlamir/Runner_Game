using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUp2x : MonoBehaviour
{
    public AudioClip diamondPickupSound;
    public TMP_Text text;

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(diamondPickupSound, transform.position);
            other.gameObject.GetComponent<PlayerController>().powerUpMultipler = 2;
            text.text = "2x Diamonds Lets Gooo ";
            Destroy(this.gameObject);
        }
    }
}
