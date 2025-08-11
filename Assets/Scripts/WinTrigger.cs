using UnityEngine;
using System.Reflection;

public class WinTrigger : MonoBehaviour
{
    private MonoBehaviour gameManager;
    private MethodInfo winMethod;

    private void Start()
    {
        // Pronađi baš GameManager skriptu u sceni (može biti Level1, Level2, Level3...)
        gameManager = FindFirstObjectByType<GameManager>(FindObjectsInactive.Include) as MonoBehaviour ??
                      FindFirstObjectByType<GameManager_Level2>(FindObjectsInactive.Include) as MonoBehaviour ??
                      FindFirstObjectByType<GameManager_Level3>(FindObjectsInactive.Include) as MonoBehaviour;

        if (gameManager != null)
        {
            // Traži LevelComplete ili PlayerWentThroughDoor
            winMethod = gameManager.GetType().GetMethod("LevelComplete") ??
                        gameManager.GetType().GetMethod("PlayerWentThroughDoor");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameManager != null && winMethod != null)
        {
            Debug.Log("Player triggered WinTrigger on " + name);
            winMethod.Invoke(gameManager, null);
        }
    }
}
