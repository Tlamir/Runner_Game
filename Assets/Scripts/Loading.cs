using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

    void Start()
    {
        if (PlayerPrefs.GetInt("level") + 1 >= 5){

            PlayerPrefs.SetInt("level",0);
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("level")+1);
        }   
    }
}
