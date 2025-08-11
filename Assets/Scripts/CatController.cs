using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CatController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput; // Vector2 to store the direction

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
      
        rb.MovePosition(rb.position + moveInput * movementSpeed * Time.fixedDeltaTime);
    }
}
