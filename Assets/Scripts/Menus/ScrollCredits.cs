using UnityEngine;
using System.Collections;

public class ScrollCredits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < 1300f)
            transform.position += new Vector3(0,1.0f,0);
        
	}
}
