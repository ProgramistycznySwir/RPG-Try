using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telemetry : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    RaycastHit raycast;
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out raycast))
        {
            text.text = Vector3.Distance(transform.position, raycast.point).ToString("F0") + "m";
        }
        else text.text = "ERR";
    }
}
