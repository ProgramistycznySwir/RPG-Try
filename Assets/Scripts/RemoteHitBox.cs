using UnityEngine;

public class RemoteHitBox : MonoBehaviour
{
    public Transform transformToFolow;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transformToFolow.position;
        transform.rotation = transformToFolow.rotation;
    }
}
