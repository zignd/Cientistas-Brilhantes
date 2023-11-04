using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum Command
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class MazeBlocksProgramManager : MonoBehaviour
{
    [SerializeField]
    private PuzzleManager puzzleManager;

    [Header("Program Stack")]

    [SerializeField]
    private GameObject[] commandSlots = new GameObject[6];

    [SerializeField]
    private Material usedCommandSlotMaterial;

    [SerializeField]
    private Material emptyCommandSlotMaterial;

    [Header("Commands Toolkit")]

    [SerializeField]
    private GameObject CommandUp;

    [SerializeField]
    private GameObject CommandDown;

    [SerializeField]
    private GameObject CommandLeft;

    [SerializeField]
    private GameObject CommandRight;

    [Header("Program Execution")]

    [SerializeField]
    private GameObject pawn;

    private Vector3 pawnInitialPosition = new Vector3(-0.559099972f, 0.0631000027f, 0.1954f);

    [SerializeField]
    private GameObject programError;

    [SerializeField]
    private GameObject programSuccess;

    [Header("Debug")]

    [SerializeField]
    private List<Command> currentCommands = new List<Command>();

    private Command[] expectedCommands = new Command[6]
    {
        Command.Up,
        Command.Right,
        Command.Up,
        Command.Right,
        Command.Down,
        Command.Right,
    };

    private Vector3[] expectedPawnPositions = new Vector3[6]
    {
        new Vector3(-0.559099972f,0.340299994f,0.1954f),
        new Vector3(-0.559099972f,0.340299994f,0.0653999969f),
        new Vector3(-0.559099972f,0.406199992f,0.061999999f),
        new Vector3(-0.559099972f,0.406199992f,-0.0637999997f),
        new Vector3(-0.559099972f,0.1426f,-0.0671999976f),
        new Vector3(-0.559099972f,0.1426f,-0.132200003f),
    };

    // Start is called before the first frame update
    void Start()
    {
        pawn.transform.localPosition = pawnInitialPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool ValidateCurrentCommands()
    {
        if (currentCommands.Count != expectedCommands.Length)
        {
            return false;
        }

        for (int j = 0; j < currentCommands.Count; j++)
        {
            if (currentCommands[j] != expectedCommands[j])
            {
                return false;
            }
        }

        return true;
    }

    private string CommandToText(Command command)
    {
        switch (command)
        {
            case Command.Up:
                return "Cima";
            case Command.Down:
                return "Baixo";
            case Command.Left:
                return "Esquerda";
            case Command.Right:
                return "Direita";
            default:
                return "-";
        }
    }

    private void UpdateProgramStack()
    {
        for (int i = 0; i < commandSlots.Length; i++)
        {
            if (i < currentCommands.Count)
            {
                commandSlots[i].transform.Find("CommandBase").GetComponent<Renderer>().material = usedCommandSlotMaterial;
                commandSlots[i].transform.Find("Text").GetComponent<TextMeshPro>().text = CommandToText(currentCommands[i]);
            }
            else
            {
                commandSlots[i].transform.Find("CommandBase").GetComponent<Renderer>().material = emptyCommandSlotMaterial;
                commandSlots[i].transform.Find("Text").GetComponent<TextMeshPro>().text = CommandToText(Command.None);
            }
        }
    }

    internal void AddCommand(Command command)
    {
        if (currentCommands.Count >= commandSlots.Length)
        {
            return;
        }

        currentCommands.Add(command);

        UpdateProgramStack();
    }

    internal void RemoveCommand(int commandIndex)
    {
        if (commandIndex < 0 || commandIndex >= currentCommands.Count)
        {
            return;
        }

        currentCommands.RemoveAt(commandIndex);

        UpdateProgramStack();
    }

    private IEnumerator RunProgramCoroutine()
    {
        programSuccess.SetActive(false);
        programError.SetActive(false);

        // Iterate through expected commands and current commands to move the pawn when they match
        for (int i = -1; i < expectedCommands.Length; i++)
        {
            if (i == -1)
            {
                // Reset pawn position
                pawn.transform.localPosition = pawnInitialPosition;
                yield return new WaitForSeconds(1f);
                continue;
            }
            
            if (i >= currentCommands.Count)
            {
                break;
            }

            if (currentCommands[i] == expectedCommands[i])
            {
                pawn.transform.localPosition = expectedPawnPositions[i];
                yield return new WaitForSeconds(1f);
            }
            else
            {
                break;
            }
        }

        if (ValidateCurrentCommands())
        {
            programSuccess.SetActive(true);
            programError.SetActive(false);

            puzzleManager.TriggerVictory();
        }
        else
        {
            programSuccess.SetActive(false);
            programError.SetActive(true);
        }

        yield return null;
    }

    internal void RunProgram()
    {
        StartCoroutine(RunProgramCoroutine());
    }
}
