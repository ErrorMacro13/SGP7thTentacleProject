using UnityEngine;
using System.Collections;

public class FallingSpikeBehavior : MonoBehaviour
{
    private bool drop = false;
    public float dropSpeed = 3;
    public float DropDistance = 2;
    public float dropDelay = 0;
    private float delayDrop = 0;
    float CurrGameSpeed = 1.0f;
    private Vector3 startLoc;
    public Sprite nonactive;
    public Sprite active;
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

    // Use this for initialization
    void Start()
    {
        startLoc = transform.position;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        delayDrop = dropDelay;
        GetComponent<SpriteRenderer>().sprite = nonactive;
    }

    // Update is called once per frame
    void Update()
    {
        if (drop)
            if (delayDrop <= 0)
                Fall(Time.deltaTime);
            else delayDrop -= Time.deltaTime;
    }
    void Fall(float dt)
    {
        if (gameObject.transform.position.y >= startLoc.y - DropDistance)
        {
            gameObject.transform.position += new Vector3(0, -dropSpeed * dt * CurrGameSpeed, 0);
            if (gameObject.transform.position.y <= startLoc.y - DropDistance)
            {
                drop = false;
            }
        }
    }
    void Activate()
    {
        GetComponent<SpriteRenderer>().sprite = active;
        drop = true;
    }
    void ResetOverWorld()
    {
        gameObject.transform.position = startLoc;
        drop = false;
        GetComponent<SpriteRenderer>().sprite = nonactive;
        delayDrop = dropDelay;
    }
}
