using UnityEngine;
using UnityEngine.UI;

public class Musica : MonoBehaviour
{
    public AudioSource musica;
    public Slider sliderVolume; // Arraste o Slider que controla o volume aqui no Inspector

    private void Start()
    {
        musica = FindObjectOfType<AudioSource>(); // Encontra automaticamente o objeto de áudio
        sliderVolume.value = PlayerPrefs.GetFloat("VolumeMusica", 1.0f); // Define o valor inicial do Slider com o volume salvo ou 1.0f se não houver valor salvo
        musica.volume = sliderVolume.value; // Aplica o volume da música
        sliderVolume.onValueChanged.AddListener(AtualizarVolume); // Adiciona um ouvinte para o evento de mudança do Slider
    }

    public void AtualizarVolume(float volume)
    {
        musica.volume = volume; // Atualiza o volume da música com o valor do Slider
        PlayerPrefs.SetFloat("VolumeMusica", volume); // Salva o volume da música nas PlayerPrefs
    }
}
