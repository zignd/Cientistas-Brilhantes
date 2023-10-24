using System;
using UnityEngine;
using System.IO.Ports;

public class JoystickData
{
    public short X { get; set; }
    public short Y { get; set; }
    public bool SEL { get; set; }
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
    public bool Button1 { get; set; }
    public bool Button2 { get; set; }
}

public class JoyCOMBridge : MonoBehaviour
{
    private const string PORT_NAME = "COM7";
    private const int BAUD_RATE = 115200;

    private static SerialPort serialPort;

    public Payload ReceivedPayload;

    public JoyCOMBridge()
    {
        ReceivedPayload = new Payload
        {
            MPU6050 = new MPU6050Data(),
            Joystick = new JoystickData()
        };
    }

    void Start()
    {
        // OpenSerialPort();
    }

    void Update()
    {
        //if (serialPort == null)
        //{
        //    OpenSerialPort();
        //}

        //byte[] buffer = new byte[39];  // Buffer size to match the data being sent
        //JoystickData joystickData = new JoystickData();
        //MPU6050Data mpu6050Data = new MPU6050Data();
        //Payload payload = new Payload();

        //// Read data from the serial port
        //serialPort.Read(buffer, 0, buffer.Length);

        //// Unpack joystick data
        //joystickData.X = BitConverter.ToInt16(buffer, 0);
        //joystickData.Y = BitConverter.ToInt16(buffer, 2);
        //joystickData.SEL = BitConverter.ToInt16(buffer, 4) == 0;

        //// Unpack MPU6050 data
        //mpu6050Data.AccelX = BitConverter.ToSingle(buffer, 6);
        //mpu6050Data.AccelY = BitConverter.ToSingle(buffer, 10);
        //mpu6050Data.AccelZ = BitConverter.ToSingle(buffer, 14);
        //mpu6050Data.GyroX = BitConverter.ToSingle(buffer, 18);
        //mpu6050Data.GyroY = BitConverter.ToSingle(buffer, 22);
        //mpu6050Data.GyroZ = BitConverter.ToSingle(buffer, 26);
        //mpu6050Data.Temp = BitConverter.ToSingle(buffer, 30);

        //// Unpack the buttons data
        //payload.Button1 = BitConverter.ToInt16(buffer, 34) == 0;
        //payload.Button2 = BitConverter.ToInt16(buffer, 36) == 0;

        //// Verify checksum
        //byte checksum = 0;
        //for (int i = 0; i < 38; i++)
        //{
        //    checksum ^= buffer[i];
        //}

        //if (checksum == buffer[38])
        //{
        //    payload.Joystick = joystickData;
        //    payload.MPU6050 = mpu6050Data;

        //    payload.Joystick.X = Normalize(payload.Joystick.X);
        //    payload.Joystick.Y = Normalize(payload.Joystick.Y);

        //    ReceivedPayload = payload;

        //    // Debug.Log("Received: GyroX: " + ReceivedPayload.MPU6050.GyroX + " GyroY: " + ReceivedPayload.MPU6050.GyroY + " GyroZ: " + ReceivedPayload.MPU6050.GyroZ + " Joystick.X: " + ReceivedPayload.Joystick.X + " Joystick.Y: " + ReceivedPayload.Joystick.Y);
        //}
        //else
        //{
        //    Debug.LogError("Checksum verification failed. Data might be corrupted.");
        //}
    }

    void OnDestroy()
    {
        //Debug.Log("Checking if serial port is open...");
        //if (serialPort.IsOpen)
        //{
        //    Debug.Log("Closing serial port...");
        //    serialPort.Close();
        //    Debug.Log("Serial port closed");
        //}
        //else
        //{
        //    Debug.Log("Serial port is not open");
        //}
    }

    private void OpenSerialPort()
    {
        serialPort = new SerialPort(PORT_NAME, BAUD_RATE);

        Debug.Log("Openning serial port...");
        serialPort.Open();
        Debug.Log("Serial port opened");
    }

    private short Normalize(short value)
    {
        if (value >= 1800 && value <= 1900)
            return 0;

        if (value > 1900)
            return -1;

        return 1;
    }
}