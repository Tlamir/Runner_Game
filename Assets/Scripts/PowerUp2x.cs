using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUp2x : MonoBehaviour
{
    public TMP_Text text;
    public AudioClip diamondPickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(diamondPickupSound, transform.position);
            StartCoroutine(PowerUp(other.gameObject));
            this.GetComponent<Renderer>().enabled = false;
        }
    }

    IEnumerator PowerUp(GameObject gameObject)
    {
        text.text = "2x Diamonds Lets Gooo ";
        gameObject.gameObject.GetComponent<PlayerController>().powerUpMultipler = 2;
        yield return new WaitForSeconds(3f);
        gameObject.gameObject.GetComponent<PlayerController>().powerUpMultipler = 1;
        text.text = "";

    }
}
