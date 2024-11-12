using UnityEngine;
using System.Collections.Generic;

public class zad3_MovingPlatform : MonoBehaviour
{
    public List<Transform> waypoints;  // Lista punktów do których ma siê poruszaæ platforma
    public float speed = 2f;            // Prêdkoœæ platformy
    private int currentWaypointIndex = 0; // Indeks aktualnego punktu
    private bool isMovingForward = true;  // Kierunek poruszania (do przodu lub wstecz)

    void Update()
    {
        if (waypoints.Count > 0)
        {
            // Ruch platformy do kolejnego punktu
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            // Sprawdzanie, czy platforma dotar³a do punktu
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                // Zmiana kierunku, jeœli dotar³a do ostatniego punktu
                if (isMovingForward)
                {
                    currentWaypointIndex++;
                    if (currentWaypointIndex >= waypoints.Count)
                    {
                        isMovingForward = false; // Zawracamy, jeœli dotarliœmy do koñca
                        currentWaypointIndex = waypoints.Count - 2; // Przechodzimy do poprzedniego punktu
                    }
                }
                else
                {
                    currentWaypointIndex--;
                    if (currentWaypointIndex < 0)
                    {
                        isMovingForward = true; // Zawracamy, jeœli dotarliœmy na pocz¹tek
                        currentWaypointIndex = 1; // Ustawiamy siê na drugi punkt
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Rozpoczynamy ruch platformy, gdy gracz na ni¹ wejdzie
            Debug.Log("Gracz wszed³ na platformê.");
        }
    }
}
