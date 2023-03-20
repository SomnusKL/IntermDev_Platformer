using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;
using UnityEngine.SceneManagement;

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
    private bool isOnLadder;

    private bool canDoubleJump = false;
    //Animation
    private Animator playerAnimation;
    
    //reset player location
    private Vector3 respawnPoint;
    public GameObject Detector;
    
    //screenshake
    public bool screenshake = false;
    
    public Transform ladderCheck;
    public LayerMask ladderLayer;
    public float ladderCheckRadius = 5f;
    
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
        isOnLadder = Physics2D.OverlapCircle(ladderCheck.position, groundCheckRadius, ladderLayer);

        direction = Input.GetAxis("Horizontal");
        
        if (isOnLadder)
        {
            float verticalDirection = Input.GetAxis("Vertical");

            if (verticalDirection != 0)
            {
                player.velocity = new Vector2(player.velocity.x, verticalDirection * speed);
            }
            else
            {
                player.velocity = new Vector2(player.velocity.x, 0);
            }
        }
        else
        {
            direction = Input.GetAxis("Horizontal");
        }
        
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
        if (Input.GetButtonDown("Jump"))
        {
            if (isTouchingGround)
            {
                player.velocity = new Vector2(player.velocity.x, jumpSpeed);
                canDoubleJump = true; // set canDoubleJump to true after the first jump
                Debug.Log("jump");
            }
            else if (canDoubleJump) // check if the player can double jump
            {
                player.velocity = new Vector2(player.velocity.x, jumpSpeed);
                canDoubleJump = false; // reset the double jump ability
                Debug.Log("double jump");
            }
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
        else if (collision.CompareTag("NextLevel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
        else if (collision.CompareTag("PreviousLevel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        }
        else if (collision.CompareTag("End"))
        {
            SceneManager.LoadScene("End");
        }
        else if (collision.CompareTag("Ob")){
            SceneManager.LoadScene("Lose");
        }
    }
}
