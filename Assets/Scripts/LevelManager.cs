using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelManager : MonoBehaviour
{  
    public TMP_Text levelText;
    public TMP_Text menuText;
    public AudioClip gameOverSound;
    public AudioClip winSound;

    private GameObject player;
    private Health healthSytsem;

    public int level;
    private bool isLevelComplated = false;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
        healthSytsem = player.GetComponent<Health>();
        level = PlayerPrefs.GetInt("level");
        levelText.text = "Level "+ (level+1);
        menuText.text = "Touch to start";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLevelComplated)
        {
            if (Input.GetMouseButtonDown(0)) //Game Started
            {
                menuText.text = "";

            }
            if (healthSytsem.health == 0) // Game Over
            {
                menuText.text = "Game Over Restart";
                isLevelComplated = true;
                AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
            }
            if (player.GetComponent<PlayerController>().isGameFinished && !player.GetComponent<PlayerController>().isGameOver) //Level Complated
            {
                menuText.text = "Level completed  Go to next level";
                AudioSource.PlayClipAtPoint(winSound, transform.position);
                level++;
                PlayerPrefs.SetInt("level", level);
                isLevelComplated = true;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (player.GetComponent<PlayerController>().isGameOver) // loose restart the game
            {
                Restart();
            }
            else // Win and go to next level
            {
                if (SceneManager.GetActiveScene().buildIndex + 1>=4)
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                
            }
            
        }

        

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
