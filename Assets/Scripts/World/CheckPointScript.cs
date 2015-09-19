using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {

    public GameObject Door;
    public float YDown;
    bool hit = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !hit)
        {
            hit = true;
            Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y - YDown, Door.transform.position.z);
        }
    }
}
