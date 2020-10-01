using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera camera;
    public HeadRotation head;
    //public CameraRotation cameraRotation;

    private float cameraFoV;
    private float cameraSensitivity;

    public float zoomSensitivity = 5f;

    float zoom = 1;
    public float Zoom { get { return isZoomedIn ? zoom : 1; } }

    public float maxZoom = 8;

    public Vector3 cameraTargetPos;
    bool isZoomedIn = false;
    public bool IsZoomedIn { get { return isZoomedIn; } }

    // Start is called before the first frame update
    void Start()
    {
        cameraFoV = camera.fieldOfView;
        cameraSensitivity = head.sensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isZoomedIn && Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            zoom = 1;
            isZoomedIn = true;
        }

        zoom = Mathf.Clamp(zoom + Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity, 1, maxZoom);

        //if (zoom == 1)
        //    isZoomedIn = false;
        //else
        //    isZoomedIn = true;

        if (Input.GetMouseButtonDown(2))
            isZoomedIn = !isZoomedIn;
        ApplyZoom();
        //OnMouseMiddleButton();
    }

    void ApplyZoom()
    {
        if(isZoomedIn)
        {
            camera.fieldOfView = cameraFoV / zoom;
            head.sensitivity = cameraSensitivity / zoom;
        }
        else
        {
            camera.fieldOfView = cameraFoV;
            head.sensitivity = cameraSensitivity;
        }

        //if (zoom > 1)
        //{
        //    //camera.transform.localPosition = cameraPos;
        //    camera.fieldOfView = cameraFoV / zoom;
        //    head.sensitivity = cameraSensitivity / zoom;
        //    //cameraRotation.sensitivityFromZoom = 1f / zoom;
        //    //zoomedIn = true;
        //}
        //else
        //{
        //    //cameraTargetPos = (cameraPos * 2) / zoom;
        //    //camera.transform.localPosition = (cameraPos * 2) / zoom;
        //    camera.fieldOfView = cameraFoV;
        //    head.sensitivity = cameraSensitivity / zoom;
        //    //cameraRotation.sensitivityFromZoom = 1f;
        //    //zoomedIn = false;
        //}
    }

    void OnMouseMiddleButton()
    {
        //if(isZoomedIn)

    }
}
