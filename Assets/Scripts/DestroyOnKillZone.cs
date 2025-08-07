using UnityEngine;

public class DestroyOnKillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillZone"))
        {
            // Uništi objekat na kojem je OVA skripta (npr. mačka)
            Destroy(gameObject);
        }
    }
}
