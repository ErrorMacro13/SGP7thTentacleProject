using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float jumpForce = 350f;
    public bool isGrounded = false;
    public bool isJumping = false;
    private LevelData highscores = new LevelData();
    public float speed = 0f;
    public float CurrJumpPenalty = 1f;
    public float OriginalJumpPenalty = .05f;
    bool JumpHeld = false;
    public Text YS;
    public Text YT;
    public Text HS;
    public Text HT;
    public Text YST;
    public Text YTT;
    public Text HST;
    public Text HTT;
    bool isFacingLeft = false;
    bool isSlow = false;
    bool timeCharge = false;
    public int score = 0;
    public Rigidbody2D player;
    public GameObject world;
    public GameObject saver;
    public Vector3 startPosition;
    public GameObject StartCheckPoint;
    private CurrentPlayerLevel CPL = new CurrentPlayerLevel();

    // Use this for initialization
    void Start()
    {
        saver = GameObject.Find("SaveDataLoader");
        CPL = saver.GetComponent<XMLScript>().LoadPlayersCurrentLevelAndScore();
        score = CPL.score;
        StartCheckPoint = GameObject.Find("CheckPoint" + (CPL.level));
        startPosition = StartCheckPoint.transform.position;
        transform.position = startPosition;
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }
    public int GetScore()
    {
        return score;
    }
    public int GetCurrentLevel()
    {
        return CPL.level - 1;
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
            if (speed <= 0)
                speed = 1;
            if (speed > 0 && speed < maxSpeed)
                speed += .5f * CurrJumpPenalty;
            if (speed > maxSpeed)
                speed = maxSpeed;

            if (isFacingLeft)
            {
                isFacingLeft = !isFacingLeft;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            player.velocity = new Vector2(speed, player.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (speed >= 0)
                speed = -1;
            if (speed < 0 && speed > -maxSpeed)
                speed -= .5f * CurrJumpPenalty;
            if (speed > maxSpeed)
                speed = -maxSpeed;

            if (!isFacingLeft)
            {
                isFacingLeft = !isFacingLeft;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
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
            case "Acid":
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
            case "Acid":
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
            case "Acid":
                Death();
                break;
            case "CheckPoint":
                startPosition = other.transform.position;
                SendMessageUpwards("ResetTimer");
                if (other.GetComponent<CheckPointScript>().EndOfLevelCheckPoint)
                {
                    highscores = saver.GetComponent<XMLScript>().LoadLevelData("XML\\Levels\\LevelData" + other.GetComponent<CheckPointScript>().CheckpointNumber);
                    YS.text = world.GetComponent<GameWorldScript>().CalcScore().ToString();
                    YT.text = world.GetComponent<GameWorldScript>().GetTime().ToString();
                    HS.text = highscores.ArcadeScores[0].score.ToString();
                    HT.text = highscores.FreePlayTimes[0].time.ToString();
                    YST.text = "Your Score: ";
                    YTT.text = "Your Time: ";
                    HST.text = "Best Score: ";
                    HTT.text = "Best Time: ";
                }
                else
                {
                    YST.text = "";
                    YTT.text = "";
                    HST.text = "";
                    HTT.text = "";
                    YS.text = "";
                    YT.text = "";
                    HS.text = "";
                    HT.text = "";
                }
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
