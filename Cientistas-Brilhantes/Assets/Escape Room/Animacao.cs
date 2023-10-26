using UnityEngine;

public class Animacao : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator objeto = null;
    [SerializeField] private bool openTrigger = false;

    [SerializeField] private string openAnimationName; // Nome da animação de abertura
    [SerializeField] private string closeAnimationName; // Nome da animação de fechamento

    public float maxDistance = 2f; // Defina a distância máxima para a interação.

    private bool isInteracted = false;

    private void ToggleAnimation()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

        if (!isInteracted && distanceToPlayer <= maxDistance)
        {
            objeto.Play(openAnimationName, 0, 0.0f);

            isInteracted = true;
        }
        else if (isInteracted)
        {
            objeto.Play(closeAnimationName, 0, 0.0f);

            isInteracted = false;
        }
    }

    private void OnMouseDown()
    {
        ToggleAnimation();
    }

    public void TriggerInteraction1()
    {
        ToggleAnimation();
    }

    public void TriggerInteraction2()
    {
        // Do nothing
    }

    public void TriggerInteraction3()
    {
        // Do nothing
    }
}
