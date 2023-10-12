using UnityEngine;

public class Botao : MonoBehaviour
{
    public GameObject PuzzlePiece;
    private bool isInteracted = false;

    [SerializeField] private Animator objeto = null;

    [SerializeField] private string openAnimationName; // = "Abrir Porta 2"; // Nome da animação de abertura

    private void OnMouseDown()
    {
        if (!isInteracted)
        {
            // Desativa o objeto que você interagiu
           // gameObject.SetActive(false);

            if (PuzzlePiece != null)
            {
                objeto.Play(openAnimationName, 0, 0.0f);

                // PuzzlePiece.SetActive(false);
                var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
               // puzzleManager.ValidatePuzzle();
            }

            isInteracted = true;
        }
    }

    void Update()
    {

    }
}
