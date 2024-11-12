using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zad5_HasPlayerObjectCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("TAG"))
        {
            Debug.Log("Wykryto zderzenie z przeszkod¹ z tagiem ObstacleTag " + hit.gameObject.name);
        }
    }
}