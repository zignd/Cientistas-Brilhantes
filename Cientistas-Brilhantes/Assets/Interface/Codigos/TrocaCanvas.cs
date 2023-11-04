using UnityEngine;
using UnityEngine.UI;

public class TrocaCanvas : MonoBehaviour
{
    public GameObject elementoASumir; 
    public GameObject elementoAAparecer; 

    public void TrocarElementosUI()
    {
        elementoASumir.SetActive(false);
        elementoAAparecer.SetActive(true);
    }
}
