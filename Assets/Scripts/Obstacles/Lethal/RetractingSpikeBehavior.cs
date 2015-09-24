using UnityEngine;
using System.Collections;

public class RetractingSpikeBehavior : MonoBehaviour
{
    public float RetractingSpeed;
    public float RetractDelay;
    public float EmergeDelay;
    public float RDelay;
    public float GDelay;

    public bool RetractingHorizontal;
    public bool RetractingVertical;

    private Vector3 StartLoc;

    private bool ChangeDirection = false;
    bool enabled = false;

    private float DelayEmerge;
    private float DelayRetract;
    public float InitialDelay;
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
    void FixedUpdate()
    {
        if (enabled)
        {
            if (InitialDelay <= 0.0f)
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
                InitialDelay -= Time.deltaTime;
        }
    }
    void MoveDownUp(float dt)
    {
        if (RDelay <= 0)
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
        }
        else
            RDelay -= Time.deltaTime;

        if (GDelay <= 0)
        {
            if (gameObject.transform.position.y <= StartLoc.y && ChangeDirection && DelayEmerge <= 0)
            {
                gameObject.transform.position += new Vector3(0, RetractingSpeed * dt * CurrGameSpeed, 0);
                if (gameObject.transform.position.y >= StartLoc.y)
                {
                    ChangeDirection = false;
                    DelayEmerge = EmergeDelay;
                }
            }
        }
        else
            GDelay -= Time.deltaTime;
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

    void RedTrigger(float duration)
    {
        RDelay = duration;
    }

    void GreenTrigger(float duration)
    {
        GDelay = duration;


    }

    void ToggleActive(bool isActive)
    {
        if (isActive)
            enabled = true;
        else
            enabled = false;
    }
}
