using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    private void Start()
    {
        //simply a console message
        Debug.Log("It do be working doe 0-0");

        // this gets the player's rigid body once when the game starts we can simply
        // just use the variable "rigid". it is much less taxing on the computer
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // float allows for joystick support. these functions use the unity gamepad shit,
        // which explains why we use "Jump" and "Horizontal" instead of "space" and shit
        // rigid.velocity.y means that when you input another direction, your character
        // will continue where they were originally going. TLDR, no jerky movements
        float dirX = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(dirX * 11f, rigid.velocity.y);

        // basically if the user hits space, the player jumps. Note: Vector2(x, y)
        // since jump only changes the player's y position, it is the only nonzero variable.
        if(Input.GetButtonDown("Jump"))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 21f);
        }
    }
}
