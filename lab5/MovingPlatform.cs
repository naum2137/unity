using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float elevatorSpeed = 2f;         // Prêdkoœæ windy
    public float distanceToMove = 6.6f;      // Dystans do przemieszczenia
    private bool isRunning = false;          // Czy winda jest w ruchu
    private Vector3 downPosition;            // Pozycja dolna
    private Vector3 upPosition;              // Pozycja górna
    private Vector3 currentTarget;           // Bie¿¹cy cel (górny lub dolny)

    void Start()
    {
        // Inicjalizacja pozycji górnej i dolnej
        downPosition = transform.position;
        upPosition = transform.position + new Vector3(0, distanceToMove, 0);
        currentTarget = upPosition; // Na pocz¹tek ustalamy cel na pozycjê górn¹
    }

    void Update()
    {
        if (isRunning)
        {
            // Przemieszczamy windê w kierunku bie¿¹cego celu
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, elevatorSpeed * Time.deltaTime);

            // Sprawdzamy, czy winda osi¹gnê³a cel
            if (Vector3.Distance(transform.position, currentTarget) < 0.02f)
            {
                // Zmieniamy cel po dotarciu do punktu docelowego
                currentTarget = (currentTarget == upPosition) ? downPosition : upPosition;
                isRunning = false; // Zatrzymujemy windê po dotarciu do celu
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Rozpoczynamy ruch windy, gdy gracz wejdzie na platformê
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player wszed³ na windê.");
            isRunning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player zszed³ z windy.");
        }
    }
}


