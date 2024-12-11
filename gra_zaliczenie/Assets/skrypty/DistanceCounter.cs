using TMPro;
using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    public Transform player; // Odnoœnik do gracza
    public TextMeshProUGUI distanceText; // Tekst do wyœwietlania odleg³oœci
    public TextMeshProUGUI maxHeightText; // Tekst do wyœwietlania maksymalnej wysokoœci w danej rundzie
    public TextMeshProUGUI sessionMaxHeightText; // Tekst do wyœwietlania maksymalnej wysokoœci w ca³ej sesji

    private float startingY; // Pozycja pocz¹tkowa gracza na osi Y
    private float maxHeight; // Najwiêksza osi¹gniêta wysokoœæ w danej rundzie
    private float sessionMaxHeight; // Najwiêksza osi¹gniêta wysokoœæ w ca³ej sesji grania

    private void Start()
    {
        if (player != null)
        {
            startingY = player.position.y; // Ustaw pozycjê pocz¹tkow¹ gracza
        }
        maxHeight = 0; // Reset maksymalnej wysokoœci w rundzie
        sessionMaxHeight = 0; // Na pocz¹tku gry maksymalna wysokoœæ w sesji wynosi 0
    }

    private void Update()
    {
        if (player != null)
        {
            // Oblicz przebywan¹ odleg³oœæ w osi Y
            float distanceTravelled = player.position.y - startingY;

            // Zaokr¹glij odleg³oœæ do 1 miejsca po przecinku
            distanceTravelled = Mathf.Max(0, Mathf.Round(distanceTravelled * 10f) / 10f);

            // Zaktualizuj maksymaln¹ wysokoœæ w danej rundzie
            if (distanceTravelled > maxHeight)
            {
                maxHeight = distanceTravelled;
            }

            // Zaktualizuj maksymaln¹ wysokoœæ w ca³ej sesji
            if (maxHeight > sessionMaxHeight)
            {
                sessionMaxHeight = maxHeight;
            }

            // Wyœwietl przebywan¹ odleg³oœæ na ekranie
            distanceText.text = "Distance: " + distanceTravelled.ToString("F1");

            // Wyœwietl maksymaln¹ osi¹gniêt¹ wysokoœæ w danej rundzie
            maxHeightText.text = "Max Height: " + maxHeight.ToString("F1");

            // Wyœwietl maksymaln¹ wysokoœæ w ca³ej sesji
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
        maxHeight = 0; // Reset maksymalnej wysokoœci w bie¿¹cej rundzie
    }

}
