using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowImageOnCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject imageToShow; // Referência ao objeto da imagem que você deseja mostrar.
    [SerializeField]
    private string sceneToLoad;    // Nome da cena para carregar (se necessário).

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o jogador colidiu com um objeto marcado como "LoadSceneTrigger"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with " + gameObject.name);

            // Verifica se há um objeto de imagem para mostrar.
            if (imageToShow != null)
            {
                imageToShow.SetActive(true); // Ativa o objeto da imagem.
            }

            // Se necessário, carrega a cena especificada.
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                LoadScene();
            }
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
