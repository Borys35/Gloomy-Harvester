using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool CanMove = true;
    public static bool Running = false;

    public GameObject playerSprite;
    public GameObject groundCheck;
    public LayerMask whatIsGround;
    public float walkSpeed = 4;
    public float runSpeed = 6;
    public float rotateAmount = 15;
    public float jumpVelocity = 10;

    Rigidbody2D rb;
    Animator anim;
    float movement;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        movement = CanMove ? Input.GetAxis("Horizontal") : 0;
        playerSprite.transform.eulerAngles = new Vector3(0, 0, rotateAmount * -movement);

        if (CanMove && IsGrounded() && Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            anim.SetTrigger("Jump");
        }
    }

    void FixedUpdate() {
        speed = Running ? runSpeed : walkSpeed;
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);

        if (rb.velocity.x > 0)
            transform.localScale = new Vector3(1, 1, 1);

        else if (rb.velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    
    bool IsGrounded() {
        RaycastHit2D raycast = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, 0.5f, whatIsGround);

        if (raycast.collider == null) return false;
        return raycast.collider.tag == "Ground";
    }
}
