using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacao : MonoBehaviour
{
    [SerializeField] private Animator objeto = null;
    [SerializeField] private bool openTrigger = false;


    [SerializeField] private string openAnimationName; // = "Abrir Porta 2"; // Nome da animação de abertura
    [SerializeField] private string closeAnimationName; // = "Fechar Porta 2"; // Nome da animação de fechamento

    private bool isInteracted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                objeto.Play(openAnimationName, 0, 0.0f);
                //gameObject.SetActive(false);
            }
            
        }
    }

    private void OnMouseDown()
    {
        if (!isInteracted)
        {
            // gameObject.SetActive(false);
            objeto.Play(openAnimationName, 0, 0.0f);

            var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
            // puzzleManager.ValidatePuzzle();

            isInteracted = true;
        }

        else if (isInteracted)
        {
            // gameObject.SetActive(false);
            objeto.Play(closeAnimationName, 0, 0.0f);

            var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
            // puzzleManager.ValidatePuzzle();

            isInteracted = false;
        }
    }
}
