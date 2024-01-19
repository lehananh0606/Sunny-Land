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
    [SerializeField] int totalJumps;
    int availableJumps;
    float HorizontalValue;
    float crouchSpeedModifier = 0.5f;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform overheadCheckCollider;
    [SerializeField] bool isGrounded;
    bool facingRight = true;
    bool CrouchPressed;
    Animator animator;
    bool coyoteJump;

    const float groundCheckRadius = 0.2f;
    const float overheadCheckColliderRadius = 0.2f;
    bool mutipleJumps;

    void Awake()
    {
        availableJumps = totalJumps;
        rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
    }

    void GroundCheck(){

        bool wasGrounded = isGrounded;
        isGrounded  =false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(colliders.Length > 0){
            isGrounded = true;
            if(!wasGrounded){
                availableJumps=  totalJumps;
                mutipleJumps = false;
            }
        }else{
           if(wasGrounded){
             StartCoroutine(CoyoteJumpDelay());
           }
        }
            

            animator.SetBool("Jump", !isGrounded);
        
    }


    IEnumerator CoyoteJumpDelay(){
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        HorizontalValue = Input.GetAxisRaw("Horizontal");
       
       if(Input.GetButtonDown("Jump")){

           
            Jump();
            
       }
      
   
      

       if(Input.GetButtonDown("Crouch"))
        CrouchPressed = true;
       else if(Input.GetButtonUp("Crouch"))
       CrouchPressed = false;
       //Set the yVelocity 
       animator.SetFloat("yVelocity", rb.velocity.y); 
    }

    void FixedUpdate(){
        GroundCheck();
        Move(HorizontalValue, CrouchPressed);
    }

void Jump(){
 if(isGrounded){

        mutipleJumps = true;
        availableJumps--;
        rb.velocity = Vector2.up * jumpPower;
        animator.SetBool("Jump", true);
    }else{

        if(coyoteJump){
            mutipleJumps = true;
            availableJumps--;
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
        if(mutipleJumps && availableJumps>0){
             availableJumps--;
            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
    }
 
}

void Move(float dir, bool crouchFlag){
    if(!crouchFlag){
        if(Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckColliderRadius, groundLayer)){
            crouchFlag = true;
        }
        standingCollider.enabled = !crouchFlag;
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

    private void OnDrawGizmos(){

    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
    }

   
}
