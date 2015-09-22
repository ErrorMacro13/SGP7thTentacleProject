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

    Transform TransOriginal;

    float TBBOriginal;
    float TBFOriginal;
    Sprite SpriteOrignial;

    public Sprite Crumble;


    // Use this for initialization
    void Start()
    {
        Transform TransOriginal = transform;
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
            Shrink();
        }

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
        GetComponent<Transform>().localScale -= GetComponent<Transform>().localScale;
    }

    void ResetOverWorld()
    {
        transform.position = TransOriginal.position;
        transform.localScale.Set(TransOriginal.lossyScale.x, TransOriginal.lossyScale.y, TransOriginal.lossyScale.z);

        GetComponent<Rigidbody2D>().isKinematic = true;
        active = false;
        TimeBeforeBreak = TBBOriginal;
        TimeBeforeFall = TBFOriginal;
        GetComponent<SpriteRenderer>().sprite = SpriteOrignial;
    }
}
