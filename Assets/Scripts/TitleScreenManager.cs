using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void StartNewGame()
    {
        // Load the game scene or perform any other necessary actions
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
