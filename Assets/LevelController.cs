using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles functionality of controlling the level (ie; spawn platforms, add scores, spawn player, etc.)
/// </summary>
public class LevelController : MonoBehaviour {

    public static LevelController instance;

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
    public GameObject[] obstacles;
    public GameObject coin;

    public ParticleSystem distantStars;
    public ParticleSystem stars;

    private int levelScore;
    private float timer;
    private UIController uIController;
    private bool updateParticleSpeed;
    private int previousHeight;

    private bool inEscapeMenu;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        previousHeight = -1;
        Time.timeScale = 1;
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
        inEscapeMenu = uIController.inEscapeMenu;
    }
	
	void Update () {
        if (uIController != null)
        {
            inEscapeMenu = uIController.inEscapeMenu;
            if (Input.GetKeyDown(KeyCode.Escape) && !inEscapeMenu && !gameOver && !(levelScore >= scoreGoal))
            {
                uIController.ShowEscapeMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && inEscapeMenu)
            {
                uIController.HideEscapeMenu();
            }
        }
        if (gameOver)
        {
            Debug.Log("Lose");
            uIController.ShowGameOverScreen();
        }
        else if(levelScore >= scoreGoal)
        {
            Debug.Log("Win");
            uIController.ShowWinScreen();
            Time.timeScale = 0;
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
            if (Mathf.Abs(height - previousHeight) > 1)
            {
                //bring height closer to previous height
                height = Mathf.FloorToInt((height + previousHeight) / 2);
            }
            previousHeight = height;

            Instantiate(platform, grid.CellToWorld(new Vector3Int(x, height, 0)), Quaternion.identity);

            //spawn obstacles
            if(Random.Range(1, 10) < 5)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length - 1)].gameObject, grid.CellToWorld(new Vector3Int(x + Random.Range(0, 2), height + 1, 0)), Quaternion.identity);
            }

            //spawn coins above platform
            //25% chance to have coins on a platform
            if(Random.Range(1, 10) < 4)
            {
                int numCoins = Random.Range(3, 7);
                int heightOffset = Random.Range(1, 3);
                int xOffset = Random.Range(0, 2);
                switch (numCoins)
                {
                    case 3:
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + 1 + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x - 1 + xOffset, height + heightOffset, 0)));
                        break;
                    case 4:
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + 1 + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x - 1 + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + xOffset, height + heightOffset + 1, 0)));
                        break;
                    case 5:
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + 1 + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x - 1 + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + xOffset, height + heightOffset + 1, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + 1 + xOffset, height + heightOffset + 1, 0)));

                        break;
                    case 6:
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + 1 + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x - 1 + xOffset, height + heightOffset, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + xOffset, height + heightOffset + 1, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x + 1 + xOffset, height + heightOffset + 1, 0)));
                        SpawnCoin(coin, grid.CellToWorld(new Vector3Int(x - 1 + xOffset, height + heightOffset + 1, 0)));
                        break;
                }
            }
            x += Mathf.FloorToInt(Random.Range(platform.transform.localScale.x/2, platform.transform.localScale.x/2 + maxPlatformDist));
        }

    }

    void SpawnCoin(GameObject gameObject, Vector3 location)
    {
        if (Physics2D.Raycast(location, Vector2.down, 0.2f).collider != null)
        {
            Instantiate(coin, location, Quaternion.identity);
        }
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

    public void AddScore()
    {
        levelScore += coinValue;
        OnScoreChanged();
    }
}
