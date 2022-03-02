using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera Camera;
    public float Speed = 10f;
    public float SwipeSpeed = 10f;

    private bool isGameStarted=false;

    private Transform localTrans;
    private Vector3 lastMousePos;
    private Vector3 mousePos;
    private Vector3 newPosForTrans;

     float xPosMin = -1.81f, xPosMax = 1.75f;

    // Start is called before the first frame update
    void Start()
    {
        localTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            localTrans.position += localTrans.forward * Speed *Time.deltaTime;
        }      
        if (Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            //Play Animation Here
        }
        else if (Input.GetMouseButton(0))
        {
            mousePos = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, SwipeSpeed));

            float xDiff = mousePos.x - lastMousePos.x;

            newPosForTrans.x=localTrans.position.x + xDiff;
            newPosForTrans.y = localTrans.position.y;
            newPosForTrans.z = localTrans.position.z;
            localTrans.position = newPosForTrans;
            lastMousePos = mousePos;

            float xPos = Mathf.Clamp(transform.position.x, xPosMin, xPosMax);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }  
    }
}
