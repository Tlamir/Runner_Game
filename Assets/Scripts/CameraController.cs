using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class CameraController : MonoBehaviour
{

    public GameObject player;        //Public variable to store a reference to the player game object
    Animation cameraAnim;


    private Vector3 offset;            //Private variable to store the offset distance between the player and camera
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
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;

        if (player.GetComponent<PlayerController>().isGameFinished && !isAnimationPlayed && !player.GetComponent<PlayerController>().isGameOver)
        {
            cameraAnim.Play();
            isAnimationPlayed = true;
        }
    }

}