using UnityEngine;
using System.Collections;

public class SpinningBladeBehavior : MonoBehaviour
{
    float CurrGameSpeed = 1.0f;
    public float rotateSpeed = 10.0f;
    public bool Clockwise = false;
    // Use this for initialization
    void Start()
    {
        //GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Clockwise)
            transform.Rotate(0, 0, rotateSpeed * CurrGameSpeed * Time.timeScale);
        else
            transform.Rotate(0, 0, -rotateSpeed * CurrGameSpeed * Time.timeScale);


        if (CurrGameSpeed < 0.25f)
        {
            tag = "Untagged";
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f);
        }
        else if (CurrGameSpeed >= 0.25f)
        {
            tag = "Lethal";
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

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

    void ResetOverWorld() { }

}
