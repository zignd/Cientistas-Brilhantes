using System.Collections;
using UnityEngine;

public class InicioUI : MonoBehaviour
{
    public GameObject UI1;
    public GameObject UI2;

    private void Start()
    {
        //StartCoroutine(ActivateUI1());
        UI1.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UI1.SetActive(false);
            UI2.SetActive(true);

        }
    }

    //private IEnumerator ActivateUI1()
    //{
    //    UI1.SetActive(true);
    //    yield return new WaitForSeconds(10f);
    //    UI1.SetActive(false);
    //}

    //private IEnumerator ActivateUI2()
    //{

    //    UI2.SetActive(true);
    //    yield return new WaitForSeconds(10f);
    //    UI2.SetActive(false);
    //}
}
