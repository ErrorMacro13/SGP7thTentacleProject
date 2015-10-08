using UnityEngine;
using System.Collections;

public class ElectricFloorBehavior : MonoBehaviour
{
    float CurrGameSpeed = 1.0f;
    
    public bool isActive;
    public bool isCharging;
    public bool isDormant;

    bool ia;
    bool ic;
    bool id;

    public float timeBetweenStates = 3.0f;
    float timer;
    float animSpeed = 1.0f;

    GameObject player;
    Animator anim;
    void ResetOverWorld()
    {
        anim.SetBool("isActive", false);
        anim.SetBool("isDormant", false);
        anim.SetBool("isCharging", false);
        isDormant = id;
        isCharging = ic;
        isActive = ia;
        timer = timeBetweenStates;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/ElectricFloorDormantPH", typeof(Sprite)) as Sprite;
    }
    // Use this for initialization
    void Start()
    {
        ia = isActive;
        ic = isCharging;
        id = isDormant;
        timer = timeBetweenStates;
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/ElectricFloorDormantPH", typeof(Sprite)) as Sprite;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        timer -= (Time.deltaTime * CurrGameSpeed);

        if (timer <= 0.0f)
        {
            if (isDormant)
            {
                isDormant = false;
                isCharging = true;
            }
            else if (isCharging)
            {
                isCharging = false;
                isActive = true;
            }
            else if (isActive)
            {
                isActive = false;
                isDormant = true;
            }
            timer = timeBetweenStates;
        }


        anim.speed = animSpeed * CurrGameSpeed;


    }

    void FixedUpdate()
    {
        if (isActive)
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/ElectricFloorActivePH", typeof(Sprite)) as Sprite;
            anim.SetBool("isCharging", false);
            anim.SetBool("isActive", true);
            gameObject.tag = "Lethal";
        }
        else if (isCharging)
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/ElectricFloorChargingPH", typeof(Sprite)) as Sprite;
            anim.SetBool("isDormant", false);
            anim.SetBool("isCharging", true);
        }
        else if (isDormant)
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/ElectricFloorDormantPH", typeof(Sprite)) as Sprite;
            anim.SetBool("isActive", false);
            anim.SetBool("isDormant", true);
            gameObject.tag = "Ground";
        }
    }

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
        if(isCharging && other.gameObject.tag == "Player")
        {
            SendMessageUpwards("Refill", 0.2);
        }
    }
}
