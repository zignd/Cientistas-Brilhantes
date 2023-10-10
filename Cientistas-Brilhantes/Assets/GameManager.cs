using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject VictoryCanvas;
    public GameObject[] Pipes;

    [SerializeField]
    int totalPipes = 0;

    [SerializeField]
    int correctPipes = 0;

    private bool puzzleCompleted = false; // Verifica se o puzzle foi concluído
    private bool isVictoryCanvasActive = false;

    private bool victoryCanvasVisible = false; // Adicionamos uma variável para rastrear a visibilidade do VictoryCanvas


    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;
        Pipes = new GameObject[totalPipes];

        VictoryCanvas.SetActive(false);

        if (PipesHolder == null)
        {
            Debug.LogError("PipesHolder is null");
        }
        if (VictoryCanvas == null)
        {
            Debug.LogError("Canvas is null");
        }

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        // Verifica se a tecla "E" foi pressionada
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Inverte a visibilidade do VictoryCanvas ao pressionar "E"
            victoryCanvasVisible = !victoryCanvasVisible;
            VictoryCanvas.SetActive(victoryCanvasVisible);
        }

        if (!puzzleCompleted && Input.GetKeyDown(KeyCode.E))
        {
            ToggleVictoryCanvas();
        }
    }

    void ToggleVictoryCanvas()
    {
        isVictoryCanvasActive = !isVictoryCanvasActive;
        VictoryCanvas.SetActive(isVictoryCanvasActive);
    }

    public void correctMove()
    {
        correctPipes += 1;

        Debug.Log("Correct Move");

        if (correctPipes == totalPipes || correctPipes == totalPipes - 1)
        {
            Debug.Log("You Win!");
            puzzleCompleted = true;
            ToggleVictoryCanvas();
        }
    }

    public void wrongMove()
    {
        correctPipes -= 1;
    }
}
