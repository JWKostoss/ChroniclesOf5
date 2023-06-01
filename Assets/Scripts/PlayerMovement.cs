using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigid;
    private BoxCollider2D coll;
    private float dirX;

    //Ground Layer in Unity and wallCheck child game object for checking if player is colliding with wall
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;

    //Checks if sprite is facing right for sprite flip

    private bool isWallSliding;
    private float wallSlidingSpeed = 10f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(18f, 22f);

    // Start is called before the first frame update
    void Start()
    {
        //simply a console message
        Debug.Log("It do be working doe 0-0");

        // this gets the player's rigid body once when the game starts we can simply
        // just use the variable "rigid". it is much less taxing on the computer
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // float allows for joystick support. these functions use the unity gamepad shit,
        // which explains why we use "Jump" and "Horizontal" instead of "space" and shit
        // rigid.velocity.y means that when you input another direction, your character
        // will continue where they were originally going. TLDR, no jerky movements
        dirX = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(dirX * 11f, rigid.velocity.y);

        // basically if the user hits space, the player jumps. Note: Vector2(x, y)
        // since jump only changes the player's y position, it is the only nonzero variable.
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 21f);
        }

        Vector3 playerScale = transform.localScale;
        if (dirX < 0f)
        {
            playerScale.x = -1;
        }
        
        if (dirX > 0f)
        {
            playerScale.x = 1;
        }
        transform.localScale = playerScale;

        wallSlide();
        wallJump();
    }


    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);
    }
    private void wallSlide()
    {
        //Performs wall slide after checking if player is against a wall and not grounded
        //Sets wall sliding boolean to true to fulfill wall jump requirements, and slows down the player by the wallSlidingSpeed float set previously.
        if(isWalled() && !isGrounded())
        {
            isWallSliding = true;
            Debug.Log("Epic wall slide!");
            rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Clamp(rigid.velocity.y, -wallSlidingSpeed, float.MaxValue));
        } 
        else
        {
            isWallSliding = false;
        }
    }
    private void wallJump()
    {
        if(isWallSliding)
        {
        isWallJumping = false;
        wallJumpingDirection = transform.localScale.x;
        wallJumpingCounter = wallJumpingTime;

        CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rigid.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }
}
