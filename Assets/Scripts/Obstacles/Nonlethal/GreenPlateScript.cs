using UnityEngine;
using System.Collections;

public class GreenPlateScript : MonoBehaviour {
    public GameObject[] targets;
    public Sprite ActivatedImg;
    public Sprite NonactiveImg;
    public float duration = 5;
    private float tempduration;
    private Vector3 size;
    private Transform origTrans;
	// Use this for initialization
	void Start () {
        origTrans = transform;
        tempduration = duration;
        size = transform.lossyScale;
	}
    void ResetOverWorld()
    {
        transform.position = origTrans.position;
        transform.localScale = origTrans.localScale;
        tempduration = duration;
        GetComponent<SpriteRenderer>().sprite = NonactiveImg;
        BroadcastMessage("Deactivate");
    }
	// Update is called once per frame
	void Update () {
        if (tempduration <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = NonactiveImg;
            BroadcastMessage("Deactivate");
            tempduration = duration;
        }
        else tempduration -= Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D ent)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].BroadcastMessage("GreenTrigger", duration);
        }
        GetComponent<SpriteRenderer>().sprite = ActivatedImg;
        BroadcastMessage("Activate");
        transform.localScale = new Vector3(1.0f,.5f,1.0f);
    }
    void OnTriggerExit2D(Collider2D ent)
    {
        transform.localScale = size;
    }
}
