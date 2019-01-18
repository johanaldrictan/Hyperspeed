using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text scoreText;
    public Text goalText;
    public GameObject InGameCanvas;
    public GameObject GameOverCanvas;

    private void Start()
    {
        GameOverCanvas.GetComponent<CanvasGroup>().interactable = false;
    }
    
    public void SetGoal(int goalScore)
    {
        goalText.text = "Score to Hyperspeed: " + goalScore;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void ShowGameOverScreen()
    {
        InGameCanvas.SetActive(false);
        GameOverCanvas.GetComponent<CanvasGroup>().interactable = true;
        GameOverCanvas.GetComponent<CanvasGroup>().alpha = 1;
    }
    public void ShowWinScreen()
    {

    }
    public void ShowEscapeMenu()
    {

    }
}
