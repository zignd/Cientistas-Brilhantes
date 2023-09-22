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

        int rand = Random.Range(0,rotations.Length);
        transform.eulerAngles = new Vector3(0, rotations[rand], 0);

        if(PossibleRots > 1)
        {
            if (transform.eulerAngles.y == correctRotation[0] || transform.eulerAngles.y == correctRotation[1])
            {
                isPlaced = true;
                gameManager.correctMove();
            }
        }
        else
        {
            if (transform.eulerAngles.y == correctRotation[0])
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
            if (transform.eulerAngles.y == correctRotation[0] || transform.eulerAngles.y == correctRotation[1] && isPlaced == false)
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
            if (transform.eulerAngles.y == correctRotation[0] && isPlaced == false)
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
