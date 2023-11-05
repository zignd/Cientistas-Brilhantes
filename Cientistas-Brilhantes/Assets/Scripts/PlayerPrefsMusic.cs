using UnityEngine;

public class PlayerPrefsMusic : MonoBehaviour
{
    public AudioSource musica;

    private void Start()
    {
        musica = FindObjectOfType<AudioSource>(); // Encontra automaticamente o objeto de áudio
        float volume = PlayerPrefs.GetFloat("VolumeMusica", 1.0f); // Recupera o valor do volume das PlayerPrefs
        musica.volume = volume; // Aplica o volume da música
    }
}
