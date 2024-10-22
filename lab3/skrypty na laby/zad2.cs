using UnityEngine;

public class zad2 : MonoBehaviour
{
    public float speed = 2.0f; // Pr�dko�� przesuwania
    private float startX; // Pozycja pocz�tkowa w osi X
    private float targetX = 10.0f; // Cel, do kt�rego przesuwamy
    private bool movingForward = true; // Czy poruszamy si� do przodu?

    void Start()
    {
        // Zapisz pocz�tkow� pozycj� X Cube'a
        startX = transform.position.x;
    }

    void Update()
    {
        // Sprawd� kierunek ruchu
        if (movingForward)
        {
            // Przesuwanie Cube�a do przodu
            transform.position += Vector3.right * speed * Time.deltaTime;

            // Sprawd�, czy osi�gn�� cel
            if (transform.position.x >= startX + targetX)
            {
                movingForward = false; // Zmie� kierunek na powr�t
            }
        }
        else
        {
            // Przesuwanie Cube�a wstecz
            transform.position -= Vector3.right * speed * Time.deltaTime;

            // Sprawd�, czy wr�ci� do pozycji pocz�tkowej
            if (transform.position.x <= startX)
            {
                movingForward = true; // Zmie� kierunek na do przodu
            }
        }
    }
}
