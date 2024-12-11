using UnityEngine;

public class CameraResize : MonoBehaviour
{
    public float newSize = 10f; // Nowy rozmiar kamery

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

        // Zmieñ rozmiar kamery
        cam.orthographicSize = newSize;
    }
}
