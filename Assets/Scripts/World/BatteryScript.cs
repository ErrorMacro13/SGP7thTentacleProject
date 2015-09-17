using UnityEngine;
using System.Collections;

public class BatteryScript : MonoBehaviour {

    public GameObject World;
    private Vector3 size;
    public int refillAmt;
	// Use this for initialization
	void Start () {
        size = GetComponent<Transform>().localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D ent)
    {
        print(ent.tag);
        Debug.Log(ent.tag);
        if (ent.tag == "Player")
        {
            print("coll w/ player");
            World.BroadcastMessage("Refill", refillAmt);
            gameObject.transform.localScale -= gameObject.transform.localScale;
        }
    }

    void ResetOverWorld()
    {
        gameObject.transform.localScale = size;
    }
}
