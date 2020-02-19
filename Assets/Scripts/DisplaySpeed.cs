using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySpeed : MonoBehaviour
{
    public Rigidbody rigidbody;
    public TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = (rigidbody.velocity.magnitude * 3.6f).ToString("F1") + "km/h";
    }
}
