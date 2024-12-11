using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Odnoœnik do gracza
    public Vector3 offset;   // Dystans miêdzy kamer¹ a graczem
    public float smoothSpeed = 0.125f; // Prêdkoœæ "g³adkiego" ruchu kamery

    private void LateUpdate()
    {
        if (player != null)
        {
            // Œledzenie tylko osi Y
            Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, offset.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

}
