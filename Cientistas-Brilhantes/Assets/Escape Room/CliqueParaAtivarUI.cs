using UnityEngine;

public class CliqueParaAtivarUI : MonoBehaviour, IInteractable
{
    public GameObject textoUI;
    public GameObject imagemUI;
    public float maxDistance = 2f; // Defina a dist�ncia m�xima para a intera��o.

    private bool uiAtivada = false;

    private void Start()
    {
        textoUI.SetActive(false);
        imagemUI.SetActive(false);
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
    }

    private void AtivarUI()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

        if (!uiAtivada && distanceToPlayer <= maxDistance)
        {
            textoUI.SetActive(true);
            imagemUI.SetActive(true);
            uiAtivada = true;
        }
    }

    private void OnMouseDown()
    {
        AtivarUI();
    }

    public void TriggerInteraction1()
    {
        AtivarUI();
    }

    public void TriggerInteraction2()
    {
        DesativarUI();
    }
}
