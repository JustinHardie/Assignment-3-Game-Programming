using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxFactor = 0.3f; 
    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - previousCamPos; //calculates how much camera has moved since last frame
        transform.position += new Vector3(delta.x * parallaxFactor, 0, 0); //moves the background
        previousCamPos = cam.position;
    }
}
