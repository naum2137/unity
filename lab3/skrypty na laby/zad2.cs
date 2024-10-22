using UnityEngine;

public class zad2 : MonoBehaviour
{
    public float speed = 2.0f; // Prêdkoœæ przesuwania
    private float startX; // Pozycja pocz¹tkowa w osi X
    private float targetX = 10.0f; // Cel, do którego przesuwamy
    private bool movingForward = true; // Czy poruszamy siê do przodu?

    void Start()
    {
        // Zapisz pocz¹tkow¹ pozycjê X Cube'a
        startX = transform.position.x;
    }

    void Update()
    {
        // SprawdŸ kierunek ruchu
        if (movingForward)
        {
            // Przesuwanie Cube’a do przodu
            transform.position += Vector3.right * speed * Time.deltaTime;

            // SprawdŸ, czy osi¹gn¹³ cel
            if (transform.position.x >= startX + targetX)
            {
                movingForward = false; // Zmieñ kierunek na powrót
            }
        }
        else
        {
            // Przesuwanie Cube’a wstecz
            transform.position -= Vector3.right * speed * Time.deltaTime;

            // SprawdŸ, czy wróci³ do pozycji pocz¹tkowej
            if (transform.position.x <= startX)
            {
                movingForward = true; // Zmieñ kierunek na do przodu
            }
        }
    }
}
