using UnityEngine;

public class zad3 : MonoBehaviour
{
    public float speed = 2.0f; // Prêdkoœæ poruszania siê
    private Vector3[] directions = new Vector3[] {
        Vector3.right,   // Ruch w prawo
        Vector3.forward, // Ruch do przodu
        Vector3.left,    // Ruch w lewo
        Vector3.back     // Ruch do ty³u
    };
    private int currentDirectionIndex = 0; // Index kierunku
    private Vector3 startPosition; // Pozycja pocz¹tkowa
    private Vector3 targetPosition; // Pozycja docelowa
    private float distance = 10.0f; // Odleg³oœæ do pokonania (bok kwadratu)

    void Start()
    {
        // Ustaw pocz¹tkow¹ pozycjê i cel
        startPosition = transform.position;
        targetPosition = startPosition + directions[currentDirectionIndex] * distance;
    }

    void Update()
    {
        // Poruszaj obiekt w kierunku docelowym
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // SprawdŸ, czy osi¹gnêliœmy cel
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Obróæ Cube o 90 stopni wokó³ osi Y
            transform.Rotate(Vector3.up, -90);

            // Zaktualizuj kierunek ruchu
            currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;

            // Ustaw nowy cel
            startPosition = transform.position;
            targetPosition = startPosition + directions[currentDirectionIndex] * distance;
        }
    }
}
