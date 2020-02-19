using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelector : MonoBehaviour
{
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit raycastHit;
            Debug.DrawRay(new Vector3(1, 1, 1), new Vector3(22, 22, 22));
            //Debug.DrawRay(camera.ViewportPointToRay(Input.mousePosition).origin, camera.ViewportPointToRay(Input.mousePosition).direction, Color.red, 100f);
            if (Physics.Raycast(camera.ViewportPointToRay(Input.mousePosition), out raycastHit, 1000f, LayerMask.NameToLayer("all")))
            {
                Debug.Log(raycastHit.collider.name);
            }
            
        }
    }
}
