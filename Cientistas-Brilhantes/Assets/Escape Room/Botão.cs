using UnityEngine;

public class Botao : MonoBehaviour, IInteractable
{
    public GameObject PuzzlePiece;
    private bool isInteracted = false;

    [SerializeField] private Animator objeto = null;

    [SerializeField] private string openAnimationName; // = "Abrir Porta 2"; // Nome da animação de abertura

    void Update()
    {

    }
    private void OnMouseDown()
    {
        ToggleButton();
    }

    public void ToggleButton()
    {
        if (!isInteracted)
        {
            if (PuzzlePiece != null)
            {
                objeto.Play(openAnimationName, 0, 0.0f);
            }

            isInteracted = true;
        }
    }

    public void TriggerInteraction1()
    {
        ToggleButton();
    }

    public void TriggerInteraction2()
    {
        // Do nothing
    }

    public void TriggerInteraction3()
    {
        // Do nothing
    }
}
