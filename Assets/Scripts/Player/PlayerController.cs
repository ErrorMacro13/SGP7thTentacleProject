using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public AudioSource slide;
    public AudioSource jump;
    public AudioSource death;

    public float maxSpeed = 5f;
    public float jumpForce = 350f;
    public bool isGrounded = false;
    public bool isJumping = false;
    public bool isSliding = false;
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
    bool isSlippery = false;
    bool timeCharge = false;
    bool resizeBCSlide = false;
    public float score = 0;
    public Rigidbody2D player;
    public BoxCollider2D playerBC;
    public GameObject world;
    public GameObject saver;
    public GameObject SM;
    public Vector3 startPosition;
    public GameObject StartCheckPoint;
    private CurrentPlayerStats CPS = new CurrentPlayerStats();
    private int life = 3;
    public int lastLevelCompleted = 0;
    Vector2 standBox;
    Vector2 slideBox;

    Animator anim;
    // Use this for initialization
    void Start()
    {
        if (!world.GetComponent<GameWorldScript>().DisableDrain) world.GetComponent<GameWorldScript>().SetEnergy(0);
        SM = GameObject.Find("SoundManager");
        saver = GameObject.Find("SaveDataLoader");
        CPS = saver.GetComponent<XMLScript>().LoadPlayersStats();
        score = CPS.score;
        lastLevelCompleted = CPS.level;
        if(CPS.life > 0) life = CPS.life;
        StartCheckPoint = GameObject.Find("CheckPoint" + (CPS.level));
        print(CPS.level.ToString());
        startPosition = StartCheckPoint.transform.position;
        transform.position = startPosition;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        anim = GetComponent<Animator>();
        playerBC = GetComponent<BoxCollider2D>();
        standBox = new Vector2(playerBC.size.x, playerBC.size.y);
        slideBox = new Vector2(playerBC.size.y, playerBC.size.x);
    }
    public void SpawnPlayerAt(int CheckPointNumber = 0)
    {
        ChangeLives(3);
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
    public int GetLives()
    {
        return life;
    }
    public void ChangeLives(int amount = 1)
    {
        if (SM.GetComponent<SoundManager>().GameState == 1)
        {
            for (int i = 0; i < amount; i++)
            {
                life++;
            }
        }
    }
    public void AddLife()
    {
        if (SM.GetComponent<SoundManager>().GameState == 1)
        {
            life++;
        }
    }
    public void LoseLife()
    {
        if (SM.GetComponent<SoundManager>().GameState == 1)
        {
            life--;
        }
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

        if (Input.GetKeyDown(KeyCode.S) && isGrounded)
        {
            anim.SetBool("isSliding", true);
            isSliding = true;
            slide.Play();
            ToggleSlide(true);
            if (isFacingLeft)
                speed = -maxSpeed;
            else
                speed = maxSpeed;

        }

        if (Input.GetKey(KeyCode.S) && isSliding)
        {
            if (speed > 0.0f)
                speed -= 0.05f;
            else if (speed < 0.0f)
                speed += 0.05f;
            
            if (speed <= 0.05f && speed >= -0.05f)
            {
                anim.SetBool("isSliding", false);
                speed = 0;
                isSliding = false;
                ToggleSlide(false);
            }
        }
        else
        {
            anim.SetBool("isSliding", false);
            isSliding = false;
            ToggleSlide(false);
        }

        GetComponent<Rigidbody2D>().freezeRotation = true;

        if (speed > -0.75f || speed < 0.75f)
        {
            anim.SetBool("isRunning", false);

        }

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

        if (Input.GetKey(KeyCode.D))
        {


            if (!isSlippery && !isSliding)
            {
                if (speed <= 0)
                    speed = 1;
                if (speed > 0 && speed < maxSpeed)
                    speed += .5f * CurrJumpPenalty;
                if (speed > maxSpeed)
                    speed = maxSpeed;
            }
            else if (isSlippery)
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

            if (!isSlippery && !isSliding)
            {
                if (speed >= 0)
                    speed = -1;
                if (speed < 0 && speed > -maxSpeed)
                    speed -= .5f * CurrJumpPenalty;
                if (speed > maxSpeed)
                    speed = -maxSpeed;
            }
            else if (isSlippery)
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

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isRunning", false);

        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("isSliding", false);
            isSliding = false;
            ToggleSlide(false);
        }

        if (isGrounded && isJumping)
        {
            Jump();
            isJumping = false;
        }

    }

    void Jump()
    {
        jump.Play();
        anim.SetBool("isJumping", true);
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
                anim.SetBool("isJumping", false);
                GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0);
                GetComponent<ParticleSystem>().Play();
                GetComponent<ParticleSystem>().startSpeed *= CurrGameSpeed;
                isSlow = true;
                break;
            case "Slippery":
                anim.SetBool("isJumping", false);
                GetComponent<ParticleSystem>().startColor = new Color(175, 238, 238);
                GetComponent<ParticleSystem>().Play();
                GetComponent<ParticleSystem>().startSpeed *= CurrGameSpeed;
                isSlippery = true;
                isGrounded = true;
                break;
            case "ChargeTimelock":
                timeCharge = true;
                break;
            case "Ground":
                anim.SetBool("isJumping", false);
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
                GetComponent<ParticleSystem>().Stop();
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

    void ToggleSlide(bool slide)
    {
        if (slide)
        {
            playerBC.size = slideBox;
            //player.transform.Rotate(0, 0, 90);
        }
        else
        {
            playerBC.size = standBox;
            //player.transform.Rotate(0, 0, -90);

        }

    }

    void Death()
    {
        death.Play();
        LoseLife();
        isSlow = false;
        timeCharge = false;
        playerBC.size = standBox;
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
        anim.SetBool("isSliding", false);
        anim.SetBool("isRunning", false);

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
