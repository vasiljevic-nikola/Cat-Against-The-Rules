using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Book, Lamp, Laptop }
    public ObjectType type;
    public bool isDangerous;

    private bool isCleared = false;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDangerous && collision.gameObject.CompareTag("Player"))
        {
            gameManager.GameOver();
        }

        if (!isDangerous && collision.gameObject.CompareTag("Floor"))
        {
            HandleTaskCompletion();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillZone"))
        {
            if (!isDangerous)
            {
                HandleTaskCompletion();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void HandleTaskCompletion()
    {
        if (isCleared) return;
        isCleared = true;

        gameManager.CompleteTask(type);
        gameObject.SetActive(false);
    }
}
