using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 2.5f;
    public float sensitivityFromZoom = 1f;

    public Vector2 cameraLimits = new Vector2(-45f, 80f);
    public Transform camera;

    private float cameraYaw = 0f;
    private float cameraPitch = 0f;

    private Vector3 cameraTargetPosition;

    public bool hijackCursor = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = !hijackCursor;
        cameraTargetPosition = camera.localPosition;
    }

    RaycastHit raycast;

    // Update is called once per frame
    void Update()
    {
        cameraYaw += Input.GetAxis("Mouse X") * mouseSensitivity * sensitivityFromZoom;
        cameraPitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * sensitivityFromZoom;

        cameraPitch = Mathf.Clamp(cameraPitch, cameraLimits.x, cameraLimits.y);

        

        if (Physics.Linecast(transform.TransformPoint(cameraTargetPosition.x, cameraTargetPosition.y, 0f), transform.TransformPoint(cameraTargetPosition), out raycast))
            camera.localPosition = transform.InverseTransformPoint(raycast.point);
        else
            camera.localPosition = cameraTargetPosition;

        //transform.rotation.SetEulerAngles(cameraPitch, cameraYaw, 0f);// = new Vector3(cameraPitch, cameraYaw);
        transform.eulerAngles = new Vector3(cameraPitch, cameraYaw);
    }
}
