using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject PuzzlePieces;
    public GameObject VictoryCanvas;
    public AudioSource victoryAudio; // Referência ao AudioSource

    private bool victoryCanvasVisible = false; // Adicionamos uma variável para rastrear a visibilidade do VictoryCanvas

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
        // Verifica se a tecla "E" foi pressionada
        if (victoryCanvasVisible && Input.GetKeyDown(KeyCode.E))
        {
            // Inverte a visibilidade do VictoryCanvas ao pressionar "E"
            victoryCanvasVisible = !victoryCanvasVisible;
           // VictoryCanvas.SetActive(victoryCanvasVisible);
        }
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
            victoryCanvasVisible = true; // Define a visibilidade do VictoryCanvas como verdadeira
            VictoryCanvas.SetActive(true);

            if (victoryAudio != null)
            {
                victoryAudio.Play(); // Toca o áudio da vitória
            }
        }
    }
}
