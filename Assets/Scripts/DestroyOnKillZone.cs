using UnityEngine;

public class DestroyOnKillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillZone"))
        {
            // Destroy the object that this script is on (e.g. cat)
            Destroy(gameObject);
        }
    }
}
