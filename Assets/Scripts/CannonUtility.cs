using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonUtility : MonoBehaviour
{
    public bool marchingBullets = false;

    public GameObject pellet;
    public GameObject pelletM;
    public float pelletVelocity = 100f;

    public Rigidbody rigidbody;

    public UnityEngine.UI.Image reloadCircle;


    public Transform barrelEnd;
    public float reloadTime;

    private float cooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) marchingBullets = !marchingBullets;

        if (Input.GetAxis("Fire1") > 0)
        {
            Fire();
        }
        if (cooldown > 0) cooldown -= Time.deltaTime;

        reloadCircle.fillAmount = Mathf.Clamp(cooldown / reloadTime, 0, 1);
    }

    void Fire()
    {
        if (cooldown > 0) return;

        cooldown = reloadTime;

        //GameObject newPellet = Instantiate(pellet, barrelEnd.position, barrelEnd.rotation);

        if (marchingBullets)
        {
            GameObject newPellet = Instantiate(pelletM, barrelEnd.position, barrelEnd.rotation);

            newPellet.GetComponent<MarhingBullet>().SetVelocity(pelletVelocity, barrelEnd.forward);

            rigidbody.AddForceAtPosition(-newPellet.transform.forward * pelletVelocity * newPellet.GetComponent<MarhingBullet>().mass * 100f, barrelEnd.position);
        }
        else
        {
            GameObject newPellet = Instantiate(pellet, barrelEnd.position, barrelEnd.rotation);

            newPellet.GetComponent<Rigidbody>().velocity = newPellet.transform.forward * pelletVelocity;

            rigidbody.AddForceAtPosition(-newPellet.transform.forward * pelletVelocity * newPellet.GetComponent<Rigidbody>().mass * 100f, barrelEnd.position);
        }        
    }
}
