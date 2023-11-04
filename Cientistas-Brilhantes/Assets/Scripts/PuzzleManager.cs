using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum Mode { MouseAndKeyboard, Controller };

public class PuzzleManager : MonoBehaviour
{
    public GameObject PuzzlePieces;
    public GameObject VictoryCanvas;
    public AudioSource VictoryAudio;
    public Mode SelectedMode;

    [SerializeField]
    private string nextScene;

    [SerializeField]
    private int debugMode;

    void Start()
    {
        SelectedMode = (Mode)PlayerPrefs.GetInt("SelectedMode");
        debugMode = PlayerPrefs.GetInt("DebugMode");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            TriggerVictory();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (VictoryCanvas == null)
            {
                Debug.LogError("Can't click VictoryCanvas is null");
                return;
            }

            ExecuteEvents.Execute(VictoryCanvas, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedMode = Mode.MouseAndKeyboard;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedMode = Mode.Controller;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            PlayerPrefs.SetInt("DebugMode", 0);
            debugMode = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefs.SetInt("DebugMode", 1);
            debugMode = 1;
        }

    }

    public void ValidatePuzzle()
    {
        if (PuzzlePieces == null)
        {
            Debug.LogError("Can't validate puzzle PuzzlePieces is null");
            return;
        }

        var allPiecesActive = true;
        foreach (Transform child in PuzzlePieces.gameObject.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                allPiecesActive = false;
            }
        }

        if (allPiecesActive)
        {
            TriggerVictory();
        }
    }

    public void TriggerVictory()
    {
        VictoryCanvas.SetActive(true);

        if (VictoryAudio != null)
        {
            VictoryAudio.Play();
        }

        StartCoroutine(LoadSceneAfterDelay(5));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextScene);
    }
}
