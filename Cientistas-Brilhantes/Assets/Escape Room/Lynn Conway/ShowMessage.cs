using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    [SerializeField]
    private PuzzleManager puzzleManager;

    [SerializeField]
    private GameObject messageBox;

    [SerializeField]
    private List<string> messages = new List<string>();

    private bool isMessageBoxActive = false;

    private int currentMessage = 0;

    private void Start()
    {
        messageBox.SetActive(false);
    }

    private bool LoadMessage()
    {
        if(currentMessage >= messages.Count)
        {
            return false;
        }

        messageBox.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = messages[currentMessage];
        currentMessage++;
        return true;
    }

    private void Update()
    {
        if(isMessageBoxActive)
        {
            if (puzzleManager.Mode == Mode.MouseAndKeyboard)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    UpdateMessage();
                }
            }
            else
            {
                if (JoyCOMBridge.ReceivedPayload.Button2)
                {
                    UpdateMessage();
                }
            }
        }
    }

    private void UpdateMessage()
    {
        if (!LoadMessage())
        {
            messageBox.SetActive(false);
            isMessageBoxActive = false;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            LoadMessage();
            messageBox.SetActive(true);
            isMessageBoxActive = true;    
        }
    }
}