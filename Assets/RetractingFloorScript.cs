using UnityEngine;
using System.Collections;

public class RetractingFloorScript : MonoBehaviour {

    public Sprite[] Floor;
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = Floor[0];
	}
    void ResetOverWorld()
    {
        Start();
    }
	// Update is called once per frame
	void Update () {
	
	}

    public bool ChangeImage(int index)
    {
        if(GetComponent<SpriteRenderer>().sprite = Floor[index]) return true;
        else return false;
    }
}
