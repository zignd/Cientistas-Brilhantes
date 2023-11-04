using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCommand : MonoBehaviour, IInteractable
{
    [SerializeField]
    private MazeBlocksProgramManager mazeBlocksProgramManager;

    [SerializeField]
    private Command command;

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
        mazeBlocksProgramManager.AddCommand(command);
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
