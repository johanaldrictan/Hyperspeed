using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles functionality of controlling the level (ie; spawn platforms, add scores, spawn player, etc.)
/// </summary>
public class LevelController : MonoBehaviour {

    public GameObject playerPrefab;
    public float platformSpeed;

    private bool gameOver = false;


	// Use this for initialization
	void Start () {
        SpawnPlayer();
	}
	
	void Update () {
        if (gameOver)
        {
            Debug.Log("Lose");
        }
	}

    void SpawnPlayer()
    {
        Instantiate(playerPrefab);
    }

    public void GameOver()
    {
        gameOver = true;
    }


}
