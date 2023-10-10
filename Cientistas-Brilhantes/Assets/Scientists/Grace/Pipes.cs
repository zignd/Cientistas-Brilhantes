using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };
    public float[] correctRotation;

    [SerializeField]
    bool isPlaced = false;

    int PossibleRots = 1;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        PossibleRots = correctRotation.Length;

        int rand = Random.Range(0, rotations.Length);
        transform.localEulerAngles = new Vector3(0, rotations[rand], 0);

        if (PossibleRots > 1)
        {
            if (Mathf.Approximately(transform.localEulerAngles.y, correctRotation[0]) || Mathf.Approximately(transform.localEulerAngles.y, correctRotation[1]))
            {
                isPlaced = true;
                gameManager.correctMove();
            }
        }
        else
        {
            if (Mathf.Approximately(transform.localEulerAngles.y, correctRotation[0]))
            {
                isPlaced = true;
                gameManager.correctMove();
            }
        }
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 90, 0));

        if (PossibleRots > 1)
        {
            if (Mathf.Approximately(transform.localEulerAngles.y, correctRotation[0]) || Mathf.Approximately(transform.localEulerAngles.y, correctRotation[1]) && isPlaced == false)
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManager.wrongMove();
            }
        }
        else
        {
            if (Mathf.Approximately(transform.localEulerAngles.y, correctRotation[0]) && isPlaced == false)
            {
                isPlaced = true;
                gameManager.correctMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManager.wrongMove();
            }
        }
    }
}
