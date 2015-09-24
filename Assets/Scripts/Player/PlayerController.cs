using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float jumpForce = 350f;

    public bool isGrounded = false;
    public bool isJumping = false;

    public float speed = 0f;
    public float CurrJumpPenalty = 1f;
    public float OriginalJumpPenalty = .05f;
    bool JumpHeld = false;

    bool isFacingLeft = false;
    bool isSlow = false;
    bool isSlippery = false;
    bool timeCharge = false;

    public Rigidbody2D player;
    public GameObject world;

    public Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        GetComponent<Rigidbody2D>().freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
            CurrJumpPenalty = 1.0f;
        else
            CurrJumpPenalty = OriginalJumpPenalty;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            JumpHeld = true;
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
            if (!isSlippery)
            {
                if (speed <= 0)
                    speed = 1;
                if (speed > 0 && speed < maxSpeed)
                    speed += .5f * CurrJumpPenalty;
                if (speed > maxSpeed)
                    speed = maxSpeed;
            }
            else
            {
                if (speed < maxSpeed)
                    speed += 0.1f;
            }
            if (isFacingLeft)
            {
                isFacingLeft = !isFacingLeft;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            player.velocity = new Vector2(speed, player.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!isSlippery)
            {
                if (speed >= 0)
                    speed = -1;
                if (speed < 0 && speed > -maxSpeed)
                    speed -= .5f * CurrJumpPenalty;
                if (speed > maxSpeed)
                    speed = -maxSpeed;
            }
            else
            {
                if(speed > -maxSpeed)
                    speed -= 0.1f;
            }
            if (!isFacingLeft)
            {
                isFacingLeft = !isFacingLeft;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            player.velocity = new Vector2(speed, player.velocity.y);
        }
        else if(isSlippery)
        {
            if (speed > 0.0f)
                speed -= 0.05f;
            else if (speed < 0.0f)
                speed += 0.05f;
            player.velocity = new Vector2(speed, player.velocity.y);
        }
        else
        {
            speed = 0.0f;
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
            case "Slippery":
                isSlippery = true;
                isGrounded = true;
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
            case "Slippery":
                isSlippery = true;
                isGrounded = true;
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
            case "Slippery":
                isSlippery = false;
                isGrounded = false;
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
            case "CheckPoint":
                startPosition = other.transform.position;
                SendMessageUpwards("ResetTimer");
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "CheckPoint":
                SendMessageUpwards("StartTimer");
                break;
        }
    }

    void Death()
    {
        isSlow = false;
        timeCharge = false;
        transform.position = startPosition;
        world.BroadcastMessage("SetEnergy", 0.0f);
        world.BroadcastMessage("ZeroTimer");
        world.BroadcastMessage("ResetWorld");
    }

    void Die(string DeathCase)
    {
        if (DeathCase == "Crushed" && isGrounded)
        {
            Death();
        }
    }

    void ResumeTimer()
    {

    }
}
