using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaPersistente : MonoBehaviour
{
    private AudioSource musicaPersistente;

    private void Start()
    {
        // Encontra o objeto de áudio da cena persistente
        GameObject objetoPersistente = GameObject.Find("ObjetoDeAudioPersistente");
        if (objetoPersistente != null)
        {
            musicaPersistente = objetoPersistente.GetComponent<AudioSource>();
        }
    }

    public void CarregarNovaCena(string nomeDaCena)
    {
        // Salva o volume da música atual
        float volumeMusicaAtual = musicaPersistente.volume;

        // Carrega a nova cena
        SceneManager.LoadScene(nomeDaCena);

        // Restaura o volume da música após a cena ser carregada
        if (musicaPersistente != null)
        {
            musicaPersistente.volume = volumeMusicaAtual;
        }
    }
}
