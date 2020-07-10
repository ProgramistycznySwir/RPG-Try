using UnityEngine;

public class ArmorInspector : MonoBehaviour
{
    public Transform hitIndicator;
    public TMPro.TextMeshPro text;
    public float range = 50f;

    public RaycastHit hit;
    //void Update()
    //{
    //    if(Physics.Raycast(transform.position, transform.forward, out hit, range))
    //    {
    //        text.text = hit.triangleIndex.ToString();
    //        hitIndicator.position = hit.point;
    //        RemoteHitBox hitBox = hit.collider.GetComponent<RemoteHitBox>();
    //        if(hitBox)
    //        {
    //            if (Input.GetKeyDown(KeyCode.Alpha1))
    //                hitBox.MapArmorSection(RemoteHitBox.ArmorSection.Front, hit.triangleIndex);
    //            else if (Input.GetKeyDown(KeyCode.Alpha2))
    //                hitBox.MapArmorSection(RemoteHitBox.ArmorSection.Sides, hit.triangleIndex);
    //            else if (Input.GetKeyDown(KeyCode.Alpha3))
    //                hitBox.MapArmorSection(RemoteHitBox.ArmorSection.Rear, hit.triangleIndex);
    //            else if (Input.GetKeyDown(KeyCode.Alpha4))
    //                hitBox.MapArmorSection(RemoteHitBox.ArmorSection.Top, hit.triangleIndex);
    //            else if (Input.GetKeyDown(KeyCode.Alpha5))
    //                hitBox.MapArmorSection(RemoteHitBox.ArmorSection.Bot, hit.triangleIndex);
    //        }
    //    }
    //    else
    //    {
    //        text.text = "none";
    //        hitIndicator.localPosition = Vector3.zero;
    //    }
    //}
}
