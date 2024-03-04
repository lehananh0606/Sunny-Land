using UnityEngine;

public class Playermovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField]private CircleCollider2D groundCheck; // Tham chiếu đến CircleCollider2D của GroundCheck
    private float dirX = 0f;
    private bool isGrounded = false;
    private bool canDoubleJump = true; 
    private int jumpCount = 0; // Biến để đếm số lần nhảy đã thực hiện

    [SerializeField] private float moveSpeed = 14f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float doubleJumpForce = 20f;

    private enum MovementState { idle, running, jumping, falling }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        // Lấy tham chiếu đến CircleCollider2D của GroundCheck
        groundCheck = GameObject.Find("GroundCheck").GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // Kiểm tra nếu người chơi đang chạm vào "GroundCheck"
        if (IsGround())
        {
            isGrounded = true;
            jumpCount = 0; // Reset jumpCount khi đang ở trạng thái nằm trên mặt đất
            canDoubleJump = true; // Reset double jump khi đang ở trạng thái nằm trên mặt đất
        }

        // Nhảy
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isGrounded = false;
                jumpCount++;
            }
            else if (canDoubleJump && jumpCount == 1 && rb.velocity.y < 0.1f && rb.velocity.y > -0.1f) // Chỉ cho phép double jump khi người chơi đang không ở trạng thái nhảy hoặc rơi
            {
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
                canDoubleJump = false;
                jumpCount++;
            }
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsGround()
    {
        // Kiểm tra xem CircleCollider2D của GroundCheck có chạm vào đất không
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundCheck.radius, jumpableGround);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject) // Đảm bảo rằng collider không thuộc về nhân vật
            {
                return true;
            }
        }
        return false;
    }
}
