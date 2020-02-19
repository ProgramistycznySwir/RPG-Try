using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides buildings atlas and every variables needed for Building-System
/// Values of this class are setted by BuildingSystemSetup object putted in the scene which destroys itself after that
/// </summary>
public static class BuildingSystem
{
    public static GameObject[] buildings;
    public static int buildingsCount;


    //chyba się nie przyda...
    public static GameObject GetBuildingByName(string name)
    {
        foreach (GameObject building in buildings) if (building.name == name) return building;
        return null;
    }
    public static int GetBuildingIDByName(string name)
    {
        for(int i = 0; i < buildingsCount; i++)
        {
            if (buildings[i].name == name) return i;
        }
        return -1;
    }
}
