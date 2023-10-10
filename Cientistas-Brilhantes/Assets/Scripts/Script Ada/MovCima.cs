using UnityEngine;

public class MovCima: MonoBehaviour
{
    Vector3 offset;
    Vector3 initialPosition;
    public GameObject objetoMovimentavel; // Objeto a ser movido para cima.

    void Start()
    {
        initialPosition = transform.position;
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
            if (hitInfo.transform.CompareTag("DropArea"))
            {
                // Verifique se a DropArea contém uma peça com rótulo "Cima".
                var dropArea = hitInfo.transform;
                foreach (Transform child in dropArea)
                {
                    if (child.CompareTag("Cima"))
                    {
                        // Mova o objetoMovimentavel para cima.
                        MoveObjeto(objetoMovimentavel, Vector3.up);
                    }
                }

                // Movimente a peça arrastada para a posição da DropArea.
                transform.position = dropArea.position;
            }
            else
            {
                transform.position = initialPosition;
            }
        }
        else
        {
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

    void ResetToInitialPosition()
    {
        transform.position = initialPosition;
    }

    void MoveObjeto(GameObject objeto, Vector3 direction)
    {
        objeto.transform.Translate(direction);
    }
}
