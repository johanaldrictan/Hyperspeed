using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    private GameController gameController;

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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger exit");
        if (collision.CompareTag("Player"))
        {
            gameController.GameOver();
        }
        Destroy(collision.gameObject);
    }
}
