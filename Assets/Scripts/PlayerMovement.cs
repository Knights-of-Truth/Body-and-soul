using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;
    float dx;
    private void Start() {
        
    }
    private void FixedUpdate() {
        dx=Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(dx * movementSpeed, rb.velocity.y);
        rb.velocity = movement;
    }
}
