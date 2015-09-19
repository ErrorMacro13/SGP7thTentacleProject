using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float jumpForce = 350f;

    public bool isGrounded = false;
    public bool isJumping = false;
    bool isFacingLeft = false;
    bool isSlow = false;
    bool timeCharge = false;

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

        GetComponent<Rigidbody2D>().freezeRotation = true;

        if (isSlow)
            maxSpeed = 1.5f;
        else
            maxSpeed = 5.0f;

        if (timeCharge)
        {
            world.SendMessage("Refill", 0.5f);
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

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Lethal":
                Death();
                break;
            case "SlowPlayer":
                isSlow = true;
                break;
            case "ChargeTimelock":
                timeCharge = true;
                break;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":

                isGrounded = true;
                break;
            case "Lethal":
                Death();
                break;
            case "SlowPlayer":
                isSlow = true;
                break;
            case "ChargeTimelock":
                timeCharge = true;
                break;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                isGrounded = false;
                break;
            case "SlowPlayer":
                isSlow = false;
                break;
            case "ChargeTimelock":
                timeCharge = false;
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

    void OnTriggerStay2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Lethal":
                Death();
                break;
        }
    }

    void Death()
    {
        isSlow = false;
        timeCharge = false;
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
