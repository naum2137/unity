using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public Transform player; // Odnoœnik do gracza
    public float fallThreshold = 15f; // Dopuszczalna odleg³oœæ od najni¿szej platformy
    public float minimumY = -20f; // Minimalna wysokoœæ, po której gracz przegrywa
    private List<GameObject> platforms = new List<GameObject>(); // Lista platform w grze
    public GameObject gameOverCanvas; // Przypisz Canvas w inspektorze

    private bool isGameOver = false; // Flaga stanu gry

    private void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Wy³¹cz Canvas na starcie
            Debug.Log("GameOverCanvas zosta³ wy³¹czony na starcie.");
        }
        isGameOver = false; // Upewnij siê, ¿e gra zaczyna siê jako aktywna
    }

    private void Update()
    {
        if (isGameOver) return; // Jeœli gra ju¿ siê skoñczy³a, nie wykonuj sprawdzania

        if (player != null)
        {
            // SprawdŸ, czy gracz spad³ za nisko wzglêdem najni¿szej platformy
            float lowestPlatformY = GetLowestPlatformY();
            if (player.position.y < lowestPlatformY - fallThreshold || player.position.y < minimumY)
            {
                GameOver();
            }
        }
    }

    private float GetLowestPlatformY()
    {
        float lowestY = float.MaxValue;

        // ZnajdŸ najni¿ej po³o¿on¹ platformê
        foreach (GameObject platform in platforms)
        {
            if (platform != null)
            {
                float platformY = platform.transform.position.y;
                if (platformY < lowestY)
                {
                    lowestY = platformY;
                }
            }
        }

        return lowestY;
    }

    public void AddPlatform(GameObject platform)
    {
        // Dodaj platformê do listy
        platforms.Add(platform);
    }

    public void RemovePlatform(GameObject platform)
    {
        // Usuñ platformê z listy
        platforms.Remove(platform);
    }

    private void GameOver()
    {
        if (isGameOver) return; // Jeœli gra ju¿ siê skoñczy³a, nie rób nic
        isGameOver = true; // Ustaw flagê jako zakoñczon¹

        Debug.Log("GameOver() zosta³o wywo³ane!");

        // U¿ycie nowej metody
        var gameOverUI = Object.FindFirstObjectByType<GameOverUI>();

        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOverScreen();
        }
        else
        {
            Debug.LogError("Nie znaleziono GameOverUI w scenie!");
        }
    }

    public void ResetGame()
    {
        isGameOver = false; // Zresetuj flagê gry

        // Reset gracza
        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ResetPlayer(); // Reset pozycji i prêdkoœci
        }

        // Reset platform
        var platformSpawner = FindObjectOfType<PlatformSpawner>();
        if (platformSpawner != null)
        {
            platformSpawner.ResetPlatforms(); // Usuniêcie i ponowne wygenerowanie platform
            ResetPlatforms(platformSpawner.GetPlatforms()); // Aktualizacja platform w GameOverManager
        }

        // Reset licznika odleg³oœci
        var distanceCounter = FindObjectOfType<DistanceCounter>();
        if (distanceCounter != null)
        {
            distanceCounter.ResetCounter(); // Reset zmiennych
        }

        // Wy³¹cz GameOverCanvas
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
            Debug.Log("GameOverCanvas zosta³ wy³¹czony w ResetGame.");
        }

        Debug.Log("Reset gry zakoñczony.");
    }



    public void ResetPlatforms(List<GameObject> newPlatforms)
    {
        platforms.Clear();
        platforms.AddRange(newPlatforms);
    }


}
