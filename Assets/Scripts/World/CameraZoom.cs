using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

    GameObject Cam;
    public float newZoom;
    public float YOffset;
    public float XOffset;
    public bool Locked;
    bool zooming = false;
    Vector3 Offset;
    bool update = false;

	// Use this for initialization
	void Start () {
        Cam = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (zooming)
        {
            if (Locked)
            {
                Cam.SendMessage("RePosition", new Vector3(0, 0, -20));
            }
            else
            {
                if (newZoom > Cam.gameObject.GetComponent<Camera>().orthographicSize)
                {
                    Cam.gameObject.GetComponent<Camera>().orthographicSize += .1f;
                    if (Cam.gameObject.GetComponent<Camera>().orthographicSize > newZoom)
                        Cam.gameObject.GetComponent<Camera>().orthographicSize = newZoom;
                }
                else if (newZoom < Cam.gameObject.GetComponent<Camera>().orthographicSize)
                {
                    Cam.gameObject.GetComponent<Camera>().orthographicSize -= .1f;
                    if (Cam.gameObject.GetComponent<Camera>().orthographicSize < newZoom)
                        Cam.gameObject.GetComponent<Camera>().orthographicSize = newZoom;
                }

                if (Offset.x > XOffset)
                {
                    update = true;
                    Offset.x -= .05f;
                    if (Offset.x < XOffset)
                        Offset.x = XOffset;
                }
                else if (Offset.x < XOffset)
                {
                    update = true;
                    Offset.x += .05f;
                    if (Offset.x > XOffset)
                        Offset.x = XOffset;
                }

                if (Offset.y > YOffset)
                {
                    update = true;
                    Offset.y -= .05f;
                    if (Offset.y < YOffset)
                        Offset.y = YOffset;
                }
                else if (Offset.y < YOffset)
                {
                    update = true;
                    Offset.y += .05f;
                    if (Offset.y > YOffset)
                        Offset.y = YOffset;
                }
            }
            if (update)
            {
                update = false;
                Cam.SendMessage("RePosition", Offset);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Offset = Cam.transform.position;
            zooming = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            zooming = false;
        }
    }
}
