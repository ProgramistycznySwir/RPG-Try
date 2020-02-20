using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderUtility : MonoBehaviour
{
    public Transform camera;

    public float placingRange = 50f;

    public KeyCode change = KeyCode.U;

    public int currentID = 0;

    private Transform currentPreview;

    // Start is called before the first frame update
    void Start()
    {
        PreparePreview();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(change)) CycleBuilding();

        //if (Input.GetMouseButtonDown(1)) ShowPreview(true);
        if (Input.GetMouseButton(1))
        {
            RaycastHit raycast;
            if (Physics.Raycast(camera.position - camera.forward * camera.position.x, camera.forward, out raycast, placingRange))
            {
                ShowPreview(true);
                UpdatePreview(raycast.point, Quaternion.identity);

                if (Input.GetMouseButtonDown(0)) PlaceBuilding(raycast.point, Quaternion.identity);
            }
            else ShowPreview(false);
        }
        if (Input.GetMouseButtonUp(1)) ShowPreview(false);
        //Później za pomocą przytrzymania RMB tworzy się podgląd budowli, a trzymając RMB i klikając LMB stawia się budowlę
    }

    public void PlaceBuilding(Vector3 position, Quaternion rotation)
    {
        //Instantiate(BuildingSystem.buildings[currentID], raycast.point, Quaternion.LookRotation(camera.forward, raycast.normal));
        Instantiate(BuildingSystem.buildings[currentID], position, rotation);


    }

    public void CycleBuilding()
    {
        currentID++;
        if (currentID >= BuildingSystem.buildingsCount) currentID = 0;

        Destroy(currentPreview.gameObject);

        PreparePreview();
    }

    //Disables scripts on preview
    public void PreparePreview()
    {
        currentPreview = Instantiate(BuildingSystem.buildings[currentID], Vector3.zero, Quaternion.identity).transform;

        ///disables all components of Preview
        foreach (MonoBehaviour component in currentPreview.GetComponents<MonoBehaviour>())
        {
            Debug.Log("I'm productive");
            if (component != this) Debug.Log("I'm also productive" + component.ToString()); //component.enabled = false;
        }
    }
    public void ShowPreview(bool show)
    {
        currentPreview.GetComponent<MeshRenderer>().enabled = show;
    }
    public void UpdatePreview(Vector3 position, Quaternion rotation)
    {
        currentPreview.position = position;
        currentPreview.rotation = rotation;
    }
}
