using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private GameManager_Level2 gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager_Level2>();
    }

    private void OnTriggerEnter2D(Collider2D other) // ← 2D verzija
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player went through the trigger door.");
            if (gameManager != null)
            {
                gameManager.PlayerWentThroughDoor();
            }
            else
            {
                Debug.LogWarning("GameManager_Level2 not found!");
            }
        }
    }
}
