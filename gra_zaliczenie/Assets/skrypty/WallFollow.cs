using UnityEngine;

public class WallFollow : MonoBehaviour
{
    public Transform player; // Odno�nik do gracza

    private void Update()
    {
        if (player != null)
        {
            // Pod��anie za graczem tylko na osi Y
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }
}
