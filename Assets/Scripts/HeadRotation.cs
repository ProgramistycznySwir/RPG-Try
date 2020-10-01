using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    public float sensitivity;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    float cameraV;
    public float CameraV { get { return cameraV; } }
    float cameraH;
    public float CameraH { get { return cameraH; } }
    void LateUpdate()
    {
        cameraV -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        cameraV = Mathf.Clamp(cameraV, -90, 90);

        cameraH += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        if(cameraH < 0 || cameraV > 360)
            cameraH %= 360f; // Normalizes to 0-360 range

        transform.eulerAngles = new Vector3(cameraV, cameraH, 0f);
    }
}
