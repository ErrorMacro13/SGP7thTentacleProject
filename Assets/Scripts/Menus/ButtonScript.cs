using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ButtonClick(string name)
    {
        Application.LoadLevel(name);
    }

    public void ChangeImages(string name)
    {
        SendMessageUpwards(name);
    }
}
