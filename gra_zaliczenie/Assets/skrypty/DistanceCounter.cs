using TMPro;
using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    public Transform player; // Odno�nik do gracza
    public TextMeshProUGUI distanceText; // Tekst do wy�wietlania odleg�o�ci
    public TextMeshProUGUI maxHeightText; // Tekst do wy�wietlania maksymalnej wysoko�ci w danej rundzie
    public TextMeshProUGUI sessionMaxHeightText; // Tekst do wy�wietlania maksymalnej wysoko�ci w ca�ej sesji

    private float startingY; // Pozycja pocz�tkowa gracza na osi Y
    private float maxHeight; // Najwi�ksza osi�gni�ta wysoko�� w danej rundzie
    private float sessionMaxHeight; // Najwi�ksza osi�gni�ta wysoko�� w ca�ej sesji grania

    private void Start()
    {
        if (player != null)
        {
            startingY = player.position.y; // Ustaw pozycj� pocz�tkow� gracza
        }
        maxHeight = 0; // Reset maksymalnej wysoko�ci w rundzie
        sessionMaxHeight = 0; // Na pocz�tku gry maksymalna wysoko�� w sesji wynosi 0
    }

    private void Update()
    {
        if (player != null)
        {
            // Oblicz przebywan� odleg�o�� w osi Y
            float distanceTravelled = player.position.y - startingY;

            // Zaokr�glij odleg�o�� do 1 miejsca po przecinku
            distanceTravelled = Mathf.Max(0, Mathf.Round(distanceTravelled * 10f) / 10f);

            // Zaktualizuj maksymaln� wysoko�� w danej rundzie
            if (distanceTravelled > maxHeight)
            {
                maxHeight = distanceTravelled;
            }

            // Zaktualizuj maksymaln� wysoko�� w ca�ej sesji
            if (maxHeight > sessionMaxHeight)
            {
                sessionMaxHeight = maxHeight;
            }

            // Wy�wietl przebywan� odleg�o�� na ekranie
            distanceText.text = "Distance: " + distanceTravelled.ToString("F1");

            // Wy�wietl maksymaln� osi�gni�t� wysoko�� w danej rundzie
            maxHeightText.text = "Max Height: " + maxHeight.ToString("F1");

            // Wy�wietl maksymaln� wysoko�� w ca�ej sesji
            if (sessionMaxHeightText != null)
            {
                sessionMaxHeightText.text = "Session Max: " + sessionMaxHeight.ToString("F1");
            }
        }
    }

    public void ResetCounter()
    {
        if (player != null)
        {
            startingY = player.position.y;
        }
        maxHeight = 0; // Reset maksymalnej wysoko�ci w bie��cej rundzie
    }

}
