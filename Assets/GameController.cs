using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject playerPrefab;

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
