using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int State;
    public Animator anim;
    public LevelLoader _lv;

    [Header("Movement")]
    public float movementSpeed;
    public Rigidbody2D rb;
    float dx;
    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayers;
    bool Dashed = false;
    bool isDashing;
    public float dashDist;
    [Header("Sounds")]
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource diamondSound;
    [SerializeField] private AudioSource bounceSound;

    private bool dir = true;
    float freezeTime = 0f;
    RigidbodyConstraints2D originalConstraints;
    private void Start() {
        originalConstraints = rb.constraints;
    }
    private void Update() {

        if (!PauseMenu.GameIsPaused && Input.GetKeyDown(KeyCode.R)){
           _lv.ReloadLevel();
        }


        dx=Input.GetAxisRaw("Horizontal");
       

        if(walkSound.isPlaying==false && dx!=0 && isGrounded(feet) && (rb.velocity.x >1 || rb.velocity.x<-1) ) {
            walkSound.PlayDelayed(0.1f);
        }

        if (!PauseMenu.GameIsPaused && Input.GetButtonDown("Jump") && isGrounded(feet) && ( State == 0 || State == 2)){
            if (State == 0){
                jumpForce = 30;
            }else if (State == 2){
                jumpForce = 20;
            }
            Jump();
        }
        if (!PauseMenu.GameIsPaused && Input.GetKeyDown("k")){
            if (State == 1){
                State = 0;
            }else if (State == 0){
                State = 1;
            }
        }
        
        if (Mathf.Abs(dx) > 0.05f){
            anim.SetBool("IsRunning", true);
            
        }else{
            anim.SetBool("IsRunning", false);
        }

        if (dx > 0){
            
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            dir = true;
        }else if (dx < 0){
            transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
            dir = false;
        }


        if (!PauseMenu.GameIsPaused && Input.GetKeyDown(KeyCode.LeftShift) && ( State == 1 || State == 2)){
            if (State == 1){
                dashDist = 20;
            }else if (State == 2){dashDist = 15;}
            if (dir && !Dashed){
                StartCoroutine(Dash(1));
            }else if (!dir && !Dashed){
                StartCoroutine(Dash(-1));
            }

            
        }
        
        if (gameObject.transform.position.y < -9f){
            if(!hurtSound.isPlaying){
                hurtSound.Play();
                Destroy(gameObject,0.2f);
            }
             _lv.ReloadLevel();
            
        }
        anim.SetInteger("state", State);
        anim.SetBool("IsGrounded", isGrounded(feet));

        if(freezeTime < 0.7f){
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            freezeTime += Time.fixedDeltaTime;
        }
        else
            rb.constraints = originalConstraints;
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
    private void OnCollisionEnter2D(Collision2D other) { //TODO
        if(other.gameObject.CompareTag("Enemy")){
            if(!hurtSound.isPlaying)
                hurtSound.Play();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(gameObject, 0.2f);
            _lv.ReloadLevel();
            
        }
        // if(other.gameObject.CompareTag("Boss")){
        //     yield return new WaitForSeconds(0.5f);
        //     if(!hurtSound.isPlaying)
        //         hurtSound.Play();
        //     rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //     Destroy(gameObject, 0.2f);
        //     _lv.ReloadLevel();
            
        // }
        if(other.gameObject.CompareTag("Win")){
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _lv.LoadNextLevel();
        }
        if(other.gameObject.CompareTag("Transp")){
                diamondSound.Play();
            if (State == 0){
                State = 1;
            }else if (State == 1){
                State = 0;
            }
            freezeTime = 0f;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.CompareTag("Transp2")){
            if (State != 2){
                State = 2;
            }else{
                State = 0;
            }
        }
    
        if (other.gameObject.CompareTag("Jumper")){
                bounceSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, 30);
         }
    }

}