using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles functionality of controlling the level (ie; spawn platforms, add scores, spawn player, etc.)
/// </summary>
public class LevelController : MonoBehaviour {

    private bool gameOver = false;

    public GameObject playerPrefab;
    public float platformSpeed;

    //Level generation
    public Grid grid;
    public GameObject[] platforms;


	// Use this for initialization
	void Start () {
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
            Debug.Log(Time.timeSinceLevelLoad);
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
