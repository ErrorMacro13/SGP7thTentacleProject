using UnityEngine;
using System.Collections;

public class CrushingWallBehavior : MonoBehaviour
{
    public float Speed = 3;
    public float DropDistance;

    private Vector3 StartLoc;

    private bool ChangeDirection = false;
    private bool Crush = false;

    private float CurrGameSpeed = 1.0f;
    private float DisabledDuration;
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
        ChangeDirection = false;
        Crush = false;
    }
    // Use this for initialization
    void Start()
    {
        StartLoc = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Crush && DisabledDuration <= 0)
        {
            print(DisabledDuration);
            MoveDownUp(Time.deltaTime);
        }
        else if (DisabledDuration > 0)
            DisabledDuration -= Time.deltaTime;
    }
    void MoveDownUp(float dt)
    {
        if (gameObject.transform.position.y >= StartLoc.y - DropDistance && !ChangeDirection)
        {
            gameObject.transform.position += new Vector3(0, -Speed * dt * CurrGameSpeed, 0);
            if (gameObject.transform.position.y <= StartLoc.y - DropDistance) ChangeDirection = true;
        }
        if (gameObject.transform.position.y <= StartLoc.y && ChangeDirection)
        {
            gameObject.transform.position += new Vector3(0, Speed * dt * CurrGameSpeed, 0);
            if (gameObject.transform.position.y >= StartLoc.y)
            {
                ChangeDirection = false;
                Crush = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D ent)
    {
        if (ent.tag == "Player")
        {
            print("broadcasting to player");
            ent.BroadcastMessage("Die", "Crushed");
        }
    }
    void Activate()
    {
        Crush = true;
    }

    void RedTrigger(float duration)
    {
        Activate();
    }
    void GreenTrigger(float duration)
    {
        print(duration);
        DisabledDuration = duration;
    }
}
