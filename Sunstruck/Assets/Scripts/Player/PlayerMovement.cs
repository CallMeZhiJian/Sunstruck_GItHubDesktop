using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float climbSpeed;
    [SerializeField] private LayerMask groundLayer;

    private float horizontal;
    private float verticle;
    private bool isClimbing;
    private bool isLadder;

    public Rigidbody2D playerRb;
    private BoxCollider2D playerCollider;
    public Animator anima;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        playerRb.velocity = new Vector2(horizontal * speed, playerRb.velocity.y);

        anima.SetFloat("speed", Mathf.Abs(horizontal));

        if (horizontal > 0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (horizontal < 0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }

        verticle = Input.GetAxis("Vertical");

        if(isLadder && (verticle > 0f || verticle < 0f) && isGrounded())
        {
            isClimbing = true;
        }
        else if (isLadder && isGrounded())
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            playerRb.gravityScale = 0f;
            playerRb.velocity = new Vector2(playerRb.velocity.x, verticle * climbSpeed);
        }
        else
            playerRb.gravityScale = 3f;
    }

    private bool isGrounded()
    {
        RaycastHit2D hitGround = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return hitGround.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Climable"))
            isLadder = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Climable"))
        {
            isLadder = false;
            isClimbing = false;
        }   
    }
}
