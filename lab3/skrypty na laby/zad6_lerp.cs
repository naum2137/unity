using UnityEngine;

public class zad6_lerp : MonoBehaviour
{
    public Transform target; // Obiekt œledzony
    public Transform tracker; // Obiekt œledz¹cy

    public float smoothTime = 0.3f; // Czas wyg³adzania dla SmoothDamp
    public float lerpSpeed = 5.0f; // Prêdkoœæ dla Lerp

    private Vector3 velocity = Vector3.zero; // Prêdkoœæ dla SmoothDamp

    void Update()
    {
        Vector3 targetPosition = target.position;

        // Lerp: œledzenie obiektu
         tracker.position = Vector3.Lerp(tracker.position, targetPosition, lerpSpeed * Time.deltaTime);
    }
}
