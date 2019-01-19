using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles functionality of controlling the level (ie; spawn platforms, add scores, spawn player, etc.)
/// </summary>
public class LevelController : MonoBehaviour {

    private bool gameOver = false;

    public GameObject playerPrefab;
    public float minPlatformSpeed;
    public float maxPlatformSpeed;
    public float scoreInterval;
    public int scoreGoal;

    public float scoreThreshold;
    public int coinValue;


    public float platformSpeed;

    //Level generation
    public Grid grid;
    public GameObject[] platforms;
    public GameObject coin;

    public ParticleSystem distantStars;
    public ParticleSystem stars;

    private int levelScore;
    private float timer;
    private UIController uIController;
    private bool updateParticleSpeed;
    private int previousHeight;
    

	// Use this for initialization
	void Start () {
        previousHeight = -1;
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
        uIController.SetGoal(scoreGoal);
        OnScoreChanged();
        SpawnTilemap();
        StartCoroutine(SpawnTilemapWithInterval());
        SpawnPlayer();
	}
	
	void Update () {
        
        if (gameOver)
        {
            Debug.Log("Lose");
            uIController.ShowGameOverScreen();
        }
        else if(levelScore == scoreGoal)
        {
            Debug.Log("Win");
            uIController.ShowWinScreen();
        }
        else
        {
            timer += Time.deltaTime;

            if (timer > scoreInterval)
            {
                levelScore ++;
                OnScoreChanged();
                //Reset the timer to 0.
                timer = 0;
            }
        }
	}
    private void LateUpdate()
    {
        //fix particles here
        ChangeParticleSpeed();
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
        //todo: Implement proper random tilemap generation
        //Grid dimensions
        int xMin = 10;
        int xMax = 27;
        int yMin = -5;
        int yMax = 3;

        int maxPlatformDist = 2;


        for (int x = xMin; x <= xMax;)
        {           
            //Pick random platform
            GameObject platform = platforms[Random.Range(0, platforms.Length)];

            //move to platform center
            x += Mathf.FloorToInt(platform.transform.localScale.x/2);

            int height = Mathf.FloorToInt(Random.Range(yMin, yMax));
            if (Mathf.Abs(height - previousHeight) > 2)
            {
                //bring height closer to previous height
                height = Mathf.FloorToInt((height + previousHeight) / 2);
            }
            previousHeight = height;

            Instantiate(platform, grid.CellToWorld(new Vector3Int(x, height, 0)), Quaternion.identity);

            //spawn coins above platform

            //spawn obstacles on platform
            //TODO in future version

            x += Mathf.FloorToInt(Random.Range(platform.transform.localScale.x/2, platform.transform.localScale.x/2 + maxPlatformDist));
        }
        
        //spawn obstacles
        //TODO in future version


    }

    void ChangeSpeed()
    {
        platformSpeed = minPlatformSpeed + (Mathf.Clamp01(levelScore / scoreThreshold) * (maxPlatformSpeed - minPlatformSpeed));
        updateParticleSpeed = true;
    }

    void ChangeParticleSpeed()
    {
        if (updateParticleSpeed)
        {
            updateParticleSpeed = false;
            UpdateParticles(distantStars);
            UpdateParticles(stars);
        }
    }

    void UpdateParticles(ParticleSystem particleSystem)
    {
        ParticleSystem.MainModule psmain = particleSystem.main;
        psmain.simulationSpeed = (float)(platformSpeed);
    }

    void OnScoreChanged()
    {
        uIController.UpdateScore(levelScore);
        ChangeSpeed();
    }

}
