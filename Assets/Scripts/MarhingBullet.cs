using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarhingBullet : MonoBehaviour
{
    public TrailRenderer trail;

    private Vector3 velocity;
    [Tooltip("Used durring collisions")]
    public float mass;
    [Range(0f, 1f)]
    public float reflectivity = 0f;


    private Vector3 newPosition;
    private RaycastHit raycast;
    
    void Start()
    {
        trail.AddPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -200f) Destroy(gameObject);
        newPosition = transform.position + velocity * Time.deltaTime;

        if (Physics.Linecast(transform.position, newPosition, out raycast)) Hit(raycast);
        else Step();

        ///pulls towards ground
        velocity += Physics.gravity * Time.deltaTime;
    }

    void Hit(RaycastHit raycast)
    {
        Rigidbody hittedRigidbody;       


        //Debug.Log(trail.)
        if (reflectivity == 0)
        {
            trail.emitting = false;
            trail.AddPosition(raycast.point);

            if (hittedRigidbody = raycast.collider.GetComponent<Rigidbody>())
            {
                hittedRigidbody.AddForceAtPosition(velocity * mass * 45f, raycast.point);
            }

            Destroy(gameObject, trail.time);
            Destroy(gameObject.GetComponent<MeshRenderer>());
            Destroy(this);
        }
        else
        {
            trail.AddPosition(raycast.point);

            if (hittedRigidbody = raycast.collider.GetComponent<Rigidbody>())
            {
                hittedRigidbody.AddForceAtPosition(velocity * mass * 45f, raycast.point);                
            }
            float surfaceBounciness = 1f;
            if (raycast.collider.tag == "Map") surfaceBounciness = 0.05f;
            velocity = Vector3.Reflect(velocity, raycast.normal) * reflectivity * surfaceBounciness;

            transform.position = raycast.point;
        }

        trail.emitting = true;
    }

    void Step()
    {
        transform.position = newPosition;
        transform.forward = velocity.normalized;
    }

    public void SetVelocity(float velocity, Vector3 dirrection) //gives barrelEnd.forward
    {
        this.velocity = dirrection * velocity;
    }
}
