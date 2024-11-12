using UnityEngine;

public class zad2_SlidingDoor : MonoBehaviour
{
    public float doorSpeed = 2f;             // Pr�dko�� otwierania drzwi
    private Vector3 closedPosition;          // Pozycja zamkni�tych drzwi
    private Vector3 openPosition;            // Pozycja otwartych drzwi
    private bool isOpening = false;          // Czy drzwi s� otwierane?

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
            // Gracz wszed� do obszaru triggera, otwieramy drzwi
            isOpening = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gracz opu�ci� obszar triggera, drzwi wracaj� do pozycji zamkni�tej
            isOpening = false;
        }
    }
}


