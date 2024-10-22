using UnityEngine;

public class zad4 : MonoBehaviour
{
    public float speed = 5.0f; // Pr�dko�� poruszania si�

    void Update()
    {
        // Pobierz warto�ci z klawiszy WSAD lub strza�ek
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Stw�rz wektor ruchu
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Przemie�� gracza po p�aszczy�nie
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
