using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Cannon : Weapon
{
    public Transform[] barrelEnds;
    int barrelEndIndex;

    public float cooldownTime = 0.5f;
    float cooldown;

    public float muzzleVelocity = 400f;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0))
            Fire();
    }

    public override void Fire()
    {
        while(cooldown <= 0)
        {
            Projectile newProjectile = Instantiate(projectile, barrelEnds[barrelEndIndex].position, barrelEnds[barrelEndIndex].rotation).GetComponent<Projectile>();
            newProjectile.SetVelocity(barrelEnds[barrelEndIndex].forward * muzzleVelocity);

            if (++barrelEndIndex >= barrelEnds.Length)
                barrelEndIndex = 0;

            cooldown += cooldownTime;
        }
    }
}
