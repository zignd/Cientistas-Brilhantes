using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunProgram : MonoBehaviour, IInteractable
{
    [SerializeField]
    private MazeBlocksProgramManager mazeBlocksProgramManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        TriggerInteraction1();
    }

    public void TriggerInteraction1()
    {
        mazeBlocksProgramManager.RunProgram();
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
