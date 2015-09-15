using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    public GameObject Player;
    Vector3 camPosition = new Vector3(0, 0, -20);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Player.transform.position + camPosition;
	}
}
