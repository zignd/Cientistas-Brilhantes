using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    public GameObject PuzzlePiece;
    private bool isInteracted = false;

    private void OnMouseDown()
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

    void Update()
    {

    }
}
