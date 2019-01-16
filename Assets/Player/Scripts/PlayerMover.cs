using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    bool isTouchingGround = false;
    bool wantsToJump = false;
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    bool hasFallen = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        // do button presses here
        wantsToJump = Input.GetButton("Jump");
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
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

    }
}
