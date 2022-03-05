using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamons : MonoBehaviour
{
    public AudioClip diamondPickupSound;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 30 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(diamondPickupSound, transform.position);
            Destroy(this.gameObject);     
        }
    }
}
