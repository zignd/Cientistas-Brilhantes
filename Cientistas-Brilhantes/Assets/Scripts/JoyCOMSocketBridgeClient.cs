using System;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using Unity.VisualScripting;
using UnityEngine;
using Palmmedia.ReportGenerator.Core.Common;
using Newtonsoft.Json;

public class JoystickData
{
    public short X { get; set; }
    public short Y { get; set; }
    public short SEL { get; set; }
}

public class MPU6050Data
{
    public float AccelX { get; set; }
    public float AccelY { get; set; }
    public float AccelZ { get; set; }
    public float GyroX { get; set; }
    public float GyroY { get; set; }
    public float GyroZ { get; set; }
    public float Temp { get; set; }
}

public class Payload
{
    public JoystickData Joystick { get; set; }
    public MPU6050Data MPU6050 { get; set; }
}

public class JoyCOMSocketBridgeClient : MonoBehaviour
{
    private WebSocket ws;

    public Payload ReceivedPayload;

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
        ReceivedPayload = JsonConvert.DeserializeObject<Payload>(e.Data);

        // var payload = JsonUtility.FromJson<Payload>(e.Data);
        // var payload = JsonUtility.FromJson<Payload>(j);

        Debug.Log("Received: " + e.Data);
    }

    void OnDestroy()
    {
        Debug.Log("WebSocket client stopping...");

        if (ws != null)
            ws.Close();
    }
}