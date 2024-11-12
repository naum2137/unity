using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zad4_PressurePlateJumpTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("WSZEDL NA PLATFORME");
            zad4_MoveWithCharacterController characterController = collider.GetComponent<zad4_MoveWithCharacterController>();

            if (characterController != null)
            {
                characterController.JumpWithForce(3.0f);
            }

        }
    }


}