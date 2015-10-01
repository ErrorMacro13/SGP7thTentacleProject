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
    public Image[] Lives;
    bool isFacingLeft = false;
    bool isSlow = false;
    bool isSlippery = false;
    bool timeCharge = false;
    public float score = 0;
    public Rigidbody2D player;
    public GameObject world;
    public GameObject saver;
    public Vector3 startPosition;
    public GameObject StartCheckPoint;
    private CurrentPlayerStats CPS = new CurrentPlayerStats();
    private int life = 3;

    Animator anim;
    // Use this for initialization
    void Start()
    {
        saver = GameObject.Find("SaveDataLoader");
        CPS = saver.GetComponent<XMLScript>().LoadPlayersStats();
        score = CPS.score;
        //life = CPS.life;
        StartCheckPoint = GameObject.Find("CheckPoint" + (CPS.level));
        startPosition = StartCheckPoint.transform.position;
        transform.position = startPosition;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        for (int i = 0; i < 10; i++)
        {
            Lives[i].enabled = false;
        }
        for (int i = 0; i < life; i++)
        {
            Lives[i].enabled = true;
        }
        anim = GetComponent<Animator>();
    }
    public void SpawnPlayerAt(int CheckPointNumber = 0)
    {
        DefaultLife(3);
        StartCheckPoint = GameObject.Find("CheckPoint" + CheckPointNumber);
        startPosition = StartCheckPoint.transform.position;
        transform.position = startPosition;
    }
    public float GetScore()
    {
        return score;
    }
    public float GetCurrentLevel()
    {
        return CPS.level - 1;
    }
    public void DefaultLife(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            life++;
            Lives[i].enabled = true;
        }
    }
    public void AddLife()
    {
        life++;
        Lives[life].enabled = true;
    }
    public void LoseLife()
    {
        life--;
        Lives[life].enabled = false;
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
        else if (maxSpeed < 5.0f)
            maxSpeed += 1.0f * Time.deltaTime;
        else
            maxSpeed = 5.0f;

        if (timeCharge)
        {
            world.SendMessage("Refill", 0.5f);
        }
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("animSpeed", Mathf.Abs(move));

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
                if (speed > -maxSpeed)
                    speed -= 0.1f;
            }
            if (!isFacingLeft)
            {
                isFacingLeft = !isFacingLeft;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            player.velocity = new Vector2(speed, player.velocity.y);
        }
        else if (isSlippery)
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
                GetComponent<ParticleSystem>().Play();
                GetComponent<ParticleSystem>().startSpeed *= CurrGameSpeed;
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
    private float CurrGameSpeed = 1.0f;
    void SetTime(short GameSpeed)
    {
        switch (GameSpeed)
        {
            case 1:
                CurrGameSpeed = 0.5f;
                break;
            case 2:
                CurrGameSpeed = 0.25f;
                break;
            case 3:
                CurrGameSpeed = 0.0f;
                break;
            default:
                CurrGameSpeed = 1.0f;
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
                GetComponent<ParticleSystem>().Stop();
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

        switch (other.tag)
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
                    highscores = saver.GetComponent<XMLScript>().LoadLevel(other.GetComponent<CheckPointScript>().CheckpointNumber);
                    YS.text = world.GetComponent<GameWorldScript>().CalcScore().ToString();
                    YT.text = world.GetComponent<GameWorldScript>().GetTime().ToString();
                    HS.text = highscores.Arcade_Scores[0].score.ToString();
                    HT.text = highscores.Free_Play_Times[0].time.ToString();
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
        LoseLife();
        isSlow = false;
        timeCharge = false;
        if (life == 0)
        {
            switch (Application.loadedLevelName)
            {
                case "TutorialLevels":
                    SpawnPlayerAt(0);
                    break;
                case "AdvancedTestingLevels":
                    SpawnPlayerAt(6);
                    break;
                case "BoilerRoomLevels":
                    SpawnPlayerAt(12);
                    break;
                case "R&DLevels":
                    SpawnPlayerAt(18);
                    break;
                case "VentilationLevels":
                    SpawnPlayerAt(24);
                    break;
            }
        }
        else transform.position = startPosition;
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
