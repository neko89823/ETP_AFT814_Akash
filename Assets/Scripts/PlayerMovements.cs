using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    private PlayerInputAction PIA;
    private InputAction ia_Movement;
    private float speed;
    private float movementInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastPosition;

    public Rigidbody2D rbPlayer;
    public float movementSpeed = 7f;
    public float maxMovementSpeed = 10f;
    public float jumpForce = 6f;
    public Vector2 BoxSize;
    public float castDistance = 2f;
    public float interactionDistance = 2f;
    

    private void IA_JumpStarted(InputAction.CallbackContext context)
    {
        JumpExecution();
    }

    private void IA_InteractionStarted(InputAction.CallbackContext context)
    {
        InteractionExecution();
    }

    private void Start()
    {
        lastPosition = transform.position;
    }
    private void Awake()
    {
        PIA = new PlayerInputAction();
        ia_Movement = PIA.Movements.Walking;
        PIA.Movements.Enable();

        PIA.Movements.Jump.started += IA_JumpStarted;
        PIA.Movements.Jump.Enable();

        PIA.Movements.Interaction.started += IA_InteractionStarted;
        PIA.Movements.Interaction.Enable();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movementInput = ia_Movement.ReadValue<float>();

        FlipSpriteHorizontallyBasedOnInput();

        CalculateSpeed();

        animator.SetFloat("Speed", speed);
        animator.SetBool("IsGrounded", IsTouchingGround());
    }

    private void FixedUpdate()
    {
        if (speed < maxMovementSpeed)
        {
            rbPlayer.AddForce(new Vector2(movementInput * movementSpeed, 0));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, BoxSize);
    }

    public bool IsTouchingGround()
    {
        int mask = LayerMask.GetMask("Ground");

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, BoxSize, 0, -transform.up, castDistance, mask);

        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void JumpExecution()
    {
        if (IsTouchingGround() == false)
        {
            //null;
        }
        else if (IsTouchingGround() == true)
        {
            rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("Jump Executed");
        }
    }

    private void FlipSpriteHorizontallyBasedOnInput()
    {
        if (movementInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void CalculateSpeed()
    {
        float distance = Vector3.Distance(lastPosition, transform.position);
        speed = distance / Time.deltaTime;
        lastPosition = transform.position;
    }

    private void InteractionExecution()
    {
        int mask = LayerMask.GetMask("Interaction");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, interactionDistance, mask);
        Debug.DrawLine(transform.position, transform.position + transform.right * interactionDistance, Color.red, 1f);

        if (hit.collider != null)
        {
            Debug.Log("you have hit an object by name " + hit.transform.name);
            if (hit.transform.GetComponent<InteractableItem>() != null)
            {
                hit.transform.GetComponent<InteractableItem>().InitiateInteraction();
            }
            else
            {
                Debug.Log("You have hit an non interactable item");
            }
        }
        else
        {
            Debug.Log("You hit nothing");
        }
    }
}
