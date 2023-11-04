using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePuzzlePieceFound : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject puzzlePiece;

    private void CollectPuzzlePiece()
    {
        puzzlePiece.SetActive(true);
        gameObject.SetActive(false);
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
