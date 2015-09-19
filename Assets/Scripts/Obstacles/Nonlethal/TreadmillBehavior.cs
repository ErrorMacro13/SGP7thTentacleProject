using UnityEngine;
using System.Collections;

public class TreadmillBehavior : MonoBehaviour
{
    float CurrGameSpeed = 1.0f;
    float magnitude = 200;
    public bool leftDirection = true;
    public AreaEffector2D aEffector;

    // Use this for initialization
    void Start()
    {
        if (leftDirection)
        {
            magnitude = -200;
        }
        else
        {
            magnitude = 200;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetMagnitude();

    }

    void SetMagnitude()
    {
        aEffector.forceMagnitude = magnitude * CurrGameSpeed;
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
