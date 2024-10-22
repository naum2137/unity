using UnityEngine;

public class zad3 : MonoBehaviour
{
    public float speed = 2.0f; // Pr�dko�� poruszania si�
    private Vector3[] directions = new Vector3[] {
        Vector3.right,   // Ruch w prawo
        Vector3.forward, // Ruch do przodu
        Vector3.left,    // Ruch w lewo
        Vector3.back     // Ruch do ty�u
    };
    private int currentDirectionIndex = 0; // Index kierunku
    private Vector3 startPosition; // Pozycja pocz�tkowa
    private Vector3 targetPosition; // Pozycja docelowa
    private float distance = 10.0f; // Odleg�o�� do pokonania (bok kwadratu)

    void Start()
    {
        // Ustaw pocz�tkow� pozycj� i cel
        startPosition = transform.position;
        targetPosition = startPosition + directions[currentDirectionIndex] * distance;
    }

    void Update()
    {
        // Poruszaj obiekt w kierunku docelowym
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Sprawd�, czy osi�gn�li�my cel
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Obr�� Cube o 90 stopni wok� osi Y
            transform.Rotate(Vector3.up, -90);

            // Zaktualizuj kierunek ruchu
            currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;

            // Ustaw nowy cel
            startPosition = transform.position;
            targetPosition = startPosition + directions[currentDirectionIndex] * distance;
        }
    }
}
