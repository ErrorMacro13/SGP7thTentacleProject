using UnityEngine;
using System.Collections;

public class FallingSpikeBehavior : MonoBehaviour
{
    private bool drop = false;
    public bool UseRigidPhysics = false;
    public float dropSpeed = 3;
    public float DropDistance = 2;
    public float dropDelay = 0;
    private float delayDrop = 0;
    float CurrGameSpeed = 1.0f;
    private Transform trans;
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
    void ResetOverWorld()
    {
        gameObject.transform.position = trans.position;
        drop = false;
        GetComponent<SpriteRenderer>().sprite = nonactive;

    }
    // Use this for initialization
    void Start()
    {
        startLoc = transform.position;
        trans = gameObject.transform;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        delayDrop = dropDelay;
        GetComponent<SpriteRenderer>().sprite = nonactive;
    }

    // Update is called once per frame
    void Update()
    {
        if (UseRigidPhysics)
        {
            if (GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0) && drop)
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }
        if (drop)
            if (delayDrop <= 0)
                Fall(Time.deltaTime);
            else delayDrop -= Time.deltaTime;
    }
    void Fall(float dt)
    {
        if (UseRigidPhysics)
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            if (CurrGameSpeed == 0)
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else if (CurrGameSpeed == 1)
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
            else
                gameObject.GetComponent<Rigidbody2D>().gravityScale *= CurrGameSpeed;
        }
        else
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
    }
    void Activate()
    {
        GetComponent<SpriteRenderer>().sprite = active;
        drop = true;
    }
    
}
