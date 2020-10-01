using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This projectile has constant velocity
/// </summary>
public class Projectile : MonoBehaviour
{
    Vector3 velocity;
    float speed;

    public float decayDistance = 400f;

    public MeshRenderer renderer;
    public TrailRenderer trail;
    
    void Awake()
    {
        speed = velocity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Step();
    }

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
        transform.forward = velocity;
    }

    RaycastHit hit;
    Vector3 nextPosition;
    void Step()
    {
        nextPosition = transform.position + velocity * Time.deltaTime;
        if (Physics.Linecast(transform.position, nextPosition, out hit))
            Hit(hit);
        else
            transform.position = nextPosition;

        decayDistance -= speed * Time.deltaTime;
        // Checking decay condition
        if (decayDistance < 0);
    }
    
    void Hit(RaycastHit hit)
    {
        // Damage dealing here

        transform.position = hit.point;

        Die();
    }

    void Die()
    {
        Destroy(renderer);
        Destroy(this);
        Destroy(gameObject, trail.time);
    }
}
