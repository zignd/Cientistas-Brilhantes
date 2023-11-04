using System.Collections;
using UnityEngine;

public class InicioUI : MonoBehaviour
{
    public GameObject UI1;
    public GameObject UI2;

    private void Start()
    {
        StartCoroutine(ActivateUI1AndSwitch());
    }

    private IEnumerator ActivateUI1AndSwitch()
    {
        UI1.SetActive(true);
        yield return new WaitForSeconds(10f);
        UI1.SetActive(false);

        UI2.SetActive(true);
        yield return new WaitForSeconds(10f);
        UI2.SetActive(false);
    }
}
