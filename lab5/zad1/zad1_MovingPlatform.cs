using UnityEngine;

public class zad1_MovingPlatform : MonoBehaviour
{
    public float platformSpeed = 2f;         // Pr�dko�� platformy
    public float distanceToMove = 6.6f;      // Dystans do przemieszczenia (w g�r� i w d�)
    private bool isMoving = false;           // Czy platforma jest w ruchu
    private Vector3 startPosition;           // Pozycja pocz�tkowa
    private Vector3 targetPosition;          // Pozycja docelowa
    private Vector3 currentTarget;           // Bie��cy cel (pocz�tkowy lub docelowy)

    void Start()
    {
        // Inicjalizacja pozycji pocz�tkowej i docelowej
        startPosition = transform.position;
        targetPosition = transform.position + new Vector3(0, distanceToMove, 0); // Poruszamy w osi Y (g�ra-d�)
        currentTarget = startPosition; // Na pocz�tek ustawiamy cel na pozycj� pocz�tkow�
    }

    void Update()
    {
        if (isMoving)
        {
            // Przemieszczamy platform� w kierunku bie��cego celu
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, platformSpeed * Time.deltaTime);

            // Sprawdzamy, czy platforma osi�gn�a cel
            if (Vector3.Distance(transform.position, currentTarget) < 0.02f)
            {
                // Zmieniamy cel po dotarciu do punktu docelowego
                if (currentTarget == targetPosition)
                {
                    currentTarget = startPosition; // Wracamy do miejsca pocz�tkowego
                }
                else
                {
                    isMoving = false; // Zatrzymujemy platform�, gdy wr�ci do startu
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Rozpoczynamy ruch platformy, gdy gracz wejdzie na platform�
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gracz wszed� na platform�.");
            isMoving = true;
            currentTarget = targetPosition; // Platforma rusza do celu (w g�r�)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Zatrzymujemy platform�, gdy gracz z niej zejdzie
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gracz zszed� z platformy.");
            currentTarget = startPosition; // Platforma wraca do pozycji pocz�tkowej
        }
    }
}


