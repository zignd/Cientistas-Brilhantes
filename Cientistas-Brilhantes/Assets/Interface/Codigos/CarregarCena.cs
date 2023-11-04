using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarregarCena : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingUIObject;
    public GameObject menuScreen;

   

    public void MostrarImagemECarregarCena()
    {
        //PlayerPrefs.SetString("Nome", "Rafael");

        if (loadingUIObject != null)
        {
            menuScreen.SetActive(false);
            loadingUIObject.SetActive(true); // Torna a imagem de UI visível
        }
        CarregarNovaCena();
    }

    private void CarregarNovaCena()
    {
        SceneManager.LoadScene("Museu"); // Substitua "NovaCena" pelo nome da sua cena
    }
}