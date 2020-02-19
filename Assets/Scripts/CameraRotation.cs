using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 2.5f;
    public float sensitivityFromZoom = 1f;

    public Vector2 cameraLimits = new Vector2(-45f, 80f);

    private float cameraYaw = 0f;
    private float cameraPitch = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraYaw += Input.GetAxis("Mouse X") * mouseSensitivity * sensitivityFromZoom;
        cameraPitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * sensitivityFromZoom;

        cameraPitch = Mathf.Clamp(cameraPitch, cameraLimits.x, cameraLimits.y);

        //transform.rotation.SetEulerAngles(cameraPitch, cameraYaw, 0f);// = new Vector3(cameraPitch, cameraYaw);
        transform.eulerAngles = new Vector3(cameraPitch, cameraYaw);
    }
}
