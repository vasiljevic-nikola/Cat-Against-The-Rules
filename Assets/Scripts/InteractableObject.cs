using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Book, Lamp, Laptop, Key, Pillow, Armchair }
    public ObjectType type;

    private GameManager level1Manager;
    private GameManager_Level2 level2Manager;
    private bool taskCompleted = false;

    void Start()
    {
        // Pokušaj da pronađe oba, koristiće onaj koji nije null
        level1Manager = FindFirstObjectByType<GameManager>();
        level2Manager = FindFirstObjectByType<GameManager_Level2>();

        if (level1Manager == null && level2Manager == null)
        {
            Debug.LogError("FATAL ERROR: Neither GameManager (Level 1) nor GameManager_Level2 (Level 2) found!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (taskCompleted) return;

        // Logika za Nivo 1
        if (level1Manager != null)
        {
            // AKO JE OBJEKAT LAPTOP I SUDARI SE SA IGRAČEM -> GAME OVER
            if (type == ObjectType.Laptop && collision.gameObject.CompareTag("Player"))
            {
                taskCompleted = true;
                level1Manager.GameOver(ObjectType.Laptop);
            }
            // AKO JE OBJEKAT KNJIGA ILI LAMPA I SUDARI SE SA PODOM -> ZADATAK ZAVRŠEN
            else if ((type == ObjectType.Book || type == ObjectType.Lamp) && collision.gameObject.CompareTag("Floor"))
            {
                taskCompleted = true;
                level1Manager.CompleteTask(type);
            }
        }

        // Logika za Nivo 2
        if (level2Manager != null)
        {
            if (type == ObjectType.Armchair && collision.gameObject.CompareTag("Player"))
            {
                taskCompleted = true;
                level2Manager.GameOver();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (taskCompleted) return;

        // Logika za Nivo 2 
        if (level2Manager != null)
        {
            if (type == ObjectType.Key && other.CompareTag("PillowZone"))
            {
                taskCompleted = true;
                level2Manager.CompleteKeyTask();
            }
        }
    }
}
