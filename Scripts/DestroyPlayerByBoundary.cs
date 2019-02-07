using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayerByBoundary : MonoBehaviour {

    private LevelController levelController;

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
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelController.GameOver();
            Destroy(collision.gameObject);
        }     
    }
}
