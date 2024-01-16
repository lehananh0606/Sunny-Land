using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dichuyen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask groundLayer;
    public float speed = 1;
    [SerializeField] float jumpPower = 500;
    [SerializeField] Collider2D standingCollider;
    private Rigidbody2D rb;
    float HorizontalValue;
    float crouchSpeedModifier = 0.5f;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform overheadCheckCollider;
     bool isGrounded;
    bool facingRight = true;
    bool CrouchPressed;
    Animator animator;
    bool jump;
    const float groundCheckRadius = 0.2f;
    const float overheadCheckColliderRadius = 0.2f;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
    }

    void GroundCheck(){
        isGrounded  =false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(colliders.Length > 0)
            isGrounded = true;

            animator.SetBool("Jump", !isGrounded);
        
    }
    // Update is called once per frame
    void Update()
    {
        
        HorizontalValue = Input.GetAxisRaw("Horizontal");
       
       if(Input.GetButtonDown("Jump")){

           
            jump = true;
             animator.SetBool("Jump", true);
       }
      
       else if(Input.GetButtonUp("Jump"))
       jump = false;

       if(Input.GetButtonDown("Crouch"))
        CrouchPressed = true;
       else if(Input.GetButtonUp("Crouch"))
       CrouchPressed = false;
       //Set the yVelocity 
       animator.SetFloat("yVelocity", rb.velocity.y); 
    }

    void FixedUpdate(){
        GroundCheck();
        Move(HorizontalValue, jump, CrouchPressed);
    }

void Move(float dir, bool jumpFlag, bool crouchFlag){
    if(!crouchFlag){
        if(Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckColliderRadius, groundLayer)){
            crouchFlag = true;
        }
    }


    if(isGrounded){
        
        standingCollider.enabled = !crouchFlag;

        if(jumpFlag)
        { 
           // isGrounded = false ;
            jumpFlag = false;

        rb.AddForce(new Vector2(0f, jumpPower));
    }
    }

    float xVal = dir * speed * 100 * Time.deltaTime;
    Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
    rb.velocity = targetVelocity;

    if(crouchFlag){
        xVal *= crouchSpeedModifier;
    }
   
    //flipping 
    if(facingRight && dir < 0){
        transform.localScale = new Vector3(-1, 1, 1);
        facingRight = false;
    }
    else if (!facingRight && dir > 0){
        transform.localScale = new Vector3(1, 1, 1);
        facingRight = true;
    }
    animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));

    

    

}

   
}
