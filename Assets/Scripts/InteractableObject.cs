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
    public GameObject dustEffectPrefab;

    private GameManager level1Manager;
    private GameManager_Level2 level2Manager;
    private GameManager_Level3 level3Manager;

    private bool taskCompleted = false;

    void Start()
    {
        // Find all GameManagers that may be active
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
        Debug.Log($"{name} collided with {collision.gameObject.name} (tag: {collision.gameObject.tag}) at {Time.time}");
        if (taskCompleted) return;

        // Level 1
        if (level1Manager != null)
        {
            // Laptop -> Game Over
            if (type == ObjectType.Laptop && collision.gameObject.CompareTag("Player"))
            {
                taskCompleted = true;
                level1Manager.GameOver(ObjectType.Laptop);
            }
            // Books/Lamp -> CompleteTask
            else if ((type == ObjectType.Book || type == ObjectType.Lamp) && collision.gameObject.CompareTag("Floor"))
            {
                SpawnDustEffect();
                StartCoroutine(FindFirstObjectByType<CameraShake>().Shake(0.2f, 0.15f));
                taskCompleted = true;
                level1Manager.CompleteTask(type);
            }
        }

        // Level 2
        if (level2Manager != null)
        {
            // ArmChair -> Game Over
            if (type == ObjectType.Armchair && collision.gameObject.CompareTag("Player"))
            {
                taskCompleted = true;
                level2Manager.GameOver();
            }
        }

        // Level 3 
        if (level3Manager != null)
        {
            // Blender/Coffee machine -> CompleteTask
            if ((type == ObjectType.Blender || type == ObjectType.CoffeeMachine) && collision.gameObject.CompareTag("Floor"))
            {
                SpawnDustEffect();
                StartCoroutine(FindFirstObjectByType<CameraShake>().Shake(0.2f, 0.15f));
                taskCompleted = true;
                level3Manager.CompleteTask(type);
            }
            // Stove -> Game Over
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

        // Level 2 - Key Task
        if (level2Manager != null)
        {
            if (type == ObjectType.Key && other.CompareTag("PillowZone"))
            {
                StartCoroutine(FindFirstObjectByType<CameraShake>().Shake(0.2f, 0.15f));
                taskCompleted = true;
                level2Manager.CompleteKeyTask();
            }
        }
    }

    // Dust effect
    private void SpawnDustEffect()
    {
        if (dustEffectPrefab != null)
        {
            Instantiate(dustEffectPrefab, transform.position + Vector3.down * 0.2f, Quaternion.identity);
            Debug.Log("Dust effect spawned at " + Time.time + " for " + name);
        }
        

    }
}
