using UnityEngine;

public class Wardrobe : MonoBehaviour, IInteractable
{
    public GameObject PuzzlePiece;
    private bool isInteracted = false;

    private void CollectPuzzlePiece()
    {
        if (!isInteracted)
        {
            // Desativa o objeto que você interagiu
            gameObject.SetActive(false);

            if (PuzzlePiece != null)
            {
                PuzzlePiece.SetActive(true);
                var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
                puzzleManager.ValidatePuzzle();
            }
            isInteracted = true;
        }
    }

    private void OnMouseDown()
    {
        CollectPuzzlePiece();
    }

    public void TriggerInteraction1()
    {
        CollectPuzzlePiece();
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
