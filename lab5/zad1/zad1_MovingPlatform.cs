using UnityEngine;

public class zad1_MovingPlatform : MonoBehaviour
{
    public float platformSpeed = 2f;         // Prêdkoœæ platformy
    public float distanceToMove = 6.6f;      // Dystans do przemieszczenia (w górê i w dó³)
    private bool isMoving = false;           // Czy platforma jest w ruchu
    private Vector3 startPosition;           // Pozycja pocz¹tkowa
    private Vector3 targetPosition;          // Pozycja docelowa
    private Vector3 currentTarget;           // Bie¿¹cy cel (pocz¹tkowy lub docelowy)

    void Start()
    {
        // Inicjalizacja pozycji pocz¹tkowej i docelowej
        startPosition = transform.position;
        targetPosition = transform.position + new Vector3(0, distanceToMove, 0); // Poruszamy w osi Y (góra-dó³)
        currentTarget = startPosition; // Na pocz¹tek ustawiamy cel na pozycjê pocz¹tkow¹
    }

    void Update()
    {
        if (isMoving)
        {
            // Przemieszczamy platformê w kierunku bie¿¹cego celu
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, platformSpeed * Time.deltaTime);

            // Sprawdzamy, czy platforma osi¹gnê³a cel
            if (Vector3.Distance(transform.position, currentTarget) < 0.02f)
            {
                // Zmieniamy cel po dotarciu do punktu docelowego
                if (currentTarget == targetPosition)
                {
                    currentTarget = startPosition; // Wracamy do miejsca pocz¹tkowego
                }
                else
                {
                    isMoving = false; // Zatrzymujemy platformê, gdy wróci do startu
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Rozpoczynamy ruch platformy, gdy gracz wejdzie na platformê
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gracz wszed³ na platformê.");
            isMoving = true;
            currentTarget = targetPosition; // Platforma rusza do celu (w górê)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Zatrzymujemy platformê, gdy gracz z niej zejdzie
        if (other.CompareTag("Player"))
        {
            Debug.Log("Gracz zszed³ z platformy.");
            currentTarget = startPosition; // Platforma wraca do pozycji pocz¹tkowej
        }
    }
}


