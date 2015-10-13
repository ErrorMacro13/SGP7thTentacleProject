using UnityEngine;
using System.Collections;

public class TreadmillBehavior : MonoBehaviour
{
    float CurrGameSpeed = 1.0f;
    float magnitude = 200;
    float rotateSpeed = 10;
    public bool leftDirection = true;
    public AreaEffector2D aEffector;
    public GameObject[] rotators;

    // Use this for initialization
    void Start()
    {
       
        if (leftDirection)
        {
            magnitude = -200;
            rotateSpeed = 10;
            for (int i = 0; i < 3; i++)
            {
                rotators[i].GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/TreadmillLeftTurnPH", typeof(Sprite)) as Sprite;
            }
        }
        else
        {
            magnitude = 200;
            rotateSpeed = -10;
            for (int i = 0; i < 3; i++)
            {
                rotators[i].GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Obstacles/TreadmillRightTurnPH", typeof(Sprite)) as Sprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetMagnitude();
        Rotate();
    }

    void SetMagnitude()
    {
        aEffector.forceMagnitude = magnitude * CurrGameSpeed;
    }

    void Rotate()
    {
        for (int i = 0; i < 3; i++)
        {
            rotators[i].transform.Rotate(0, 0, rotateSpeed * CurrGameSpeed * Time.deltaTime);
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
}
