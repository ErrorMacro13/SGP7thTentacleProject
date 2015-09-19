using UnityEngine;
using System.Collections;

public class CollapsingFloorBehavior : MonoBehaviour
{

    float CurrGameSpeed = 1.0f;

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

    bool active = false;
    public float TimeBeforeFall = 1.0f;
    public float TimeBeforeBreak = 1.0f;

    float TBBOriginal;
    float TBFOriginal;
    Sprite SpriteOrignial;

    public Sprite Crumble;

    Vector3 Originalpos;
    Vector3 OriginalScl;
    Quaternion OriginalRot;

    // Use this for initialization
    void Start()
    {
        Originalpos = transform.position;
        OriginalRot = transform.rotation;
        OriginalScl = transform.lossyScale;
        TBBOriginal = TimeBeforeBreak;
        TBFOriginal = TimeBeforeFall;
        SpriteOrignial = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (active && TimeBeforeFall > 0.0f)
        {
            TimeBeforeFall -= Time.deltaTime * CurrGameSpeed;
        }
        if (TimeBeforeFall <= 0.0f && CurrGameSpeed != 0.0f)
        {
            TimeBeforeBreak -= Time.deltaTime * CurrGameSpeed;
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
        if (TimeBeforeBreak <= 0.0f && active)
        {
            Shrink();
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().gravityScale = CurrGameSpeed;
        if (CurrGameSpeed == 0.0f)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

        if (GetComponent<Rigidbody2D>().velocity == new Vector2(0, 0))
            print("Stopped");
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && CurrGameSpeed != 0.0f)
        {
            active = true;
            GetComponent<SpriteRenderer>().sprite = Crumble;
        }
    }

    void Shrink()
    {
        print("shrink");
        GetComponent<Transform>().localScale -= GetComponent<Transform>().localScale;
    }

    void ResetOverWorld()
    {
        transform.position = Originalpos;
        transform.rotation = OriginalRot;
        transform.localScale = OriginalScl;
        GetComponent<Rigidbody2D>().isKinematic = true;
        active = false;
        TimeBeforeBreak = TBBOriginal;
        TimeBeforeFall = TBFOriginal;
        GetComponent<SpriteRenderer>().sprite = SpriteOrignial;
    }
}
