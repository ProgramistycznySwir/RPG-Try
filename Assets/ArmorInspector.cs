using UnityEngine;

public class ArmorInspector : MonoBehaviour
{
    public Transform hitIndicator;
    public TMPro.TextMeshPro text;
    public float range = 50f;

    public RaycastHit raycast;

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out raycast, range))
        {
            text.text = raycast.triangleIndex.ToString();
            hitIndicator.position = raycast.point;
        }
        else
        {
            text.text = "none";
            hitIndicator.localPosition = Vector3.zero;
        }
    }
}
