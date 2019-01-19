using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles functionality necessary to running the game itself. (ie; escape menu, continue/quit after loss)
/// </summary>
public class GameController : MonoBehaviour {

    private bool inEscapeMenu;
    private UIController uIController;

    // Use this for initialization
    void Start () {
        GameObject UIControllerObject = GameObject.FindWithTag("UIController");
        if (UIControllerObject != null)
        {
            uIController = UIControllerObject.GetComponent<UIController>();
        }
        if (uIController == null)
        {
            Debug.Log("Cannot find 'UIController' script");
        }
        inEscapeMenu = uIController.inEscapeMenu;
    }
	
	// Update is called once per frame
	void Update () {
        if (uIController != null)
        {
            inEscapeMenu = uIController.inEscapeMenu;
            if (Input.GetKeyDown(KeyCode.Escape) && !inEscapeMenu)
            {
                uIController.ShowEscapeMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && inEscapeMenu)
            {
                uIController.HideEscapeMenu();
            }
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
