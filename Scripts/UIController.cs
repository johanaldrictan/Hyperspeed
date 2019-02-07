using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Text scoreText;
    public Text goalText;
    public GameObject InGameCanvas;
    public GameObject GameOverCanvas;
    public GameObject WinScreenCanvas;
    public GameObject EscapeMenuCanvas;

    public GameObject HowToCanvas;
    public GameObject MainMenuCanvas;

    public bool inEscapeMenu;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            inEscapeMenu = false;
            GameOverCanvas.SetActive(false);
            WinScreenCanvas.SetActive(false);
            EscapeMenuCanvas.SetActive(false);
            GameOverCanvas.GetComponent<CanvasGroup>().interactable = false;
            WinScreenCanvas.GetComponent<CanvasGroup>().interactable = false;
            EscapeMenuCanvas.GetComponent<CanvasGroup>().interactable = false;
        }
        else
        {
            HowToCanvas.SetActive(false);
            HowToCanvas.GetComponent<CanvasGroup>().interactable = false;
        }

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
    public void ShowHowTo()
    {
        MainMenuCanvas.SetActive(false);
        HowToCanvas.SetActive(true);
        HowToCanvas.GetComponent<CanvasGroup>().interactable = true;
        HowToCanvas.GetComponent<CanvasGroup>().alpha = 1;
    }
    public void HideHowTo()
    {
        HowToCanvas.GetComponent<CanvasGroup>().alpha = 0;
        HowToCanvas.GetComponent<CanvasGroup>().interactable = false;
        HowToCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
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
