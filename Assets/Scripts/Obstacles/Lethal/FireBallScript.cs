using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour
{

    float CurrGameSpeed = 1.0f;
    public bool Left = true;
    public float BallSpeed = 3.0f;
    public float LifeSpan = 5.0f;
    public float BounceHeight;
    float VertSpeed = 0f;
    public bool GoingUp = false;
    // Use this for initialization
    void Start()
    {
    }
    void ResetOverWorld()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        LifeSpan -= Time.deltaTime * CurrGameSpeed;
        float percent = VertSpeed / BounceHeight;
        if (LifeSpan <= 0.0f)
            Destroy(gameObject);
        if (Left)
        {
            transform.position -= new Vector3(BallSpeed * CurrGameSpeed * Time.deltaTime, 0, 0);
            if (GoingUp)
                transform.rotation = Quaternion.AngleAxis(-90 - percent * 45f, Vector3.forward);
            else
                transform.rotation = Quaternion.AngleAxis(-90 + percent * 45f, Vector3.forward);
        }
        else
        {
            transform.position += new Vector3(BallSpeed * CurrGameSpeed * Time.deltaTime, 0, 0);
            if (GoingUp)
                transform.rotation = Quaternion.AngleAxis(90 + percent * 45f,Vector3.forward);
            else
                transform.rotation = Quaternion.AngleAxis(90 - percent * 45f, Vector3.forward);
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
            if (VertSpeed < BounceHeight)
            {
                VertSpeed += Time.deltaTime * CurrGameSpeed * 4;
                if (VertSpeed > BounceHeight)
                    VertSpeed = BounceHeight;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Going up!");

        if (other.gameObject.tag == "Ground")
        {
            GoingUp = true;
            if (VertSpeed < BounceHeight)
                BounceHeight = VertSpeed;
        }
    }

    void SetTime(int GameSpeed)
    {
        switch (GameSpeed)
        {
            case 1:
                CurrGameSpeed = 0.5f;
                GetComponent<Animator>().speed = .2f;
                break;
            case 2:
                CurrGameSpeed = 0.25f;
                GetComponent<Animator>().speed = .1f;
                break;
            case 3:
                CurrGameSpeed = 0.0f;
                GetComponent<Animator>().speed = .0f;
                break;
            default:
                CurrGameSpeed = 1.0f;
                GetComponent<Animator>().speed = .4f;
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
    void SetLeft(bool b)
    {
        Left = b;
    }
    void SetBounce(float f)
    {
        BounceHeight = f;
    }
}
