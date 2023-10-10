using System;
using System.IO.Ports;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;

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

class Program
{
    private const string PORT_NAME = "COM7";
    private const int BAUD_RATE = 115200;
    private const string SERVER_URL = "http://localhost:8090/";

    private static SerialPort serialPort;

    private static async Task HandleWebSocket(WebSocket socket)
    {
        while(true)
        {
            if (socket.State == WebSocketState.Open)
            {
                Console.WriteLine("Parsing data received from serial port");

                byte[] buffer = new byte[35];  // Buffer size to match the data being sent
                JoystickData joystickData = new JoystickData();
                MPU6050Data mpu6050Data = new MPU6050Data();
                Payload payload = new Payload();

                // Read data from the serial port
                serialPort.Read(buffer, 0, buffer.Length);

                // TODO: convert to use BinaryReader
                // var Reader = new BinaryReader(new MemoryStream(buffer))

                // Unpack joystick data
                joystickData.X = BitConverter.ToInt16(buffer, 0);
                joystickData.Y = BitConverter.ToInt16(buffer, 2);
                joystickData.SEL = BitConverter.ToInt16(buffer, 4);

                // Unpack MPU6050 data
                mpu6050Data.AccelX = BitConverter.ToSingle(buffer, 6);
                mpu6050Data.AccelY = BitConverter.ToSingle(buffer, 10);
                mpu6050Data.AccelZ = BitConverter.ToSingle(buffer, 14);
                mpu6050Data.GyroX = BitConverter.ToSingle(buffer, 18);
                mpu6050Data.GyroY = BitConverter.ToSingle(buffer, 22);
                mpu6050Data.GyroZ = BitConverter.ToSingle(buffer, 26);
                mpu6050Data.Temp = BitConverter.ToSingle(buffer, 30);

                // Verify checksum
                byte checksum = 0;
                for (int i = 0; i < 34; i++)
                {
                    checksum ^= buffer[i];
                }

                if (checksum == buffer[34])
                {
                    Console.WriteLine("Checksum verified. Data received successfully.");

                    payload.Joystick = joystickData;
                    payload.MPU6050 = mpu6050Data;
                    string jsonPayload = JsonSerializer.Serialize(payload);
                    Console.WriteLine(jsonPayload);

                    byte[] response = Encoding.UTF8.GetBytes(jsonPayload);

                    await socket.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
                    Console.WriteLine("Received data was forwarded to WebSocket connection");
                }
                else
                {
                    Console.WriteLine("Checksum verification failed. Data might be corrupted.");
                }
            }
            else
            {
                Console.WriteLine("WebSocket connection is closed");
            }
        }
    }

    static async Task Main(string[] args)
    {
        try
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(SERVER_URL);
            listener.Start();
            Console.WriteLine($"JoyCOMSocketBridge server started at {SERVER_URL}...");

            while (true)
            {
                try
                {
                    serialPort = new SerialPort(PORT_NAME, BAUD_RATE);

                    Console.WriteLine("Openning serial port...");
                    serialPort.Open();
                    Console.WriteLine("Serial port opened");

                    while (true)
                    {
                        Console.WriteLine("Waiting for connection...");
                        var context = await listener.GetContextAsync();
                        if (context.Request.IsWebSocketRequest)
                        {
                            var webSocketContext = await context.AcceptWebSocketAsync(null);
                            Console.WriteLine("WebSocket connection accepted");
                            await HandleWebSocket(webSocketContext.WebSocket);
                        }
                        else
                        {
                            Console.WriteLine("HTTP request received, but it is not a WebSocket request");
                            context.Response.StatusCode = 400;
                            context.Response.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception occurred in the main loop: {ex.ToString()}");
                }
                finally
                {
                    Console.WriteLine("Checking if serial port is open...");
                    if (serialPort.IsOpen)
                    {
                        Console.WriteLine("Closing serial port...");
                        serialPort.Close();
                        Console.WriteLine("Serial port closed");
                    }
                    else
                    {
                        Console.WriteLine("Serial port is not open");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JoyCOMSocketBridge server failed to start: {ex.ToString()}");
        }
    }
}
