using UnityEngine;
using System.Collections;

public class PhasingPlatformBehavior : MonoBehaviour {
    public float PhazeInSpeed = 0.005f;
    public float PhazeInDelay = 0.0f;
    public float PhazeOutSpeed = 0.005f;
    public float PhazeOutDelay = 0.0f;
    private float DelayPhazeIn = 0.0f;
    private float DelayPhazeOut = 0.0f;
    private bool PhazeOut = true;
    private bool PhazeIn = false;
    private Transform origTrans;
    private float CurrGameSpeed = 1.0f;
    private GameObject child;
    void SetTime(short GameSpeed)
    {
        switch (GameSpeed)
        {
            case 1:
                CurrGameSpeed = 0.5f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
            case 2:
                CurrGameSpeed = 0.25f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
            case 3:
                CurrGameSpeed = 0.0f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
            default:
                CurrGameSpeed = 1.0f;
                GetComponent<Animator>().speed = CurrGameSpeed;
                break;
        }
    }
    void ResetOverWorld()
    {
        transform.position = origTrans.position;
        Color newcolor = gameObject.GetComponent<Renderer>().material.color;
        newcolor.a = 1.0f;
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", newcolor);
        DelayPhazeIn = PhazeInDelay;
        DelayPhazeOut = PhazeOutDelay;
    }
	// Use this for initialization
	void Start () {
        child = GameObject.Find("Border");
        origTrans = transform;
        DelayPhazeIn = PhazeInDelay;
        DelayPhazeOut = PhazeOutDelay;
	}
	
	// Update is called once per frame
	void Update () {
        if (PhazeOut)
        {
            if (DelayPhazeOut <= 0)
            {
                Phaze();
            }
            else
            {
                DelayPhazeOut -= Time.deltaTime;
            }
        }
        else if (PhazeIn)
        {
            if (DelayPhazeIn <= 0)
            {
                Phaze();
            }
            else
            {
                DelayPhazeIn -= Time.deltaTime;
            }
        }
	}
    void Phaze()
    {
        if (gameObject.GetComponent<Renderer>().material.color.a <= 0.0)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        else
            gameObject.GetComponent<Collider2D>().isTrigger = false;

        if ((gameObject.GetComponent<Renderer>().material.color.a > 1.0f || gameObject.GetComponent<Renderer>().material.color.a < 0.0f) && PhazeOut)
        {
            Color newcolor = gameObject.GetComponent<Renderer>().material.color;
            newcolor.a = 0.0f;
            child.GetComponent<Renderer>().material.SetColor("_Color", newcolor);
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", newcolor);
            PhazeIn = true;
            PhazeOut = false;
            DelayPhazeIn = PhazeInDelay;
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        if ((gameObject.GetComponent<Renderer>().material.color.a > 1.0f || gameObject.GetComponent<Renderer>().material.color.a < 0.0f) && PhazeIn)
        {
            Color newcolor = gameObject.GetComponent<Renderer>().material.color;
            newcolor.a = 1.0f;
            child.GetComponent<Renderer>().material.SetColor("_Color", newcolor);
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", newcolor);
            PhazeIn = false;
            PhazeOut = true;
            DelayPhazeOut = PhazeOutDelay;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
        if (PhazeOut && gameObject.GetComponent<Renderer>().material.color.a > 0.0)
        {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            color.a -= PhazeOutSpeed * CurrGameSpeed;
            child.GetComponent<Renderer>().material.SetColor("_Color", color);
            
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
        }
        else if (PhazeIn && gameObject.GetComponent<Renderer>().material.color.a < 1.0)
        {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            color.a += PhazeInSpeed * CurrGameSpeed;
            child.GetComponent<Renderer>().material.SetColor("_Color", color);
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
            
        }
    }
}
