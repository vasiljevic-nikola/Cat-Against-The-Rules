using UnityEngine;
using System.Reflection;

public class WinTrigger : MonoBehaviour
{
    private MonoBehaviour gameManager;
    private MethodInfo winMethod;

    private void Start()
    {
        // Nađi bilo koji GameManager skriptu u sceni (Level1, Level2, Level3...)
        gameManager = FindFirstObjectByType<MonoBehaviour>(FindObjectsInactive.Include);

        if (gameManager != null)
        {
            // Traži LevelComplete ili PlayerWentThroughDoor
            winMethod = gameManager.GetType().GetMethod("LevelComplete") ??
                        gameManager.GetType().GetMethod("PlayerWentThroughDoor");

            if (winMethod == null)
            {
                gameManager = null; // nema ni jednu od metoda
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gameManager != null && winMethod != null)
        {
            winMethod.Invoke(gameManager, null);
        }
    }
}
