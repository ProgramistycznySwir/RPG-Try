using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleStats : MonoBehaviour
{
    public float hull;
    public float maxHull = 1000f;
    public float[] armorValues = { .10f, 0f, 0f, 0f, 0f };

    // Start is called before the first frame update
    void Start()
    {
        hull = maxHull;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(RemoteHitBox.ArmorSection armorSectionHitted, float damage, float penetration, RemoteHitBox.DamageType type)
    {
        float damageMultiplier = Mathf.Clamp(1f - armorValues[(int)armorSectionHitted] + penetration, 0f, 1f);
        damage *= damageMultiplier;
        if (armorSectionHitted == RemoteHitBox.ArmorSection.Rear)
            damage *= 1.5f; ///Rear hit damage multiplier
        else if (armorSectionHitted == RemoteHitBox.ArmorSection.Turret)
            damage *= 0.5f; ///Turret hit damage multiplier
    }

    public void SetValues(int hull)
    {

    }
}
