using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text scoreText;
    public Text goalText;
    public GameObject InGameCanvas;
    public GameObject GameOverCanvas;
    public GameObject WinScreenCanvas;
    public GameObject EscapeMenuCanvas;

    public bool inEscapeMenu;

    private void Start()
    {
        inEscapeMenu = false;
        GameOverCanvas.SetActive(false);
        WinScreenCanvas.SetActive(false);
        EscapeMenuCanvas.SetActive(false);
        GameOverCanvas.GetComponent<CanvasGroup>().interactable = false;
        WinScreenCanvas.GetComponent<CanvasGroup>().interactable = false;
        EscapeMenuCanvas.GetComponent<CanvasGroup>().interactable = false;

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
        GameOverCanvas.SetActive(true);
        GameOverCanvas.GetComponent<CanvasGroup>().interactable = true;
        GameOverCanvas.GetComponent<CanvasGroup>().alpha = 1;
    }
    public void ShowWinScreen()
    {
        InGameCanvas.SetActive(false);
        WinScreenCanvas.SetActive(true);
        WinScreenCanvas.GetComponent<CanvasGroup>().interactable = true;
        WinScreenCanvas.GetComponent<CanvasGroup>().alpha = 1;
    }
    public void ShowEscapeMenu()
    {
        EscapeMenuCanvas.SetActive(true);
        EscapeMenuCanvas.GetComponent<CanvasGroup>().interactable = true;
        EscapeMenuCanvas.GetComponent<CanvasGroup>().alpha = 1;
        inEscapeMenu = true;
        Time.timeScale = 0;
    }
    public void HideEscapeMenu()
    {
        inEscapeMenu = false;
        Time.timeScale = 1;
        EscapeMenuCanvas.GetComponent<CanvasGroup>().alpha = 0;
        EscapeMenuCanvas.GetComponent<CanvasGroup>().interactable = false;
        EscapeMenuCanvas.SetActive(false);
    }
}
