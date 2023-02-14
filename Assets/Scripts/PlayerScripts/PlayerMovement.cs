using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement variables")]
    public float movementSpeed;
    private float moveDirection;

    [Header("Jump Variables")]
    public float jumpForce;
    public LayerMask groundLayer;
    public Transform groundCheck;
    [SerializeField] private bool grounded;

    [Header("Climb Ability variables")]
    [SerializeField] bool canClimb;
    [SerializeField] bool isClimbing;
    private float climbDirection;
    public float climbSpeed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement and Jump
        //player input
        moveDirection = Input.GetAxisRaw("Horizontal");

        //change scale to make player look left or right
        if(moveDirection < 0){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(moveDirection > 0){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //move the player 
        rb.velocity = new Vector2(moveDirection * movementSpeed, rb.velocity.y);

        //Check if player is grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
        }
        #endregion

        #region Climb
        climbDirection = Input.GetAxisRaw("Vertical");
        if(canClimb && climbDirection > 0f)
        {
            isClimbing = true;
        }

        if(isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, climbDirection * climbSpeed);
        }
        if(!isClimbing)
        {
            rb.gravityScale = 5f;
        }
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Rope"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Rope"))
        {
            canClimb = false;
            isClimbing = false;
        }
    }
}
