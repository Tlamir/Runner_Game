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
    public TMP_Text collectedLostCoinText;
    public TMP_Text coinText;
    public AudioClip gameOverSound;
    public AudioClip winSound;

    private GameObject player;
    private Health healthSytsem;

    public int level;
    private bool isLevelComplated = false;

    int diamonds;
    int collectedLostDiamonds;

    public string GameOverText = "Game Over Restart";
    public string WinText = "Level Complated Go To Next Level";

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
        healthSytsem = player.GetComponent<Health>();

        level = PlayerPrefs.GetInt("level");
        diamonds = PlayerPrefs.GetInt("diamond");
        levelText.text = "Level "+ (level+1);
        menuText.text = "Touch to start";
        
    }

    // Update is called once per frame
    void Update()
    {
        
        collectedLostDiamonds = player.GetComponent<PlayerController>().collectedDiamonds;

        if (!isLevelComplated)
        {
            
            if (Input.GetMouseButtonDown(0)) //Game Started
            {
                menuText.text = "";

            }
            if (healthSytsem.health == 0) // Game Over
            {
                EndGame(GameOverText, gameOverSound);

                collectedLostCoinText.text = "You lost " + collectedLostDiamonds;
                
            }
            if (player.GetComponent<PlayerController>().isGameFinished && !player.GetComponent<PlayerController>().isGameOver) //Level Complated
            {
                level++;
                EndGame(WinText, winSound); 
                SaveProgress(level, (diamonds + collectedLostDiamonds));
                collectedLostCoinText.text = "You Collected " + collectedLostDiamonds;
            }
            coinText.text = "Diamonds: " + (diamonds + collectedLostDiamonds);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (player.GetComponent<PlayerController>().isGameOver) // loose restart the game
            {      
                Restart();
            }
            else // Win and go to next level
            {
                LoadNextLevel();
            }        
        }

    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void EndGame(string endingText,AudioClip audio)
    {
        menuText.text = endingText;
        isLevelComplated = true;
        AudioSource.PlayClipAtPoint(audio, player.transform.position);
    }

    void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 >= 5)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void SaveProgress(int levels , int diamondsTotal)
    {
        PlayerPrefs.SetInt("level", levels);
        PlayerPrefs.SetInt("diamond", diamondsTotal);
    }
}
