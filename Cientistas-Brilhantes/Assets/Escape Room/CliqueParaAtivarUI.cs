using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliqueParaAtivarUI:MonoBehaviour
{
    public GameObject textoUI;
    public GameObject imagemUI;
    public float maxDistance = 2f; // Defina a distância máxima para a interação.

    private bool uiAtivada = false;
    private CharacterController playerController; // Referência ao controlador do jogador

    private void Start()
    {
        textoUI.SetActive(false);
        imagemUI.SetActive(false);

        // Obtém a referência ao controlador do jogador
        playerController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && uiAtivada)
        {
            DesativarUI();
        }
    }

    private void DesativarUI()
    {
        textoUI.SetActive(false);
        imagemUI.SetActive(false);
        uiAtivada = false;
        // Ative o controle do jogador novamente
        playerController.enabled = true;
    }

    private void OnMouseDown()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

        if (!uiAtivada && distanceToPlayer <= maxDistance)
        {
            AtivarUI();
        }
    }

    private void AtivarUI()
    {
        textoUI.SetActive(true);
        imagemUI.SetActive(true);
        uiAtivada = true;
        // Desative o controle do jogador quando a UI está ativa
        playerController.enabled = false;
    }
}
