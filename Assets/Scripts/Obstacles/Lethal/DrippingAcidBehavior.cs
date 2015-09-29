using UnityEngine;
using System.Collections;

public class DrippingAcidBehavior : MonoBehaviour {
    private Vector3 StartLoc;
    public float dripSpeed = 0.2f;
    public float FallSpeed = 2;
    private float CurrGameSpeed = 1.0f;
    public GameObject endSpot;
    bool grow = true;
	void SetTime(short GameSpeed){
		switch (GameSpeed) {
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
    
    void ResetOverWorld(){
        transform.position = StartLoc;
        transform.localScale -= transform.localScale;
        grow = true;
    }
	// Use this for initialization
	void Start () {
	    StartLoc = transform.position;
        transform.localScale -= transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (grow) Grow();
        else Fall(Time.deltaTime);
	}
    void Grow()
    {
        transform.localScale += new Vector3(.1f*dripSpeed*CurrGameSpeed, .1f*dripSpeed*CurrGameSpeed, 0);
        if (transform.localScale.x >= .5)
            grow = false;
    }
    void Fall(float dt)
    {
        gameObject.transform.position += new Vector3(0, -FallSpeed * dt * CurrGameSpeed, 0);
        print(transform.position.y <= endSpot.transform.position.y);
        print(endSpot.transform.position.y);
        if (transform.position.y <= endSpot.transform.position.y) ResetOverWorld();
    }
}
