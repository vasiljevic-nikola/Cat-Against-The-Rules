using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Book, Lamp, Laptop }
    public ObjectType type;
    public bool isDangerous;

    // Ova funkcija se poziva kada se desi ČVRST SUDAR (npr. sa podom)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Proveravamo sudar sa igračem (za opasne objekte)
        if (isDangerous && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("GAME OVER! Collided with dangerous object: " + gameObject.name);
        }

        // Proveravamo sudar sa podom (za zadatke)
        if (!isDangerous && collision.gameObject.CompareTag("Floor"))
        {
            HandleTaskCompletion();
        }
    }

    // Ova funkcija se poziva kada objekat UĐE U TRIGGER (našu KillZone)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Proveravamo da li smo ušli u KillZone
        if (other.CompareTag("KillZone"))
        {
            // Ako je objekat zadatak (nije opasan), tretiramo ga kao ispunjenog
            if (!isDangerous)
            {
                HandleTaskCompletion();
            }
            else
            {
                // Ako je opasan objekat pao (npr. gurnut), samo ga uništi da ne pada zauvek
                Destroy(gameObject);
            }
        }
    }

    // Napravili smo posebnu funkciju da izbegnemo ponavljanje koda
    void HandleTaskCompletion()
    {
        Debug.Log("TASK COMPLETE! Object " + gameObject.name + " was cleared.");

        // Onemogućavamo objekat umesto samo collidera, da bi nestao sa scene
        gameObject.SetActive(false);
    }
}
