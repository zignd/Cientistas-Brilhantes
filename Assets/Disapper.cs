using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearReappear : MonoBehaviour
{
    private Renderer blockRenderer;
    private bool isBlockVisible = true;

    void Start()
    {
        blockRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Verifique se a tecla espaço foi pressionada
        {
            ToggleVisibility();
        }
    }

    void ToggleVisibility()
    {
        isBlockVisible = !isBlockVisible;
        blockRenderer.enabled = isBlockVisible;
    }
}
