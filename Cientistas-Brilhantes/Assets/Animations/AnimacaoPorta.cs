using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPorta : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private GameObject Aparecer = null;

    [SerializeField] private string openAnimationName; // = "Abrir Porta 2"; // Nome da animação de abertura
    [SerializeField] private string closeAnimationName; // = "Fechar Porta 2"; // Nome da animação de fechamento

    private bool isInteracted = false;

    private void CollectKey()
    {
        if (!isInteracted)
        {
            gameObject.SetActive(false);
            myDoor.Play(openAnimationName, 0, 0.0f);
            Aparecer.SetActive(true);

            isInteracted = true;
        }
    }

    private void OnMouseDown()
    {
        CollectKey();
    }

    public void TriggerInteraction1()
    {
        CollectKey();
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