using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isTouchingGround = false;
    private bool isWalking = false;
    private bool wantsToJump = false;
    private Rigidbody2D rb;
    private Animator animator;

    public float speed;
    public float jumpForce;
    public float fastFallSpeed;

    public float raycastDist;

    private LevelController levelController;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameObject levelControllerObject = GameObject.FindWithTag("LevelController");
        if (levelControllerObject != null)
        {
            levelController = levelControllerObject.GetComponent<LevelController>();
        }
        if (levelController == null)
        {
            Debug.Log("Cannot find 'LevelController' script");
        }
    }
    // Update is called once per frame
    void Update()
    {
        // do button presses here
        wantsToJump = Input.GetButton("Jump");
        animator.SetBool("Walking", isWalking);
        animator.SetBool("Grounded", isTouchingGround);
        Vector3 raycastDest = new Vector3(transform.position.x, transform.position.y - raycastDist, 0);
        Debug.DrawLine(transform.position, raycastDest, Color.yellow, 10);
    }
    void FixedUpdate()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.down, raycastDist, 1 << LayerMask.NameToLayer("Ground"));
       
        if(rayHit.collider != null)
        {
            isTouchingGround = true;
        }
        else
        {
            isTouchingGround = false;
        }

        //act on input here
        if (isTouchingGround)
        {
            if (wantsToJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                wantsToJump = false;
            }
        }
        //is in mid air
        else
        {
            //mid air
            //fastfall code
            if(Input.GetAxisRaw("Vertical") < 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, -fastFallSpeed);
            }
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            isWalking = true;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            levelController.AddScore();
            Destroy(collision.gameObject);
        }
    }
}
