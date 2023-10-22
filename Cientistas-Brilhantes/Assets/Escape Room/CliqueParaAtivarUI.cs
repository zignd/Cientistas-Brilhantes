using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CliqueParaAtivarUI : MonoBehaviour
{
    public GameObject textoUI;
    public GameObject imagemUI;

    private bool uiAtivada = false;

    private void Start()
    {
        textoUI.SetActive(false);
        imagemUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && uiAtivada)
        {
            textoUI.SetActive(false);
            imagemUI.SetActive(false);
            uiAtivada = false;
        }
    }

    private void OnMouseDown()
    {
        if (!uiAtivada)
        {
            textoUI.SetActive(true);
            imagemUI.SetActive(true);
            uiAtivada = true;
        }
    }
}
