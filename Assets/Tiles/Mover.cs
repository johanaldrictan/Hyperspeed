using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    private Rigidbody2D rb;
    private LevelController levelController;

    // Use this for initialization
    void Start()
    {
        GameObject levelControllerObject = GameObject.FindWithTag("LevelController");
        if (levelControllerObject != null)
        {
            levelController = levelControllerObject.GetComponent<LevelController>();
        }
        if (levelController == null)
        {
            Debug.Log("Cannot find 'LevelController' script");
        }
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * levelController.platformSpeed;
    }
    private void Update()
    {
        rb.velocity = -transform.right * levelController.platformSpeed;
    }
}
