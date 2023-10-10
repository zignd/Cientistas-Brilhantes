using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndReturn : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Velocidade de movimento
    private Vector3 originalPosition; // Posição original do objeto

    private bool isAtOriginalPosition = true; // Verifica se o objeto está na posição original

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isAtOriginalPosition)
            {
                TeleportToNewPosition(); // Chama a função para teleportar para uma nova posição
                isAtOriginalPosition = false;
            }
            else
            {
                ReturnToOriginalPosition(); // Chama a função para retornar à posição original
                isAtOriginalPosition = true;
            }
        }
    }

    void TeleportToNewPosition()
    {
        // Gere uma nova posição aleatória (por exemplo, dentro de um raio)
        Vector3 newPosition = originalPosition + new Vector3(Random.Range(100f, 110f), 0f, Random.Range(100f, 110f));

        // Teleporta o objeto para a nova posição
        transform.position = newPosition;
    }

    void ReturnToOriginalPosition()
    {
        // Retorna o objeto para a posição original
        transform.position = originalPosition;
    }
}
