using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;
    float dx;
    private void Start() {
        dx=Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate() {
        Vector2 movement = new Vector2(dx * movementSpeed, rb.velocity.y);
        rb.velocity = movement;
    }
}
