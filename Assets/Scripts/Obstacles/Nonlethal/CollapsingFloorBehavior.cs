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
    public float TimeBeforeBreak = 1.0f;
    public float PercentDone;
    Transform TransOriginal;

    float TBBOriginal;
    Sprite SpriteOrignial;

    public Sprite Crumble1;
    public Sprite Crumble2;
    public Sprite Crumble3;
    public Sprite Crumble4;


    // Use this for initialization
    void Start()
    {
        Transform TransOriginal = transform;
        TBBOriginal = TimeBeforeBreak;
        SpriteOrignial = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (active && TimeBeforeBreak > 0.0f)
        {
            TimeBeforeBreak -= Time.deltaTime * CurrGameSpeed;
        }
        if (TimeBeforeBreak <= 0.0f && CurrGameSpeed != 0.0f)
        {
            Shrink();
        }

    }

    void FixedUpdate()
    {
        PercentDone = (TBBOriginal - TimeBeforeBreak) / TBBOriginal;
        if (PercentDone > 0.0f && PercentDone < 0.25f)
            GetComponent<SpriteRenderer>().sprite = Crumble1;
        else if (PercentDone >= 0.25f && PercentDone < 0.50f)
            GetComponent<SpriteRenderer>().sprite = Crumble2;
        else if (PercentDone >= 0.50f && PercentDone < 0.75f)
            GetComponent<SpriteRenderer>().sprite = Crumble3;
        else if (PercentDone >= 0.75f)
            GetComponent<SpriteRenderer>().sprite = Crumble4;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && CurrGameSpeed != 0.0f)
        {
            active = true;
            GetComponent<SpriteRenderer>().sprite = Crumble1;
        }
    }

    void Shrink()
    {
        transform.localScale -= transform.localScale;
    }

    void ResetOverWorld()
    {
        transform.position = TransOriginal.position;
        transform.localScale.Set(TransOriginal.lossyScale.x, TransOriginal.lossyScale.y, TransOriginal.lossyScale.z);

        active = false;
        TimeBeforeBreak = TBBOriginal;
        GetComponent<SpriteRenderer>().sprite = SpriteOrignial;
    }
}
