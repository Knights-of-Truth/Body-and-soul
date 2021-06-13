using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    public float movementSpeed;
    public Rigidbody2D rb;
    float dx;
    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayers;
    bool Dashed = false;
    bool isDashing;
    public float dashDist;
    public string lvlnum;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource hurtSound;
    Vector3 respawnPoint;
    public int State;

    
    private void Start() {
        
        respawnPoint = gameObject.transform.position;
    }
    private void Update() {
        dx=Input.GetAxisRaw("Horizontal");
        if(walkSound.isPlaying==false && dx!=0 && isGrounded(feet) && (rb.velocity.x >1 || rb.velocity.x<-1) ) {
            walkSound.PlayDelayed(0.1f);
        }

        if (Input.GetButtonDown("Jump") && isGrounded(feet) && ( State == 0 || State == 2)){
            if (State == 0){
                jumpForce = 30;
            }
            Jump();
            }
        
        if (Mathf.Abs(dx) > 0.05f){
            anim.SetBool("IsRunning", true);
        }else{
            anim.SetBool("IsRunning", false);
        }

        if (dx > 0){
            transform.localScale = new Vector3(2f, 2f, 1f);
        }else if (dx < 0){
            transform.localScale = new Vector3(-2f, 2f, 1f);
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && ( State == 1 || State == 2)){
            if (State == 1){
                dashDist = 20;
            }
            if (dx > 0 && !Dashed){
                StartCoroutine(Dash(1));
            }else if (!Dashed){
                StartCoroutine(Dash(-1));
            }
        }
        
        if (gameObject.transform.position.y < -9f){
            if(hurtSound.isPlaying == false)
                hurtSound.Play();
            gameObject.transform.position = respawnPoint;
            
        }
        anim.SetInteger("state", State);
        anim.SetBool("IsGrounded", isGrounded(feet));
    }
    private void FixedUpdate() {
        if (!isDashing){
            Vector2 movement = new Vector2(dx * movementSpeed , rb.velocity.y);
            rb.velocity = movement;
        }
    }

    void Jump(){
        jumpSound.Play();
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

        dashSound.Play();
        isDashing = true;
        Dashed = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDist * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
        rb.gravityScale = gravity;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            if(hurtSound.isPlaying == false)
                hurtSound.Play();
            gameObject.transform.position = respawnPoint;
        }
        if(other.gameObject.CompareTag("Win")){
            SceneManager.LoadScene("Level "+ lvlnum);
        }
        if(other.gameObject.CompareTag("Transp")){
            if (State == 0){
                State = 1;
            }else if (State == 1){
                State = 0;
            }
    }
    }
}