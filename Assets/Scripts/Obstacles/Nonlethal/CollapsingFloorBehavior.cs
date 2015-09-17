using UnityEngine;
using System.Collections;

public class CollapsingFloorBehavior : MonoBehaviour {

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
    public Sprite Crumble;

    Transform Originalpos;

    // Use this for initialization
    void Start () {
        Originalpos = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (active && TimeBeforeFall > 0.0f)
        {
            TimeBeforeFall -= Time.deltaTime * CurrGameSpeed;
        }
        if(TimeBeforeFall <= 0.0f)
        {
            if (GetComponent<Rigidbody2D>().isKinematic != false)
                Invoke("Shrink", 0.5f);
            GetComponent<Rigidbody2D>().isKinematic = false;
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
        print("shrink");
        GetComponent<Transform>().localScale -= GetComponent<Transform>().localScale;
    }

    void ResetOverWorld()
    {
        GetComponent<Transform>().position = Originalpos.position;
        GetComponent<Transform>().lossyScale.Set(Originalpos.lossyScale.x, Originalpos.lossyScale.y, Originalpos.lossyScale.z);
        GetComponent<Transform>().rotation = Originalpos.rotation;
    }
}
