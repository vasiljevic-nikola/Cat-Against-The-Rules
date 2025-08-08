using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Book, Lamp, Laptop, Key, Armchair }
    public ObjectType type;

    private GameManager_Level2 gameManager;
    private bool taskCompleted = false;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager_Level2>();
        if (gameManager == null)
        {
            Debug.LogError("FATAL ERROR: GameManager_Level2 not found!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (taskCompleted) return;
        if (type == ObjectType.Armchair && collision.gameObject.CompareTag("Player"))
        {
            HandleGameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (taskCompleted) return;
        if (type == ObjectType.Key && other.CompareTag("PillowZone"))
        {
            Debug.Log("Key has entered the PillowZone!");
            HandleTaskCompletionForKey();
        }
    }

    // ISPRAVLJENA FUNKCIJA
    void HandleTaskCompletionForKey()
    {
        taskCompleted = true;
        if (gameManager != null)
        {
            // Sada pozivamo novu, specijalnu funkciju na GameManager-u
            gameManager.CompleteKeyTask();
        }
    }

    void HandleGameOver()
    {
        taskCompleted = true;
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }
}
