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
    public AudioSource VictoryAudio; // Refer�ncia ao AudioSource
    public Mode Mode;

    [SerializeField]
    private string nextScene;

    private bool victoryCanvasVisible = false; // Adicionamos uma vari�vel para rastrear a visibilidade do VictoryCanvas

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se a tecla "E" foi pressionada
        if (victoryCanvasVisible && Input.GetKeyDown(KeyCode.E))
        {
            // Inverte a visibilidade do VictoryCanvas ao pressionar "E"
            victoryCanvasVisible = !victoryCanvasVisible;
        }

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
            Mode = Mode.MouseAndKeyboard;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Mode = Mode.Controller;
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

    private void TriggerVictory()
    {
        victoryCanvasVisible = true; // Define a visibilidade do VictoryCanvas como verdadeira
        VictoryCanvas.SetActive(true);

        if (VictoryAudio != null)
        {
            VictoryAudio.Play(); // Toca o �udio da vit�ria
        }

        StartCoroutine(LoadSceneAfterDelay(5));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextScene);
    }
}
