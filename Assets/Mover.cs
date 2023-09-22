using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndReturn : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Velocidade de movimento
    private Vector3 originalPosition; // Posição original do objeto
    private bool isMoving = false; // Verifica se o objeto está se movendo

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleMovement();
        }

        if (isMoving)
        {
            // Movimenta o objeto para a direita
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    void ToggleMovement()
    {
        isMoving = !isMoving;

        // Se o objeto estiver se movendo, define a posição original como a posição atual
        if (isMoving)
        {
            originalPosition = transform.position;
        }
        else
        {
            // Se o objeto não estiver se movendo, retorna à posição original
            transform.position = originalPosition;
        }
    }
}
