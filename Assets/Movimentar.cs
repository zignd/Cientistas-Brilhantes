using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentar : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 offset;
    void OnMouseDown()
    {
        offset = transform.position;
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

}