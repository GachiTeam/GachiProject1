using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {

    public float speed = 2;

    private bool grounded=false;
    public Transform groundCheck;
    private float groundRadius=0.2f;
    public LayerMask whatIsGround;
    private bool facingRight = true;
    private Rigidbody2D rb;
    private Animator animator;
    public float jumpForce = 700f;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) == true)
        {
            Debug.Log("space");
            animator.SetTrigger("playerAttack");
        }

        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Ground", false);
            rb.AddForce(new Vector2(0, jumpForce));
        }
	}
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        animator.SetBool("Ground", grounded);

        animator.SetFloat("vSpeed", rb.velocity.y);

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");


        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        if (!rb)
        {
            Debug.Log("naspa");
        }
        else
        {
            //Debug.Log("ok");
            rb.AddForce(movement * speed);
        }

        if(moveHorizontal  >0 && !facingRight)
        {
            Flip();
        }
        else if(moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
