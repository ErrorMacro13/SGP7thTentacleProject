using UnityEngine;
using System.Collections;

public class LethalGasBehavior : MonoBehaviour
{
    float killTime = 5.0f;

    bool isColliding = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding)
        {
            killTime -= Time.deltaTime;
        }
        else
        {
            killTime = 5.0f;
        }

        if (killTime <= 0.0f)
        {
            tag = "Lethal";
        }
        else
        {
            tag = "Untagged";
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }

    void ResetOverWorld() { tag = "Untagged"; }

}
