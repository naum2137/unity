using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public Transform player; // Odno�nik do gracza
    public float fallThreshold = 15f; // Dopuszczalna odleg�o�� od najni�szej platformy
    public float minimumY = -20f; // Minimalna wysoko��, po kt�rej gracz przegrywa
    private List<GameObject> platforms = new List<GameObject>(); // Lista platform w grze
    public GameObject gameOverCanvas; // Przypisz Canvas w inspektorze

    private bool isGameOver = false; // Flaga stanu gry

    private void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Wy��cz Canvas na starcie
            Debug.Log("GameOverCanvas zosta� wy��czony na starcie.");
        }
        isGameOver = false; // Upewnij si�, �e gra zaczyna si� jako aktywna
    }

    private void Update()
    {
        if (isGameOver) return; // Je�li gra ju� si� sko�czy�a, nie wykonuj sprawdzania

        if (player != null)
        {
            // Sprawd�, czy gracz spad� za nisko wzgl�dem najni�szej platformy
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

        // Znajd� najni�ej po�o�on� platform�
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
        // Dodaj platform� do listy
        platforms.Add(platform);
    }

    public void RemovePlatform(GameObject platform)
    {
        // Usu� platform� z listy
        platforms.Remove(platform);
    }

    private void GameOver()
    {
        if (isGameOver) return; // Je�li gra ju� si� sko�czy�a, nie r�b nic
        isGameOver = true; // Ustaw flag� jako zako�czon�

        Debug.Log("GameOver() zosta�o wywo�ane!");

        // U�ycie nowej metody
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
        isGameOver = false; // Zresetuj flag� gry

        // Reset gracza
        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ResetPlayer(); // Reset pozycji i pr�dko�ci
        }

        // Reset platform
        var platformSpawner = FindObjectOfType<PlatformSpawner>();
        if (platformSpawner != null)
        {
            platformSpawner.ResetPlatforms(); // Usuni�cie i ponowne wygenerowanie platform
            ResetPlatforms(platformSpawner.GetPlatforms()); // Aktualizacja platform w GameOverManager
        }

        // Reset licznika odleg�o�ci
        var distanceCounter = FindObjectOfType<DistanceCounter>();
        if (distanceCounter != null)
        {
            distanceCounter.ResetCounter(); // Reset zmiennych
        }

        // Wy��cz GameOverCanvas
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
            Debug.Log("GameOverCanvas zosta� wy��czony w ResetGame.");
        }

        Debug.Log("Reset gry zako�czony.");
    }



    public void ResetPlatforms(List<GameObject> newPlatforms)
    {
        platforms.Clear();
        platforms.AddRange(newPlatforms);
    }


}
