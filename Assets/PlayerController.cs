using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

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

    //Animation
    private Animator playerAnimation;
    
    //reset player location
    private Vector3 respawnPoint;
    public GameObject Detector;
    
    //screenshake
    public bool screenshake = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
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
            transform.localScale = new Vector2(0.25f,0.25f);
        }
        //left direction
        else if (direction < 0f)
        {
            transform.localScale = new Vector2(-0.25f,0.25f);
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
        
        //talk to the animator
        playerAnimation.SetFloat("Speed",Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);
        
        //detector follow player
        Detector.transform.position = new Vector2(transform.position.x, Detector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fall"))
        {
            transform.position = respawnPoint;
            screenshake = true;
        }
    }
}
