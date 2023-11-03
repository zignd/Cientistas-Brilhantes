using UnityEngine;

public class AbrirMenu : MonoBehaviour
{
    public GameObject painelUI; 
    private bool uiVisivel = false;
    private CursorLockMode estadoCursorAnterior; // Armazena o estado anterior do cursor
    private bool cursorVisivelAnterior; // Armazena a visibilidade anterior do cursor

    private void Start()
    {
        EsconderUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Mudança da tecla de ativação
        {
            if (uiVisivel)
            {
                EsconderUI();
            }
            else
            {
                MostrarUI();
            }
        }
    }

    private void MostrarUI()
    {
        // Salva o estado anterior do cursor
        estadoCursorAnterior = Cursor.lockState;
        cursorVisivelAnterior = Cursor.visible;

        // Configura o cursor para ser visível e desbloqueado
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Ativa o painel da UI
        painelUI.SetActive(true);

        // Pausa o jogo (opcional, se desejar)
        Time.timeScale = 0;

        uiVisivel = true;
    }

    private void EsconderUI()
    {
        // Restaura as configurações anteriores do cursor
        Cursor.lockState = estadoCursorAnterior;
        Cursor.visible = cursorVisivelAnterior;

        // Desativa o painel da UI
        painelUI.SetActive(false);

        // Continua o jogo (opcional, se desejar)
        Time.timeScale = 1;

        uiVisivel = false;
    }
}
