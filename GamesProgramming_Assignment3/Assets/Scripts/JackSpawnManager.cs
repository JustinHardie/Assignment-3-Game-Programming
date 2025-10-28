using UnityEngine;
using System.Collections.Generic;

// Enables/disables objects near the camera
public class CameraActivator : MonoBehaviour
{
    public Camera mainCamera;
    public float activationRange = 15f;
    public string[] tagsToManage = { "Spike", "OilBarrel", "Item" };

    private List<GameObject> objectsToManage = new List<GameObject>();

    void Start()
    {
        if (!mainCamera) mainCamera = Camera.main;

        // Find all objects with the specified tags
        foreach (string tag in tagsToManage)
            objectsToManage.AddRange(GameObject.FindGameObjectsWithTag(tag));
    }

    void Update()
    {
        if (!mainCamera) return;

        Vector3 camPos = mainCamera.transform.position;

         // Enable or disable objects based on distance to camera
        foreach (var obj in objectsToManage)
        {
            if (!obj) continue;
            obj.SetActive(Vector3.Distance(camPos, obj.transform.position) < activationRange);
        }
    }
}
