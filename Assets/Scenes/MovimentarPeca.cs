using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPeca : MonoBehaviour
{
    public GameObject objectToDrag;
    public GameObject objectDragToPos;

    // O objeto vai para  posição se estiver perto o suficiente
    public float DropDistance;

    // Verifica se está no lugar certo
    public bool isLocked;

    // Adicione um contador para rastrear as peças no lugar certo
    private static int piecesInPlace = 0;
    private static int totalPieces; // Defina este valor no Inspector

    Vector2 objectInitPos;

    // Start is called before the first frame update
    void Start()
    {
        objectInitPos = objectToDrag.transform.position;
        totalPieces++; // Incrementa o número total de peças
    }

    public void DragObject()
    {
        if (!isLocked)
        {
            // o mouse movimenta o objeto
            objectToDrag.transform.position = Input.mousePosition;
        }
    }

    public void DropObject()
    {
        float Distance = Vector3.Distance(objectToDrag.transform.position, objectDragToPos.transform.position);

        if (Distance < DropDistance)
        {
            // está na posição correta
            isLocked = true;
            Debug.Log("Correct Move");

            // o objeto que estamos arrastando está na posição correta
            objectToDrag.transform.position = objectDragToPos.transform.position;

            // Incrementa o contador de peças no lugar certo
            piecesInPlace++;

            // Verifica se todas as peças estão no lugar certo
            if (piecesInPlace == totalPieces)
            {
                Debug.Log("Todas as peças estão no lugar certo!");
            }
        }
        else
        {
            objectToDrag.transform.position = objectInitPos;
        }
    }
}
