using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public Animator anim;
    public float movementSpeed;
    public Rigidbody2D rb;
    float dx;
    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayers;
    bool Dashed = false;
    bool isDashing;
    public int state;
    public float dashDist;
    private void Start() {
    
    }
    private void Update() {
        dx=Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded(feet) && ( state == 0 || state == 2)){
            Jump();
            }
        
        if (Mathf.Abs(dx) > 0.05){
            //anim.SetBool("IsRunning", true);
        }else{
            //anim.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && ( state == 1 || state == 2)){
            if (dx > 0 && !Dashed){
                StartCoroutine(Dash(1));
            }else if (!Dashed){
                StartCoroutine(Dash(-1));
            }
        }
    }
    private void FixedUpdate() {
<<<<<<< Updated upstream
        dx=Input.GetAxisRaw("Horizontal");
=======
        if (!isDashing){
>>>>>>> Stashed changes
        Vector2 movement = new Vector2(dx * movementSpeed, rb.velocity.y);
        rb.velocity = movement;
        }
    }
    void Jump(){
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        rb.velocity = movement;
    }
    bool isGrounded(Transform temp){
         Collider2D groundCheck = Physics2D.OverlapCircle(temp.position, 0.5f, groundLayers);
         if (groundCheck != null){
            Dashed = false;
            return true;
         }
        return false;
    }

    IEnumerator Dash (float direction){
        isDashing = true;
        Dashed = true;
        //anim.SetBool("IsDashing", isDashing);
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDist * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        //anim.SetBool("IsDashing", isDashing);
        rb.gravityScale = gravity;
    }
}
