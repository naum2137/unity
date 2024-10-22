using UnityEngine;

public class zad6_SmoothDamp : MonoBehaviour
{
    public Transform target; // Obiekt �ledzony
    public Transform tracker; // Obiekt �ledz�cy

    public float smoothTime = 0.3f; // Czas wyg�adzania dla SmoothDamp
    public float lerpSpeed = 5.0f; // Pr�dko�� dla Lerp

    private Vector3 velocity = Vector3.zero; // Pr�dko�� dla SmoothDamp

    void Update()
    {
        // SmoothDamp: �ledzenie obiektu z p�ynnym przej�ciem
        Vector3 targetPosition = target.position;
        tracker.position = Vector3.SmoothDamp(tracker.position, targetPosition, ref velocity, smoothTime);

    }
}
