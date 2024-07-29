using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScreen : MonoBehaviour
{
    
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
        
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
