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
    private Animator animator;

    [SerializeField]
    private float swerveSpeed = 0.5f;
    [SerializeField]
    private float maxSwerveAmount = 1f;


    private float _lastFrameFingerPositionX;
    private float _moveFactorX;
    public float MoveFactorX => _moveFactorX;
    float xPosMin = -1.81f, xPosMax = 1.75f;

    // Start is called before the first frame update
    void Start()
    {
        localTrans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            Vector3 translate = (new Vector3(0, 0, 1) * Time.deltaTime) * Speed;
            transform.Translate(translate);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            animator.SetBool("IsWon", true);
            animator.SetBool("IsMoving", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            animator.SetBool("IsMoving", true);
            _lastFrameFingerPositionX = Input.mousePosition.x;
            //Play Animation Here

        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
            MovePlayer();

        }  
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
            Vector3 translate = (new Vector3(0, 0, 1) * Time.deltaTime) * Speed;
            transform.Translate(translate);
            //animator.SetBool("IsMoving", false);
        }
    }

    public void MovePlayer()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(swerveAmount , 0, 0);
        //Limit player movment in x axis
        float xPos = Mathf.Clamp(transform.position.x, xPosMin, xPosMax);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("diamond"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("barrier"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            animator.SetBool("IsWon", true);
            animator.SetBool("IsMoving", false);
            isGameStarted = false;
            gameObject.transform.Rotate(0, 180, 0);
            Destroy(other);    
        }

    }
}