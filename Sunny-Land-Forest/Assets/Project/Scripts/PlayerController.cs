using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator playerAnimator;
    private Rigidbody2D playerRigidBody;

    public Transform groundCheck;
    public bool isGround = false;

    public float speed;

    public float touchRun = 0.0f;

    public bool facingRight = true;

    //Pulo
    public bool jump;
    public int numberJumps = 0;
    public int maxJumps = 2;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        playerAnimator.SetBool("IsGrounded", isGround);


       touchRun = Input.GetAxisRaw("Horizontal");
        Debug.Log(touchRun.ToString());

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        SetaAnimator();
    }

    private void FixedUpdate()
    {
        MovePlayer(touchRun);

        if (jump)
        {
            JumpPlayer();
        }
    }

    void MovePlayer(float movimentoH)
    {
        playerRigidBody.velocity = new Vector2(movimentoH * speed, playerRigidBody.velocity.y);
        if (movimentoH < 0 && facingRight || movimentoH > 0 && !facingRight)
        {
            Flip();
        }
    
    }

    void Flip()
    {
        facingRight = !facingRight;
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = new Vector3(theScale.x, transform.localScale.y, transform.localScale.z);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void JumpPlayer()
    {
        if (isGround)
        {
            numberJumps = 0;
        }

            if (isGround || numberJumps < maxJumps)
        {
            playerRigidBody.AddForce(new Vector2(0f, jumpForce));
            isGround = false;
            numberJumps++;
        }

        jump = false;
    }

    void SetaAnimator()
    {
        playerAnimator.SetBool("Walk", playerRigidBody.velocity.x != 0 && isGround);
        playerAnimator.SetBool("Jump", !isGround);
    }
}

