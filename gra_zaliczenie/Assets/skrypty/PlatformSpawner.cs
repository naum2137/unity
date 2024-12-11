using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Prefab platformy
    public Transform player; // Odno�nik do gracza
    public int maxPlatforms = 15; // Maksymalna liczba platform na ekranie
    public float xRange = 5f; // Zakres poziomy generowania platform
    public float minYSpacing = 2f; // Minimalny odst�p mi�dzy platformami w osi Y
    public float maxYSpacing = 4f; // Maksymalny odst�p mi�dzy platformami w osi Y
    public float removeThreshold = 10f; // Odleg�o��, po kt�rej platformy b�d� usuwane
    public float minWidth = 5f; // Minimalna szeroko�� platformy
    public float maxWidth = 10f; // Maksymalna szeroko�� platformy
    public float startHeight = -3f; // Wysoko�� pocz�tkowej platformy
    public float widthDecrement = 0.2f; // O ile zmniejsza si� szeroko�� platformy co 30 platform
    public float heightIncrement = 0.05f; // O ile zwi�ksza si� odst�p w pionie co 30 platform
    public float minWidthLimit = 2f; // Minimalna warto�� dla minWidth
    public float maxXRangeLimit = 10f; // Maksymalny limit dla xRange

    private List<GameObject> platforms = new List<GameObject>(); // Lista istniej�cych platform
    private GameOverManager gameOverManager; // Odno�nik do GameOverManager
    private int platformCounter = 0; // Licznik platform
    private Color currentColor = Color.white; // Bie��cy kolor platformy

    private void Start()
    {
        // Znalezienie GameOverManager w scenie
        gameOverManager = Object.FindFirstObjectByType<GameOverManager>();

        // Generowanie pocz�tkowych platform
        Vector3 spawnPosition = new Vector3(0, startHeight, 0);
        for (int i = 0; i < maxPlatforms; i++)
        {
            spawnPosition.x = Random.Range(-xRange, xRange);
            spawnPosition.y += Random.Range(minYSpacing, maxYSpacing); // Losowy odst�p na osi Y
            GameObject newPlatform = InstantiatePlatform(spawnPosition);
            platforms.Add(newPlatform);

            // Dodanie platformy do GameOverManager
            gameOverManager?.AddPlatform(newPlatform);
        }
    }

    private void Update()
    {
        // Usu� platformy, kt�re s� poni�ej gracza
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            if (platforms[i].transform.position.y < player.position.y - removeThreshold)
            {
                Destroy(platforms[i]);
                gameOverManager?.RemovePlatform(platforms[i]); // Usu� platform� z GameOverManager
                platforms.RemoveAt(i);
            }
        }

        // Dodaj nowe platformy, je�li jest ich mniej ni� maksymalna liczba
        while (platforms.Count < maxPlatforms)
        {
            Vector3 spawnPosition = platforms[platforms.Count - 1].transform.position;
            spawnPosition.x = Random.Range(-xRange, xRange);
            spawnPosition.y += Random.Range(minYSpacing, maxYSpacing);
            GameObject newPlatform = InstantiatePlatform(spawnPosition);
            platforms.Add(newPlatform);

            // Dodanie nowej platformy do GameOverManager
            gameOverManager?.AddPlatform(newPlatform);
        }
    }

    private GameObject InstantiatePlatform(Vector3 spawnPosition)
    {
        platformCounter++;

        // Co 30 platform zmniejsz szeroko��, zwi�ksz odst�p, dostosuj xRange i zmie� kolor
        if (platformCounter % 30 == 0)
        {
            maxWidth = Mathf.Max(minWidth, maxWidth - widthDecrement); // Ograniczenie do minimalnej szeroko�ci
            minWidth = Mathf.Max(minWidthLimit, minWidth - widthDecrement); // Ograniczenie do minimalnej warto�ci dla minWidth
            minYSpacing = Mathf.Min(maxYSpacing, minYSpacing + heightIncrement); // Ograniczenie do maksymalnej wysoko�ci
            xRange = Mathf.Min(maxXRangeLimit, 10f - (maxWidth / 2f)); // Dostosuj xRange

            // Zmie� kolor na losowy
            currentColor = new Color(Random.value, Random.value, Random.value);
        }

        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        AdjustPlatformWidth(newPlatform);

        // Ustaw kolor platformy
        SetPlatformColor(newPlatform, currentColor);

        return newPlatform;
    }

    private void AdjustPlatformWidth(GameObject platform)
    {
        // Ustaw losow� szeroko�� platformy w aktualnym zakresie
        float randomWidth = Random.Range(minWidth, maxWidth);

        // Zmie� skal� platformy w osi X
        Transform platformTransform = platform.transform;
        Vector3 scale = platformTransform.localScale;
        scale.x = randomWidth; // Ustawiamy szeroko��
        platformTransform.localScale = scale;

        // Nie zmieniamy rozmiaru ani offsetu BoxCollider2D, poniewa� skaluje si� automatycznie
        BoxCollider2D collider = platform.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.size = new Vector2(1, 1); // Ustawiamy rozmiar collidera na domy�lny
            collider.offset = Vector2.zero; // Wycentrowanie collidera
        }
    }

    private void SetPlatformColor(GameObject platform, Color color)
    {
        // Ustaw kolor platformy, zak�adaj�c �e ma komponent SpriteRenderer
        SpriteRenderer renderer = platform.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
        }
    }

    public void ResetPlatforms()
    {
        // Usu� wszystkie istniej�ce platformy
        foreach (GameObject platform in platforms)
        {
            if (platform != null)
            {
                Destroy(platform);
            }
        }
        platforms.Clear(); // Wyczy�� list� platform
        platformCounter = 0; // Reset licznika platform
        maxWidth = 10f; // Reset szeroko�ci do domy�lnej warto�ci
        minWidth = 5f; // Reset minimalnej szeroko�ci do domy�lnej warto�ci
        xRange = 5f; // Reset xRange do domy�lnej warto�ci
        minYSpacing = 2f; // Reset odst�pu do domy�lnej warto�ci
        currentColor = Color.white; // Reset koloru platform

        // Wygeneruj nowe platformy
        Vector3 spawnPosition = new Vector3(0, startHeight, 0);
        for (int i = 0; i < maxPlatforms; i++)
        {
            spawnPosition.x = Random.Range(-xRange, xRange);
            spawnPosition.y += Random.Range(minYSpacing, maxYSpacing);
            GameObject newPlatform = InstantiatePlatform(spawnPosition);
            platforms.Add(newPlatform);

            // Zarejestruj nowe platformy w GameOverManager
            if (gameOverManager != null)
            {
                gameOverManager.AddPlatform(newPlatform);
            }
        }
    }

    public List<GameObject> GetPlatforms()
    {
        return new List<GameObject>(platforms); // Zwraca kopi� listy platform
    }
}