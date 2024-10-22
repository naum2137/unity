using UnityEngine;

public class zad4 : MonoBehaviour
{
    public float speed = 5.0f; // Prêdkoœæ poruszania siê

    void Update()
    {
        // Pobierz wartoœci z klawiszy WSAD lub strza³ek
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Stwórz wektor ruchu
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Przemieœæ gracza po p³aszczyŸnie
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
