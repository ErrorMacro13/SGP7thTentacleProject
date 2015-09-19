using UnityEngine;
using System.Collections;

public class TeslaCoilBehavior : MonoBehaviour {

    public float TimeBeforeRecharge = .5f;
    float countdown;
	// Use this for initialization
	void Start () {
        countdown = TimeBeforeRecharge;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (countdown > 0)
                countdown -= Time.deltaTime;
            else
            {
                countdown = TimeBeforeRecharge;
                SendMessageUpwards("Refill", 1.0f);
            }
        }
    }
}
