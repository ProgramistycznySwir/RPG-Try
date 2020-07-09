using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechControler : MonoBehaviour
{
    public CharacterController movement;
    public float speed = 10f;
    public bool test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector2 input;
    Vector3 velocity;
    void Update()
    {
        velocity += Physics.gravity;

        if (Input.GetKeyDown(KeyCode.Space))
            test = !test;
        if(!test)
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            movement.SimpleMove(new Vector3(input.x, 0f, input.y) * speed);
        }
    }
}
