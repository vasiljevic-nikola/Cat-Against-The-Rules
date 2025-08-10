using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType
    {
        Book,
        Lamp,
        Laptop,
        Key,
        Pillow,
        Armchair,
        Stove,
        CoffeeMachine,
        Blender
    }

    public ObjectType type;

    private GameManager level1Manager;
    private GameManager_Level2 level2Manager;
    private GameManager_Level3 level3Manager;

    private bool taskCompleted = false;

    void Start()
    {
        // Pronađi sve GameManagere koji mogu biti aktivni
        level1Manager = FindFirstObjectByType<GameManager>();
        level2Manager = FindFirstObjectByType<GameManager_Level2>();
        level3Manager = FindFirstObjectByType<GameManager_Level3>();

        if (level1Manager == null && level2Manager == null && level3Manager == null)
        {
            Debug.LogError("FATAL ERROR: No GameManager was found in the scene!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (taskCompleted) return;

        // ===== Level 1 =====
        if (level1Manager != null)
        {
            // Laptop -> Game Over (ako igrač udari)
            if (type == ObjectType.Laptop && collision.gameObject.CompareTag("Player"))
            {
                taskCompleted = true;
                level1Manager.GameOver(ObjectType.Laptop);
            }
            // Knjiga ili lampa -> CompleteTask (ako padne na pod)
            else if ((type == ObjectType.Book || type == ObjectType.Lamp) && collision.gameObject.CompareTag("Floor"))
            {
                taskCompleted = true;
                level1Manager.CompleteTask(type);
            }
        }

        // ===== Level 2 =====
        if (level2Manager != null)
        {
            // Fotelja -> Game Over (ako igrač udari)
            if (type == ObjectType.Armchair && collision.gameObject.CompareTag("Player"))
            {
                taskCompleted = true;
                level2Manager.GameOver();
            }
        }

        // ===== Level 3 =====
        if (level3Manager != null)
        {
            // Blender ili aparat za kafu -> CompleteTask (ako padne na pod)
            if ((type == ObjectType.Blender || type == ObjectType.CoffeeMachine) && collision.gameObject.CompareTag("Floor"))
            {
                taskCompleted = true;
                level3Manager.CompleteTask(type);
            }
            // Rerna -> Game Over (ako igrač udari)
            else if (type == ObjectType.Stove && collision.gameObject.CompareTag("Player"))
            {
                taskCompleted = true;
                level3Manager.GameOver(type);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (taskCompleted) return;

        // ===== Level 2 - Key Task =====
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
