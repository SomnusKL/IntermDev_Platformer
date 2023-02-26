using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //basic
    public float speed = 5f;
    public float jumpSpeed = 8f;
    
    //ground check
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    
    private float direction = 0f;
    private Rigidbody2D player;
    private bool isTouchingGround;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        //check if the drawn circle is overlapped with the groundLayer
        direction = Input.GetAxis("Horizontal");
        //right direction
        if (direction > 0f)
        {
            player.velocity = new Vector2(direction*speed, player.velocity.y);
        }
        //left direction
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction*speed, player.velocity.y);
        }
        //still
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }
        //jump
        if (Input.GetButton("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }
    }
}