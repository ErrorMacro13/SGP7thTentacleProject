using UnityEngine;
using System.Collections;

public class POCScript : MonoBehaviour {
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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Fall(Time.deltaTime);
	}
    void Fall(float dt)
    {
        gameObject.transform.position += new Vector3(0, -3 * dt * CurrGameSpeed, 0);
    }
}
