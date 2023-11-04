using UnityEngine;

public class Wardrobe2 : MonoBehaviour
{
    public GameObject PuzzlePiece;
    public GameObject Aparecer;
    private bool isInteracted = false;

    private void OnMouseDown()
    {
        if (!isInteracted)
        {
            // Desativa o objeto que você interagiu
            gameObject.SetActive(false);

            if (PuzzlePiece != null)
            {
                PuzzlePiece.SetActive(false);
                Aparecer.SetActive(true);
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
