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

    public void correctMove()
    {
        VictoryCanvas.SetActive(false);
        correctPipes += 1;

        Debug.Log("Correct Move");

        if(correctPipes == totalPipes || correctPipes == totalPipes - 1)
        {
            Debug.Log("You Win!");
            VictoryCanvas.SetActive(true);
        }
    } 

    public void wrongMove()
    {
        correctPipes -= 1;
    }
}