using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAtAwake : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
