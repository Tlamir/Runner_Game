using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Camera Camera;
    public TMP_Text coinText;
    public TMP_Text collectedLostCoinText;

    private Animator animator;
    private Health healthSytsem;

    public float Speed = 10f;
    public float SwipeSpeed = 10f;
    public float MoveFactorX => _moveFactorX;
    private float xPosMin = -1.81f;
    private float xPosMax = 1.75f;
    private float _moveFactorX;
    private float _lastFrameFingerPositionX;

    [SerializeField]
    private float swerveSpeed = 0.5f;
    [SerializeField]
    private float maxSwerveAmount = 1f;

    public int diamonds = 0;
    public int level = 1;
    public int collectedDiamonds = 0;
    public int powerUpMultipler = 1;

    public bool isGameStarted = false;
    public bool isGameFinished = false;
    public bool isGameOver = false;

    [SerializeField]
    public ParticleSystem collectParticle =null;
    [SerializeField]
    public ParticleSystem hitParticle = null;
    [SerializeField]
    public ParticleSystem diamond5Particle = null;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        healthSytsem = this.GetComponent<Health>();
        powerUpMultipler = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            //Move stright after start
            Vector3 translate = (new Vector3(0, 0, 1) * Time.deltaTime) * Speed;
            transform.Translate(translate);       
        }
        if (!isGameFinished) //Lock Control After victory
        {
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
            else if (Input.GetMouseButtonUp(0) && !isGameFinished)
            {
                _moveFactorX = 0f;
                Vector3 translate = (new Vector3(0, 0, 1) * Time.deltaTime) * Speed;
                transform.Translate(translate);
            }
        }

        //Loose Condition
        if (healthSytsem.health==0)
        {
            isGameFinished = true;
            isGameStarted = false;
            isGameOver = true;
        }
    }

    public void MovePlayer()
    {     
            float swerveAmount = Time.deltaTime * swerveSpeed * MoveFactorX;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
            transform.Translate(swerveAmount, 0, 0);
            //Limit player movment in x axis
            float xPos = Mathf.Clamp(transform.position.x, xPosMin, xPosMax);
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("diamond"))
        {          
            CollectDiamond(other.GetComponent<Diamons>().valueDiamond, collectParticle);
        }
        else if (other.gameObject.CompareTag("diamond5"))
        {           
            CollectDiamond(other.GetComponent<Diamons>().value5Diamond, diamond5Particle);
        }
        else if (other.gameObject.CompareTag("barrier"))
        {
            healthSytsem.health--;
            PlayParticle(hitParticle);
        }
        else if (other.gameObject.CompareTag("Goal"))
        {

            //Win Condition
            isGameFinished = true;
            isGameStarted = false;
            animator.SetBool("IsWon", true);
            animator.SetBool("IsMoving", false);          
            gameObject.transform.Rotate(0, 180, 0);
            Destroy(other);
        }
    }  

    void PlayParticle(ParticleSystem particle)
    {
        particle.Play();
    }

    void CollectDiamond(int diamondType,ParticleSystem particle)
    {
        collectedDiamonds = collectedDiamonds + (diamondType * powerUpMultipler);
        PlayParticle(particle);
    }
}
