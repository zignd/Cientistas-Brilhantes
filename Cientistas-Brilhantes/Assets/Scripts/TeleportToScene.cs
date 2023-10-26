using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;  // The name of the scene to load.

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with an object tagged as "LoadSceneTrigger"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with " + gameObject.name);
            LoadScene();
        }
        else
        {
            Debug.Log("Player collided with " + gameObject.name + " but it is not tagged as Player");
        }

    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
