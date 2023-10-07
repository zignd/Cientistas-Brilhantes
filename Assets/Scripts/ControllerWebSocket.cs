using System;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerWebSocket : MonoBehaviour
{
    private WebSocket ws;

    void Start()
    {
        ws = new WebSocket("ws://localhost:8090");
        ws.OnMessage += OnMessage;
        ws.ConnectAsync();

        Debug.Log("WebSocket client started...");
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ws.Send("Space key released");
        }
    }

    void OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("Received: " + e.Data);
    }

    void OnDestroy()
    {
        Debug.Log("WebSocket client stopping...");

        if (ws != null)
            ws.Close();
    }
}