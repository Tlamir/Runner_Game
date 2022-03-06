using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class CameraController : MonoBehaviour
{
    public GameObject player;        
    Animation cameraAnim;

    private Vector3 offset;            
    private bool isAnimationPlayed=false;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        cameraAnim = this.GetComponent<Animation>();
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        if (player.GetComponent<PlayerController>().isGameFinished && !isAnimationPlayed && !player.GetComponent<PlayerController>().isGameOver)
        {
            cameraAnim.Play();
            isAnimationPlayed = true;
        }
    }
}