using UnityEngine;
using System.Collections;

public class ElectricFloorBehavior : MonoBehaviour
{
    float CurrGameSpeed = 1.0f;

    public bool isActive;
    public bool isCharging;
    public bool isDormant;

    public float timeBetweenStates = 3.0f;
    public float timer = 3.0f;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/ElectricFloorDormantPH", typeof(Sprite)) as Sprite;

    }

    // Update is called once per frame
    void Update()
    {


        timeBetweenStates -= (Time.deltaTime * CurrGameSpeed);
        timer = timeBetweenStates;




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
            timeBetweenStates = 3.0f;
        }





    }

    void FixedUpdate()
    {
        if (isActive)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/ElectricFloorActivePH", typeof(Sprite)) as Sprite;
            gameObject.tag = "Lethal";
        }
        else if (isCharging)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/ElectricFloorChargingPH", typeof(Sprite)) as Sprite;
            gameObject.tag = "ChargeTimelock";
        }
        else if (isDormant)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/ElectricFloorDormantPH", typeof(Sprite)) as Sprite;
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

    void ResetOverWorld() { }

}
