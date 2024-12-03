using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform pointA; // Punkt A
    public Transform pointB; // Punkt B
    public Transform player; // Posta� gracza
    public float speed = 2f; // Pr�dko�� ruchu
    public float detectionRadius = 5f; // Zasi�g reakcji
    public LayerMask playerLayer; // Warstwa gracza

    private Vector3 target; // Aktualny cel wroga

    void Start()
    {
        target = pointA.position; // Wr�g zaczyna od punktu A
    }

    void Update()
    {
        if (IsPlayerInDetectionZone())
        {
            FollowPlayer(); // Pod��aj za graczem
        }
        else
        {
            Patrol(); // Powr�t do patrolowania
        }
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Zmie� cel, gdy wr�g dotrze do punktu
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    void FollowPlayer()
    {
        // Pod��aj tylko na osi X mi�dzy punktami A i B
        Vector3 directionToPlayer = new Vector3(player.position.x - transform.position.x, 0, 0);
        Vector3 nextPosition = transform.position + directionToPlayer.normalized * speed * Time.deltaTime;

        if (IsWithinBounds(nextPosition, pointA.position, pointB.position))
        {
            transform.position = nextPosition;
        }
    }

    bool IsWithinBounds(Vector3 position, Vector3 boundA, Vector3 boundB)
    {
        float minX = Mathf.Min(boundA.x, boundB.x);
        float maxX = Mathf.Max(boundA.x, boundB.x);
        return position.x >= minX && position.x <= maxX;
    }

    bool IsPlayerInDetectionZone()
    {
        // Sprawd�, czy gracz jest w prostok�tnej strefie detekcji
        return Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer) != null;
    }

    void OnDrawGizmos()
    {
        // Rysowanie zasi�gu wykrywania w edytorze
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
