using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform pointA; // Punkt A
    public Transform pointB; // Punkt B
    public Transform player; // Postaæ gracza
    public float speed = 2f; // Prêdkoœæ ruchu
    public float detectionRadius = 5f; // Zasiêg reakcji
    public LayerMask playerLayer; // Warstwa gracza

    private Vector3 target; // Aktualny cel wroga

    void Start()
    {
        target = pointA.position; // Wróg zaczyna od punktu A
    }

    void Update()
    {
        if (IsPlayerInDetectionZone())
        {
            FollowPlayer(); // Pod¹¿aj za graczem
        }
        else
        {
            Patrol(); // Powrót do patrolowania
        }
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Zmieñ cel, gdy wróg dotrze do punktu
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    void FollowPlayer()
    {
        // Pod¹¿aj tylko na osi X miêdzy punktami A i B
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
        // SprawdŸ, czy gracz jest w prostok¹tnej strefie detekcji
        return Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer) != null;
    }

    void OnDrawGizmos()
    {
        // Rysowanie zasiêgu wykrywania w edytorze
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
