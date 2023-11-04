using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aparecer : MonoBehaviour
{
    

    [SerializeField] private GameObject Aparecer1 = null;

    

    private bool isInteracted = false;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (openTrigger)
    //        {
    //            myDoor.Play(openAnimationName, 0, 0.0f);
    //            gameObject.SetActive(false);
    //        }
    //        else if (closeTrigger)
    //        {
    //            myDoor.Play(closeAnimationName, 0, 0.0f);
    //            gameObject.SetActive(false);
    //        }
    //    }
    //}

    private void OnMouseDown()
    {
        if (!isInteracted)
        {
            gameObject.SetActive(false);
            //myDoor.Play(openAnimationName, 0, 0.0f);
            Aparecer1.SetActive(true);

            var puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
            // puzzleManager.ValidatePuzzle();

            isInteracted = true;
        }
    }
}