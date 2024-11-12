using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float elevatorSpeed = 2f;         // Pr�dko�� windy
    public float distanceToMove = 6.6f;      // Dystans do przemieszczenia
    private bool isRunning = false;          // Czy winda jest w ruchu
    private Vector3 downPosition;            // Pozycja dolna
    private Vector3 upPosition;              // Pozycja g�rna
    private Vector3 currentTarget;           // Bie��cy cel (g�rny lub dolny)

    void Start()
    {
        // Inicjalizacja pozycji g�rnej i dolnej
        downPosition = transform.position;
        upPosition = transform.position + new Vector3(0, distanceToMove, 0);
        currentTarget = upPosition; // Na pocz�tek ustalamy cel na pozycj� g�rn�
    }

    void Update()
    {
        if (isRunning)
        {
            // Przemieszczamy wind� w kierunku bie��cego celu
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, elevatorSpeed * Time.deltaTime);

            // Sprawdzamy, czy winda osi�gn�a cel
            if (Vector3.Distance(transform.position, currentTarget) < 0.02f)
            {
                // Zmieniamy cel po dotarciu do punktu docelowego
                currentTarget = (currentTarget == upPosition) ? downPosition : upPosition;
                isRunning = false; // Zatrzymujemy wind� po dotarciu do celu
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Rozpoczynamy ruch windy, gdy gracz wejdzie na platform�
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player wszed� na wind�.");
            isRunning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player zszed� z windy.");
        }
    }
}


