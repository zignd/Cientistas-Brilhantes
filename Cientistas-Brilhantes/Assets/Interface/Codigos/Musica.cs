using UnityEngine;
using UnityEngine.UI;

public class Musica : MonoBehaviour
{
    private AudioSource musica;
    public Slider sliderVolume; // Arraste o Slider que controla o volume aqui no Inspector

    private void Start()
    {
        musica = FindObjectOfType<AudioSource>(); // Encontra automaticamente o objeto de áudio
        sliderVolume.value = PlayerPrefs.GetFloat("VolumeMusica", 1.0f); // Define o valor inicial do Slider com o volume salvo ou 1.0f se não houver valor salvo
        musica.volume = sliderVolume.value; // Aplica o volume da música
    }

    public void AtualizarVolume()
    {
        musica.volume = sliderVolume.value; // Atualiza o volume da música com o valor do Slider
        PlayerPrefs.SetFloat("VolumeMusica", sliderVolume.value); // Salva o volume da música nas PlayerPrefs
    }
}
