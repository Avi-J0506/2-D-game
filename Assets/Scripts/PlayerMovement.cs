using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;

    public float playerSpeed = 2;
    public float jumpForce = 2;
    public float raycastLength = 2;
    public Transform respawnPoint;

    public bool isGrounded;
    public LayerMask groundLayerMask;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, y: jumpForce);
        }

        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        isGrounded = Physics2D.Raycast(origin: transform.position, direction: Vector2.down, distance: raycastLength, groundLayerMask);
        Debug.DrawRay(transform.position, dir: Vector3.down * raycastLength, Color.green);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
        if(other.tag == "Respawn")
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = respawnPoint.position;
    }
    
}
