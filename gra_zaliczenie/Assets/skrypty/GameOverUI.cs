using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverCanvas;

    private void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Ukryj Canvas na starcie
        }
        else
        {
            Debug.LogError("Canvas Game Over nie jest przypisany!");
        }
    }

    public void ShowGameOverScreen()
    {
        if (gameOverCanvas != null)
        {
            Debug.Log("Aktywujê Canvas Game Over!"); 
            gameOverCanvas.SetActive(true); // Aktywuj Canvas
            
        }
        else
        {
            Debug.LogError("Canvas Game Over nie jest przypisany!");
        }
    }

    public void RetryGame()
    {
        var gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ResetGame(); 
        }

        // Ukryj GameOverCanvas
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
            Debug.Log("GameOverCanvas zosta³ wy³¹czony.");
        }

        // Usuñ focus z UI
        EventSystem.current?.SetSelectedGameObject(null);

        // Usuñ focus z UI
        UnityEngine.EventSystems.EventSystem.current?.SetSelectedGameObject(null);
    }


}
