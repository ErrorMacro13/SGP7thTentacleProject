using UnityEngine;
using System.Collections;

public class MovingPlatformBehavior : MonoBehaviour
{
    public bool VerticalMovement = true;

    public float Speed = 2;
    public float StayTime = 2;
    public float MovementRange = 0;

    private bool ChangeDirection = false;
    private bool Halt = false;
    public bool enabled;

    private Vector3 StartLoc;

    private float TimeStay;
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

    void ResetOverWorld()
    {
        Halt = false;
        ChangeDirection = false;
        transform.position = StartLoc;
        TimeStay = StayTime;
    }
    // Use this for initialization
    void Start()
    {
        StartLoc = transform.position;
        TimeStay = StayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            if (VerticalMovement)
                MoveDownUp(Time.deltaTime);
            else
                MoveLeftRight(Time.deltaTime);
        }
    }
    void MoveDownUp(float dt)
    {
        if (gameObject.transform.position.y >= StartLoc.y - MovementRange && !ChangeDirection && !Halt)
        {
            gameObject.transform.position += new Vector3(0, -Speed * dt * CurrGameSpeed, 0);
            if (gameObject.transform.position.y <= StartLoc.y - MovementRange)
            {
                Halt = true;
                ChangeDirection = true;
            }
        }
        if (gameObject.transform.position.y <= StartLoc.y && ChangeDirection && !Halt)
        {
            gameObject.transform.position += new Vector3(0, Speed * dt * CurrGameSpeed, 0);
            if (gameObject.transform.position.y >= StartLoc.y)
            {
                StayTime = TimeStay;
                Halt = true;
                ChangeDirection = false;
            }
        }
        if (Halt)
        {
            TimeStay -= dt;
            if (TimeStay <= 0)
            {
                TimeStay = StayTime;
                Halt = false;
            }
        }
    }
    void MoveLeftRight(float dt)
    {
        if (gameObject.transform.position.x >= StartLoc.x - MovementRange && !ChangeDirection && !Halt)
        {
            gameObject.transform.position += new Vector3(-Speed * dt * CurrGameSpeed, 0, 0);
            if (gameObject.transform.position.x <= StartLoc.x - MovementRange)
            {
                ChangeDirection = true;
                Halt = true;
            }
        }
        else if (gameObject.transform.position.x <= StartLoc.x && ChangeDirection && !Halt)
        {
            gameObject.transform.position += new Vector3(Speed * dt * CurrGameSpeed, 0, 0);
            if (gameObject.transform.position.x >= StartLoc.x)
            {
                ChangeDirection = false;
                Halt = true;
            }
        }
        if (Halt)
        {
            TimeStay -= dt;
            if (TimeStay <= 0)
            {
                TimeStay = StayTime;
                Halt = false;
            }
        }
    }
    void ToggleActive(bool isActive)
    {
        if (isActive)
            enabled = true;
        else
            enabled = false;
    }

    void OnTriggerEnter2D(Collider2D ent)
    {
        if (ent.tag == "Player")
        {
            ent.transform.parent = transform;
        }
    }
    void OnTriggerExit2D(Collider2D ent)
    {
        if (ent.tag == "Player")
        {
            ent.transform.parent = null;
        }
    }
}
