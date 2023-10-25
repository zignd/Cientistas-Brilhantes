using AHRS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerCamera : MonoBehaviour
{
    public PuzzleManager PuzzleManager;

    public float MouseSensibilityX = 200;
    public float MouseSensibilityY = 200;

    public Transform PlayerOrientation;

    public float mouseX;
    public float mouseY;

    public float XRotation;
    public float YRotation;

    public JoyCOMBridge BridgeClient;

    public float SamplePeriod = 0.05f;
    public float Beta = 1f;
    public float t = 0.1f;
    
    public Quaternion QuaternionReadOnly;
    public Vector3 QuarternionToEulerReadOnly;
    public float[] MadgwickInternalQuaternionReadOnly;

    private MadgwickAHRS madgwickAHRS;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        BridgeClient.OnButton1Pressed += BridgeClient_OnButton1Pressed;
        BridgeClient.OnButton2Pressed += BridgeClient_OnButton2Pressed;
        BridgeClient.OnJoystickSELPressed += BridgeClient_OnJoystickSELPressed;
    }

    private void BridgeClient_OnButton1Pressed()
    {
        // Make raycast to see if there is an interactable object in front of the player
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            // If there is, check if it implements the IInteractable interface
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // If it does, call the TriggerInteraction1 method
                interactable.TriggerInteraction1();
            }
        }
    }

    private void BridgeClient_OnButton2Pressed()
    {
        // Make raycast to see if there is an interactable object in front of the player
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            // If there is, check if it implements the IInteractable interface
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // If it does, call the TriggerInteraction2 method
                interactable.TriggerInteraction2();
            }
        }
    }

    private void BridgeClient_OnJoystickSELPressed()
    {
        // Make raycast to see if there is an interactable object in front of the player
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            // If there is, check if it implements the IInteractable interface
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // If it does, call the TriggerInteraction3 method
                interactable.TriggerInteraction3();
            }
        }
    }

    private void Update()
    {
        if (PuzzleManager.Mode == Mode.MouseAndKeyboard)
        {
            UpdateRotationWithMouseData();
        }
        else
        {
            if (madgwickAHRS == null)
            {
                madgwickAHRS = new MadgwickAHRS(SamplePeriod, Beta);
            }

            if (BridgeClient.ReceivedPayload != null)
            {
                UpdateRotationWithMPU6050Data(BridgeClient.ReceivedPayload.MPU6050);
            }
        }
    }

    public void UpdateRotationWithMouseData()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * MouseSensibilityX;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * MouseSensibilityY;

        YRotation += mouseX;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        PlayerOrientation.rotation = Quaternion.Euler(0, YRotation, 0);
    }

    public void UpdateRotationWithMPU6050Data(MPU6050Data data)
    {
        if (BridgeClient.ReceivedPayload.Button1)
        {
            return;
        }

        madgwickAHRS.SamplePeriod = SamplePeriod;
        madgwickAHRS.Beta = Beta;

        madgwickAHRS.Update(data.GyroX, data.GyroY, data.GyroZ, data.AccelX, data.AccelY, data.AccelZ);

        var madgwickQuaternion = new Quaternion(
            madgwickAHRS.Quaternion[1],
            madgwickAHRS.Quaternion[0],
            madgwickAHRS.Quaternion[2],
            madgwickAHRS.Quaternion[3]
        );

        MadgwickInternalQuaternionReadOnly = madgwickAHRS.Quaternion;
        QuaternionReadOnly = madgwickQuaternion;
        QuarternionToEulerReadOnly = madgwickQuaternion.eulerAngles;

        Quaternion orientationQuaternion = PlayerOrientation.rotation;

        var slerpedResultQuaternion = Quaternion.Slerp(orientationQuaternion, madgwickQuaternion, t);

        PlayerOrientation.rotation = Quaternion.Euler(slerpedResultQuaternion.eulerAngles.x, slerpedResultQuaternion.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(slerpedResultQuaternion.eulerAngles.x, slerpedResultQuaternion.eulerAngles.y, 0);
    }
}
