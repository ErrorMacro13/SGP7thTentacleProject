using UnityEngine;
using System.Collections;

public class RetractingSpikeBehavior : MonoBehaviour
{
    public float RetractingSpeed;
    public float RetractDelay;
    public float EmergeDelay;

    public bool RetractingHorizontal;
    public bool RetractingVertical;

    private Vector3 StartLoc;

    private bool ChangeDirection = false;

    private float DelayEmerge;
    private float DelayRetract;
    private float CurrGameSpeed = 1.0f;
    float DisabledDuration = 0.0f;
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
        transform.position = StartLoc;
        DelayEmerge = EmergeDelay;
        DelayRetract = RetractDelay;
    }
    // Use this for initialization
    void Start()
    {
        StartLoc = transform.position;
        DelayEmerge = EmergeDelay;
        DelayRetract = RetractDelay;
    }

    // ChangeDirectiondate is called once per frame
    void Update()
    {
        if (DisabledDuration <= 0.0f)
        {
            if (RetractingVertical)
            {
                if (ChangeDirection)
                {
                    DelayEmerge -= Time.deltaTime;
                }
                else
                {
                    DelayRetract -= Time.deltaTime;
                }
                MoveDownUp(Time.deltaTime);
            }
            else
            {
                if (ChangeDirection)
                {
                    DelayEmerge -= Time.deltaTime;
                }
                else
                {
                    DelayRetract -= Time.deltaTime;
                }
                MoveLeftRight(Time.deltaTime);
            }
        }
        else
        {
            DisabledDuration -= Time.deltaTime;
        }
    }
    void MoveDownUp(float dt)
    {
        if (gameObject.transform.position.y >= StartLoc.y - gameObject.transform.lossyScale.y && !ChangeDirection && DelayRetract <= 0)
        {
            gameObject.transform.position += new Vector3(0, -RetractingSpeed * dt * CurrGameSpeed, 0);
            if (gameObject.transform.position.y <= StartLoc.y - gameObject.transform.lossyScale.y)
            {
                ChangeDirection = true;
                DelayRetract = RetractDelay;
            }
        }
        else if (gameObject.transform.position.y <= StartLoc.y && ChangeDirection && DelayEmerge <= 0)
        {
            gameObject.transform.position += new Vector3(0, RetractingSpeed * dt * CurrGameSpeed, 0);
            if (gameObject.transform.position.y >= StartLoc.y)
            {
                ChangeDirection = false;
                DelayEmerge = EmergeDelay;
            }
        }
    }
    void MoveLeftRight(float dt)
    {
        if (gameObject.transform.position.x >= StartLoc.x - gameObject.transform.lossyScale.x && !ChangeDirection && DelayRetract <= 0)
        {
            gameObject.transform.position += new Vector3(-RetractingSpeed * dt * CurrGameSpeed, 0, 0);
            if (gameObject.transform.position.x <= StartLoc.x - gameObject.transform.lossyScale.x)
            {
                ChangeDirection = true;
                DelayRetract = RetractDelay;
            }
        }
        else if (gameObject.transform.position.x <= StartLoc.x && ChangeDirection && DelayEmerge <= 0)
        {
            gameObject.transform.position += new Vector3(RetractingSpeed * dt * CurrGameSpeed, 0, 0);
            if (gameObject.transform.position.x >= StartLoc.x)
            {
                ChangeDirection = false;
                DelayEmerge = EmergeDelay;
            }
        }
    }

    void GreenTrigger(float duration)
    {
        print(duration);
        
        DisabledDuration = duration;

       
    }
}
