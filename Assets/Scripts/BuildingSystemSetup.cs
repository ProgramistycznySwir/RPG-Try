using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystemSetup : MonoBehaviour
{
    public GameObject[] buildings;

    // Start is called before the first frame update
    void Awake()
    {
        BuildingSystem.buildings = buildings;
        BuildingSystem.buildingsCount = buildings.Length;
        Debug.Log("BuildingSystemSetup: uploaded buildings properly");

        Destroy(this);
    }
}
