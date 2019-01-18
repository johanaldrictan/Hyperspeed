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

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        // do button presses here
        wantsToJump = Input.GetButton("Jump");
        animator.SetBool("Walking", isWalking);
        animator.SetBool("Grounded", isTouchingGround);
    }
    void FixedUpdate()
    {
        //act on input here
        if (isTouchingGround)
        {
            if (wantsToJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                wantsToJump = false;
            }
        }
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
    void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Ground") {
			isTouchingGround = true;           
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Ground") {
			isTouchingGround = false;
		}
	}
}
