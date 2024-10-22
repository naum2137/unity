using UnityEngine;

public class zad6_lerp : MonoBehaviour
{
    public Transform target; // Obiekt �ledzony
    public Transform tracker; // Obiekt �ledz�cy

    public float smoothTime = 0.3f; // Czas wyg�adzania dla SmoothDamp
    public float lerpSpeed = 5.0f; // Pr�dko�� dla Lerp

    private Vector3 velocity = Vector3.zero; // Pr�dko�� dla SmoothDamp

    void Update()
    {
        Vector3 targetPosition = target.position;

        // Lerp: �ledzenie obiektu
         tracker.position = Vector3.Lerp(tracker.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
