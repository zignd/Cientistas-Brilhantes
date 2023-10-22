using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPorta : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private GameObject Aparecer = null;

    [SerializeField] private string openAnimationName; // = "Abrir Porta 2"; // Nome da animação de abertura
    [SerializeField] private string closeAnimationName; // = "Fechar Porta 2"; // Nome da animação de fechamento

    private bool isInteracted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play(openAnimationName, 0, 0.0f);
                gameObject.SetActive(false);
            }
            else if (closeTrigger)
            {
                myDoor.Play(closeAnimationName, 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isInteracted)
        {
            gameObject.SetActive(false);
            myDoor.Play(openAnimationName, 0, 0.0f);
            Aparecer.SetActive(true);

            var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
            // puzzleManager.ValidatePuzzle();

            isInteracted = true;
        }
    }
}