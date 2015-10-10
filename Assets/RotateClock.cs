using UnityEngine;
using System.Collections;

public class RotateClock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 215 * Time.deltaTime, 0);
	}
}
