using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    public float sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float cameraV;
    float cameraH;
    void Update()
    {
        cameraV = Mathf.Clamp(cameraV + Input.GetAxis("Mouse Y"), -90, 90);
        //cameraH =

        transform.eulerAngles = new Vector3(cameraV, cameraH, 0f);
    }
}
