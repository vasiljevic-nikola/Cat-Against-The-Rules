using UnityEngine;
using UnityEngine.InputSystem; // Veoma važno: dodajemo novi Input System

[RequireComponent(typeof(Rigidbody2D))]
public class CatController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput; // Sada je ovo Vector2 za čuvanje pravca

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Ova funkcija se automatski poziva od strane Player Input komponente
    // svaki put kada se detektuje pokret (npr. pritisak na A/D/W/S).
    public void OnMove(InputAction.CallbackContext context)
    {
        // Čitamo vrednost unosa kao Vector2.
        // Za A/D, X će biti -1 ili 1, a Y će biti 0.
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Ostatak koda je isti kao pre!
        // Pomeramo Rigidbody na osnovu pročitanog unosa.
        rb.MovePosition(rb.position + moveInput * movementSpeed * Time.fixedDeltaTime);
    }
}
