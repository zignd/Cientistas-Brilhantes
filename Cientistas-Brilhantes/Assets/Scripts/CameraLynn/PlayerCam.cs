using AHRS;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerCam : MonoBehaviour
{
    public PuzzleManager PuzzleManager;

    public float SensX = 200;
    public float SensY = 200;

    public Transform Orientation;

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
        mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
        mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

        YRotation += mouseX;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, YRotation, 0);
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

        Quaternion orientationQuaternion = Orientation.rotation;

        var slerpedResultQuaternion = Quaternion.Slerp(orientationQuaternion, madgwickQuaternion, t);

        Orientation.rotation = Quaternion.Euler(slerpedResultQuaternion.eulerAngles.x, slerpedResultQuaternion.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(slerpedResultQuaternion.eulerAngles.x, slerpedResultQuaternion.eulerAngles.y, 0);
    }
}
