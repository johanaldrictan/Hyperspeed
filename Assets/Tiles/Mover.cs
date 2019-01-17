using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    private Rigidbody2D rb;
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * gameController.platformSpeed;
    }
    private void Update()
    {
        rb.velocity = -transform.right * gameController.platformSpeed;
    }
}
