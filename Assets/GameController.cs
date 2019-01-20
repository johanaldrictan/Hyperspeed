using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles functionality necessary to running the game itself. (ie; escape menu, continue/quit after loss)
/// </summary>
public class GameController : MonoBehaviour {

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
