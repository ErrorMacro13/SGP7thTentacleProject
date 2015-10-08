using UnityEngine;
using System.Collections;

public class LaserTurretBehavior : MonoBehaviour
{

    float CurrGameSpeed = 1.0f;
    public float InitialDelay = 1.0f;
    public float ChargeUpTime = 1.0f;
    public float FireTime = 1.0f;
    public float BeamWidth = 1.0f;
    public bool On = false;
    public GameObject PEStart;
    public GameObject PEEnd;

    float ID;
    bool IS;

    public GameObject Beam;
    public GameObject LaserCatcher;

    public Sprite l0;
    public Sprite l1;
    public Sprite l2;
    public Sprite l3;
    public Sprite l4;
    public Sprite l5;
    public Sprite l6;
    public Sprite l7;
    public Sprite l8;
    public Sprite l9;
    public Sprite l10;
    public Sprite l11;

    // Use this for initialization
    void ResetOverWorld()
    {
        On = IS;
        InitialDelay = ID;
    }

    void Start()
    {
        IS = On;
        ID = InitialDelay;
        Beam.transform.localPosition = LaserCatcher.transform.localPosition / 2;
        if (On)
        {
            Beam.transform.localScale = new Vector3(Beam.transform.localPosition.x * 0.88f - 1, BeamWidth, 1);
        }

        else
        {
            Beam.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (InitialDelay <= 0.0f)
        {
            if (On)
            {
                PEStart.GetComponent<ParticleSystem>().Play();
                PEStart.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
                print(PEStart.GetComponent<ParticleSystem>().playbackSpeed.ToString());
                On = false;
                InitialDelay = ChargeUpTime;
                Beam.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                PEEnd.GetComponent<ParticleSystem>().Play();
                PEEnd.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
                print(PEEnd.GetComponent<ParticleSystem>().playbackSpeed.ToString());
                On = true;
                InitialDelay = FireTime;
                Beam.transform.localScale = new Vector3(Beam.transform.localPosition.x * 0.88f - 1, BeamWidth, 1);
            }
        }
        else
        {
            InitialDelay -= (Time.deltaTime * CurrGameSpeed);
            if (On)
            {
                float percent = InitialDelay / FireTime;
                if (percent >= 0.9f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l0;
                else if (percent >= 0.8f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l1;
                else if (percent >= 0.72f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l2;
                else if (percent >= 0.64f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l3;
                else if (percent >= 0.58f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l4;
                else if (percent >= 0.5f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l5;
                else if (percent >= 0.41f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l6;
                else if (percent >= 0.33f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l7;
                else if (percent >= 0.25f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l8;
                else if (percent >= 0.16f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l9;
                else if (percent >= 0.08f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l10;
                else if (percent >= 0.0f)
                    Beam.GetComponent<SpriteRenderer>().sprite = l11;
            }
        }
    }

    void SetTime(short GameSpeed)
    {
        switch (GameSpeed)
        {
            case 1:
                CurrGameSpeed = 0.5f;
                break;
            case 2:
                CurrGameSpeed = 0.25f;
                break;
            case 3:
                CurrGameSpeed = 0.0f;
                break;
            default:
                CurrGameSpeed = 1.0f;
                break;
        }
        PEStart.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
        PEEnd.GetComponent<ParticleSystem>().playbackSpeed = CurrGameSpeed;
    }
}
