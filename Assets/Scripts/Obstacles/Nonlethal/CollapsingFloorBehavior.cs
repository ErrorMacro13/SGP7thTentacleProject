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

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (active && TimeBeforeFall > 0.0f)
        {
            TimeBeforeFall -= Time.deltaTime * CurrGameSpeed;
        }
        if(TimeBeforeFall <= 0.0f)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && CurrGameSpeed != 0.0f)
        {
            print("hit!");
            active = true;
        }
    }
}
