using UnityEngine;

public class RemoteHitBox : MonoBehaviour
{
    public enum ArmorSection { Front, Sides, Rear, Top, Bot, Turret }
    public enum DamageType { Basic, EMP, Fire };

    public int[] front;
    public int[] sides;
    public int[] rear;
    public int[] top;
    public int[] bot;

    public Transform transformToFolow;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transformToFolow.position;
        transform.rotation = transformToFolow.rotation;
    }

    public void Hit(int hittedVertex, int damage, int penetration)
    {

    }
}
