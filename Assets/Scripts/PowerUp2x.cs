using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp2x : MonoBehaviour
{
    public AudioClip diamondPickupSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
            Destroy(this.gameObject);
        }
    }
}
