using UnityEngine;
using System.Collections;

public class PlayAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void PlayAnimation()
    {
        GetComponent<Animator>().Play("Close_");
    }
}
