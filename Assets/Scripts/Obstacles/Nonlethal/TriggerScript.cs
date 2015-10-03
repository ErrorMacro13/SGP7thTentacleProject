using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {
    public Animation animate;
    public AnimationClip animateClip;
    public GameObject acid;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D ent)
    {
        print("Collides with: " + ent.tag);
        if (gameObject.tag != "Stop" && ent.tag == "Player")
        {
            SendMessageUpwards("Activate");
        }
        if (ent.tag == "Acid")
        {
            print("ResetAcid");
            GetComponent<Animator>().SetTrigger("PlayTrigger");
            ent.SendMessage("ResetOverWorld");
        }
    }
}