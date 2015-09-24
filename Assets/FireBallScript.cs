using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour
{

    public float CurrGameSpeed = 1.0f;
    public bool Left = true;
    public float BallSpeed = 3.0f;
    public float LifeSpan = 5.0f;
    public float Bouncyness = 5f;
    public float BOrigin;
    public float VertSpeed = 0f;
    public bool GoingUp = false;
    // Use this for initialization
    void Start()
    {
        BOrigin = Bouncyness;
    }

    // Update is called once per frame
    void Update()
    {
        LifeSpan -= Time.deltaTime * CurrGameSpeed;
        if (LifeSpan <= 0.0f)
            Destroy(gameObject);
        if (Left)
        {
            transform.position -= new Vector3(BallSpeed * CurrGameSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(BallSpeed * CurrGameSpeed * Time.deltaTime, 0, 0);
        }
        if(GoingUp)
        {
            transform.position += new Vector3(0, CurrGameSpeed * VertSpeed * Time.deltaTime, 0);
            VertSpeed -= Time.deltaTime * CurrGameSpeed * 4;
            if (VertSpeed < 0.0f)
            {
                VertSpeed = 0;
                GoingUp = false;
            }
        }
        else
        {
            transform.position += new Vector3(0, CurrGameSpeed * -VertSpeed * Time.deltaTime, 0);
            if (VertSpeed < 10.0f)
            {
                VertSpeed += Time.deltaTime * CurrGameSpeed * 4;
                if (VertSpeed > 10.0f)
                    VertSpeed = 10.0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Going up!");

        if (other.gameObject.tag == "Ground")
        {
            GoingUp = true;
            Bouncyness = BOrigin;
        }
    }

    void SetTime(int GameSpeed)
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

    void SetLifeSpan(float f)
    {
        LifeSpan = f;
    }
    void SetBallSpeed(float f)
    {
        BallSpeed = f;
    }
    void SetBounce(float f)
    {
        Bouncyness = f;
    }
    void SetLeft(bool b)
    {
        Left = b;
    }
}
