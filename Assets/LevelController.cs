using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles functionality of controlling the level (ie; spawn platforms, add scores, spawn player, etc.)
/// </summary>
public class LevelController : MonoBehaviour {

    private bool gameOver = false;

    public GameObject playerPrefab;
    public float startingPlatformSpeed;
    public float maxPlatformSpeed;
    public float scoreInterval;
    public float scoreGoal;

    public float platformSpeed;

    //Level generation
    public Grid grid;
    public GameObject[] platforms;

    private int levelScore;
    private float timer;
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
        levelScore = 0;
        uIController.UpdateScore(levelScore);
        SpawnTilemap();
        StartCoroutine(SpawnTilemapWithInterval());
        SpawnPlayer();
	}
	
	void Update () {
        if (gameOver)
        {
            Debug.Log("Lose");
        }
        else
        {
            timer += Time.deltaTime;

            if (timer > scoreInterval)
            {
                levelScore ++;
                uIController.UpdateScore(levelScore);
                //Reset the timer to 0.
                timer = 0;
            }
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

    IEnumerator SpawnTilemapWithInterval()
    {
        while (!gameOver)
        {
            //wait till current level has passed to generate new level elements
            yield return new WaitForSeconds((float)(17.8 / platformSpeed));
            SpawnTilemap();
        }
    }
    /// <summary>
    /// Spawns tilemap offscreen
    /// </summary>
    void SpawnTilemap()
    {
        Instantiate(platforms[0], grid.CellToWorld(new Vector3Int(18, -1, 0)), Quaternion.identity);
    }

}
