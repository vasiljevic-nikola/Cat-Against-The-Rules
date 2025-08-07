using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Definišemo tipove objekata koje imamo.
    // Ovo će nam se pojaviti kao padajući meni u Unity-ju.
    public enum ObjectType
    {
        Book,
        Lamp,
        Laptop
    }

    // Ovde biramo koji je ovo tip objekta.
    public ObjectType type;

    // Ovde štikliramo da li je objekat opasan (Game Over ako ga dodirnemo).
    public bool isDangerous;

}

