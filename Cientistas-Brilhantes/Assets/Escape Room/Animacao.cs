using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacao : MonoBehaviour
{
    [SerializeField] private Animator objeto = null;
    [SerializeField] private bool openTrigger = false;

    [SerializeField] private string openAnimationName; // Nome da animação de abertura
    [SerializeField] private string closeAnimationName; // Nome da animação de fechamento

    public float maxDistance = 2f; // Defina a distância máxima para a interação.

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
        float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);

        if (!isInteracted && distanceToPlayer <= maxDistance)
        {
            objeto.Play(openAnimationName, 0, 0.0f);

            var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
            // puzzleManager.ValidatePuzzle();

            isInteracted = true;
        }
        else if (isInteracted)
        {
            objeto.Play(closeAnimationName, 0, 0.0f);

            var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
            // puzzleManager.ValidatePuzzle();

            isInteracted = false;
        }
    }
}
