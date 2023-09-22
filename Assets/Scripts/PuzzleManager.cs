using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject PuzzlePieces;
    public GameObject VictoryCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (PuzzlePieces == null)
        {
            Debug.LogError("PuzzlePieces is null");
        }
        if (VictoryCanvas == null)
        {
            Debug.LogError("VictoryCanvas is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValidatePuzzle()
    {
        var allPiecesActive = true;
        foreach (Transform child in PuzzlePieces.gameObject.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                allPiecesActive = false;
            }
        }

        if (allPiecesActive)
        {
            VictoryCanvas.SetActive(true);
        }
    }
}
