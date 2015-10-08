using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour
{

    float CurrGameSpeed = 1.0f;
    float delayTime = .25f;
    public Sprite[] sprites;
    int currSprite = 0;
    void ResetOverWorld()
    {
        Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition += new Vector3(Time.deltaTime * 10 * CurrGameSpeed, 0, 0);
        delayTime -= Time.deltaTime * CurrGameSpeed;
        if (delayTime <= 0.0f)
        {
            currSprite++;
            if (currSprite == sprites.Length)
                currSprite = 0;
            delayTime = .25f;
            GetComponent<SpriteRenderer>().sprite = sprites[currSprite];
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.tag == "Lethal")
            Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        print("entered");
        if (gameObject.tag == "Lethal")
            Destroy(gameObject);
    }

    void SetTime(int GameSpeed)
    {
        switch (GameSpeed)
        {
            case 1:
                CurrGameSpeed = 0.5f;
                gameObject.tag = "Lethal";
                GetComponent<Rigidbody2D>().isKinematic = false;
                break;
            case 2:
                CurrGameSpeed = 0.25f;
                gameObject.tag = "Lethal";
                GetComponent<Rigidbody2D>().isKinematic = false;
                break;
            case 3:
                CurrGameSpeed = 0.0f;
                gameObject.tag = "Ground";
                GetComponent<Rigidbody2D>().isKinematic = true;
                break;
            default:
                CurrGameSpeed = 1.0f;
                gameObject.tag = "Lethal";
                GetComponent<Rigidbody2D>().isKinematic = false;
                break;
        }
    }


}
