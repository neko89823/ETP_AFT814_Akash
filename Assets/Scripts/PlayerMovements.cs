using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    private PlayerInputAction PIA;
    private InputAction ia_Movement;
    private float speed;
    private float movementInput;

    public Rigidbody2D rbPlayer;
    public float movementSpeed = 7f;
    public float maxMovementSpeed = 10f;
    public float jumpForce = 6f;
    public Vector2 BoxSize;
    public float castDistance = 2f;
    

    private void IA_JumpStarted(InputAction.CallbackContext context)
    {
        JumpExecution();
    }

    private void Awake()
    {
        PIA = 
    }
}
