using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 offset;
    Vector3 initialPosition; // Adicione uma variável para armazenar a posição inicial
    public string destinationTag = "DropArea";

    void Start()
    {
        initialPosition = transform.position; // Armazena a posição inicial ao iniciar
    }

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag == destinationTag)
            {
                transform.position = hitInfo.transform.position;
            }
            else
            {
                // Se não for solto na área de destino, volta para a posição inicial
                transform.position = initialPosition;
            }
        }
        else
        {
            // Se não houver colisão, volta para a posição inicial
            transform.position = initialPosition;
        }

        transform.GetComponent<Collider>().enabled = true;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetToInitialPosition();
        }
    }

    // Função para redefinir todos os objetos para a posição inicial
    void ResetToInitialPosition()
    {
        transform.position = initialPosition;
    }
}
