using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public string sceneName = "Lynn Conway"; // Nome da cena para carregar

    private void OnMouseDown()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
