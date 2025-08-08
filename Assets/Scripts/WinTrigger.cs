using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Proverava da li je igrač (mačka) prošao kroz trigger
        if (other.CompareTag("Player"))
        {
            // Javi GameManager-u da je zadatak ispunjen
            FindFirstObjectByType<GameManager_Level2>().PlayerWentThroughDoor();
            // Ugasi trigger da se ne aktivira više puta
            gameObject.SetActive(false);
        }
    }
}

