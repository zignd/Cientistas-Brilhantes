using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public JoyCOMSocketBridgeClient joycomsocket;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        //if (joycomsocket.ReceivedPayload != null)
        //    UpdateMPU6050Data(joycomsocket.ReceivedPayload.MPU6050);
    }

    // Sensibilidade da rotação. Estes valores determinam quão rápido a câmera rotaciona em relação aos dados do MPU6050.
    public float gyroSensitivity = 1.0f;
    public float accelSensitivity = 1.0f;

    public void UpdateMPU6050Data(MPU6050Data data)
    {
        // Usamos os dados do giroscópio para rotação
        float rotationX = data.GyroX * gyroSensitivity;
        float rotationY = data.GyroY * gyroSensitivity;
        float rotationZ = data.GyroZ * gyroSensitivity;

        // Rotação aplicada à câmera
        transform.Rotate(Vector3.up * rotationX * Time.deltaTime);
        transform.Rotate(Vector3.right * -rotationY * Time.deltaTime); // Invertido para a rotação no eixo Y corresponder à inclinação vertical.
                                                                       // Para rotação no eixo Z (roll), poderia ser algo assim:
                                                                       // transform.Rotate(Vector3.forward * rotationZ * Time.deltaTime);

        // Os dados do acelerômetro podem ser usados, por exemplo, para mover a câmera, 
        // embora seja menos comum em muitas aplicações. Um uso possível seria determinar 
        // se o dispositivo está virado para cima ou para baixo (por exemplo, usando AccelZ).

        // Mas, se quiser usar a aceleração para mover a câmera:
        //float moveX = data.AccelX * accelSensitivity;
        //float moveY = data.AccelY * accelSensitivity;
        //float moveZ = data.AccelZ * accelSensitivity;
        //transform.Translate(new Vector3(moveX, moveY, moveZ) * Time.deltaTime);
    }
}
