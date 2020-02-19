using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera camera;
    public CameraRotation cameraRotation;

    private float cameraFoV;
    private Vector3 cameraPos;

    public float zoom = 1;
    [Tooltip("It has to be power of 2")]
    public float minZoom = 0.5f;
    [Tooltip("It has to be power of 2")]
    public int maxZoom = 32;

    public Vector3 cameraTargetPos;
    public bool zoomedIn = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraFoV = camera.fieldOfView;
        cameraPos = camera.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && zoom < maxZoom)
        {
            zoom *= 2;
            Zoom();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && zoom > minZoom)
        {
            zoom /= 2;
            Zoom();
        }

        //if (!zoomedIn)
        //{
        //    RaycastHit raycast;
        //    if (Physics.Linecast(transform.position, transform.position + cameraTargetPos, out raycast) && raycast.collider.tag != transform.tag)
        //    {
        //        camera.transform.localPosition = raycast.point;
        //    }
        //    else camera.transform.localPosition = cameraTargetPos;
        //}
    }

    void Zoom()
    {
        if(zoom > 1)
        {
            camera.transform.localPosition = cameraPos;
            camera.fieldOfView = cameraFoV / zoom;
            cameraRotation.sensitivityFromZoom = 1f / zoom;
            zoomedIn = true;
        }
        else
        {
            //cameraTargetPos = (cameraPos * 2) / zoom;
            camera.transform.localPosition = (cameraPos * 2) / zoom;
            camera.fieldOfView = cameraFoV;
            cameraRotation.sensitivityFromZoom = 1f;
            zoomedIn = false;
        }
    }
}
