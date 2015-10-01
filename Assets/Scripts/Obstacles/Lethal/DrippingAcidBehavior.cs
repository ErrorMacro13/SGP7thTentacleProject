using UnityEngine;
using System.Collections;

public class DrippingAcidBehavior : MonoBehaviour {
    private Vector3 StartLoc;
    public float dripSpeed = 0.2f;
    public float FallSpeed = 2;
    private Vector3 Size;
    public float GrowSpeed = .005f;
    public float GrowSize = .05f;
    private float CurrGameSpeed = 1.0f;
    public GameObject endSpot;
    bool grow = true;
	void SetTime(short GameSpeed){
		switch (GameSpeed) {
            case 1:
                CurrGameSpeed = 0.5f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
            case 2:
                CurrGameSpeed = 0.25f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
            case 3:
                CurrGameSpeed = 0.0f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
            default:
                CurrGameSpeed = 1.0f;
                GetComponent<Animator>().speed = CurrGameSpeed;
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
        Size = transform.lossyScale;
        transform.localScale -= transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (grow) Grow();
        else Fall(Time.deltaTime);
	}
    void Grow()
    {
        transform.localScale += new Vector3(GrowSpeed*dripSpeed*CurrGameSpeed, GrowSpeed*dripSpeed*CurrGameSpeed, 0);
        if (transform.localScale.x >= GrowSize)
            grow = false;
    }
    void Fall(float dt)
    {
        gameObject.transform.position += new Vector3(0, -FallSpeed * dt * CurrGameSpeed, 0);
        if (transform.position.y <= endSpot.transform.position.y)
        {
            ResetOverWorld();
        }
    }
}
