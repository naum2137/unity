using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Odno�nik do gracza
    public Vector3 offset;   // Dystans mi�dzy kamer� a graczem
    public float smoothSpeed = 0.125f; // Pr�dko�� "g�adkiego" ruchu kamery

    private void LateUpdate()
    {
        if (player != null)
        {
            // �ledzenie tylko osi Y
            Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, offset.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

}
