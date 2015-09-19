using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float jumpForce = 350f;

    public bool isGrounded = false;
    public bool isJumping = false;
    bool isFacingLeft = false;

    public Rigidbody2D player;
    public GameObject world;

    public Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            isJumping = true;
            isGrounded = false;
        }

        if (transform.rotation.z != 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (isFacingLeft)
            {
                isFacingLeft = !isFacingLeft;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            player.velocity = new Vector2(maxSpeed, player.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!isFacingLeft)
            {
                isFacingLeft = !isFacingLeft;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            player.velocity = new Vector2(maxSpeed * -1, player.velocity.y);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (isGrounded && isJumping)
        {
            Jump();
            isJumping = false;
        }
    }

    void Jump()
    {
        player.AddForce(new Vector2(0f, jumpForce));
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                isGrounded = false;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Lethal":
                Death();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Lethal":
                Death();
                break;
        }
    }

    void Death()
    {
        transform.position = startPosition;
        world.BroadcastMessage("ResetWorld");
        world.BroadcastMessage("ResetTimer");
        world.BroadcastMessage("StartTimer");
        Invoke("ResumeTimer", 1);
    }
    void Die(string DeathCase)
    {
        if (DeathCase == "Crushed" && isGrounded)
        {
            transform.position = startPosition;
            world.BroadcastMessage("ResetWorld");
            world.BroadcastMessage("ResetTimer");
            world.BroadcastMessage("StartTimer");
            Invoke("ResumeTimer", 1);
        }
    }
    void ResumeTimer()
    {
        
    }
}
