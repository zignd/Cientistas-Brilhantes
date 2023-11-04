using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Desaparecer : MonoBehaviour
{
    public GameObject UI1;
    public GameObject UI2;
    public GameObject UI3;

    //private bool isInteracted = false;
    private bool uiAtivada = false;

    private void Start()
    {
        UI1.SetActive(false);
        UI2.SetActive(false);
        UI3.SetActive(false);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape)
    //        && uiAtivada == true)
    //    {
    //        DesativarUI();
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        //commandSlots[i].transform.Find("Text").GetComponent<TextMeshPro>().text = CommandToText(currentCommands[i]);

        
        if (other.CompareTag("Player")) // && !uiAtivada)
        {
            
            UI3.SetActive(true);
            UI1.SetActive(true);
          //  UI1.SetActive(false);

            //StartCoroutine(ActivateUI1AndSwitch());  
            // gameObject.SetActive(false);      
        }
    }

    //private IEnumerator DelayedFunction()
    //{
    //    yield return new WaitForSeconds(8f);
    //}

    private IEnumerator ActivateUI1AndSwitch()
    {

        UI1.SetActive(true);
        //StartCoroutine(DelayedFunction());
        yield return new WaitForSeconds(8f);
        UI1.SetActive(false);

        UI2.SetActive(true);
        yield return new WaitForSeconds(8f);
        //StartCoroutine(DelayedFunction());
        UI2.SetActive(false);

    }

    private void DesativarUI()
    {
        UI1.SetActive(false);
        UI2.SetActive(false);
        uiAtivada = false;
    }

    
}