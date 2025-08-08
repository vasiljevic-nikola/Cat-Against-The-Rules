using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Book, Lamp, Laptop, Key, Pillow }
    public ObjectType type;

    private GameManager gameManager;
    private bool taskCompleted = false;

    void Start()
    {
        // Pronađi GameManager-a na početku
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("FATAL ERROR: GameManager not found in the scene!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ako je zadatak već izvršen, ne radi ništa
        if (taskCompleted) return;

        // Provera da li je objekat udario u pod (za zadatke)
        if (type == ObjectType.Book || type == ObjectType.Lamp)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                HandleTaskCompletion();
            }
        }
        // Provera da li je igrač udario u laptop (za neuspeh)
        else if (type == ObjectType.Laptop)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                HandleGameOver();
            }
        }
    }

    void HandleTaskCompletion()
    {
        taskCompleted = true;
        if (gameManager != null)
        {
            gameManager.CompleteTask(type);
        }

        // Opciono: Onemogući dalje sudare za ovaj objekat
        // GetComponent<Collider2D>().enabled = false;
    }

    void HandleGameOver()
    {
        taskCompleted = true; // Označi da je interakcija sa ovim objektom gotova
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Proveravamo da li je OVAJ objekat (na kojem je skripta) jastuk
        // i da li je objekat koji je ušao u njega ključ.
        if (type == ObjectType.Pillow && other.CompareTag("Key"))
        {
            // Ako je zadatak već izvršen, ne radi ništa
            if (taskCompleted) return;

            Debug.Log("Key has touched the pillow!");
            HandleTaskCompletion();
        }
    }

}
