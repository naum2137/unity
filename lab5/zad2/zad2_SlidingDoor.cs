using UnityEngine;

public class zad2_SlidingDoor : MonoBehaviour
{
    public float doorSpeed = 2f;             // Prêdkoœæ otwierania drzwi
    private Vector3 closedPosition;          // Pozycja zamkniêtych drzwi
    private Vector3 openPosition;            // Pozycja otwartych drzwi
    private bool isOpening = false;          // Czy drzwi s¹ otwierane?

    void Start()
    {
        closedPosition = transform.position;
        openPosition = transform.position + new Vector3(3f, 0, 0);  // Otwieranie w prawo
    }

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, doorSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, doorSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gracz wszed³ do obszaru triggera, otwieramy drzwi
            isOpening = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gracz opuœci³ obszar triggera, drzwi wracaj¹ do pozycji zamkniêtej
            isOpening = false;
        }
    }
}


