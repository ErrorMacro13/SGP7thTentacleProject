using UnityEngine;
using System.Collections;

public class PlateChildScript : MonoBehaviour {
    public Sprite ActiveImg;
    public Sprite InactiveImg;
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
	
	}
    void Activate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ActiveImg;
    }
    void Deactivate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = InactiveImg;
    }
}
