using UnityEngine;
using System.Collections;

public class LightningConnectionScript : MonoBehaviour {
    private bool rotate = false;
    private Collider2D player;
    private Vector3 dad;
	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(0, 0, 0);
	}
    public float width = 1;
	// Update is called once per frame
	void FixedUpdate () {
        if (rotate)
        {
            dad = GetComponentInParent<Transform>().position;
            transform.localPosition = new Vector3((player.transform.position.x - dad.x) * .5f, (dad.y), 1);
            transform.localScale = new Vector3(transform.position.x*20,width,0);
            transform.rotation = Quaternion.AngleAxis((Mathf.Rad2Deg * Mathf.Atan((dad.y * 2) / (transform.localPosition.x * 2))), Vector3.forward);
        }
	}

    void Show(Collider2D other)
    {
        player = other;
        rotate = true;
        transform.localScale = new Vector3(1, 1, 1);
        //GetComponent<Animator>().SetFloat("Play", 0);
    }
    void Hide()
    {
        rotate = false;
        transform.localScale = new Vector3(0, 0, 0);
        //GetComponent<Animator>().SetFloat("Play", 2);
    }
}
