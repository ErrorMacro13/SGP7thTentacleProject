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
            Beam.transform.localScale = new Vector3(Beam.transform.localPosition.x * 2 - 1, BeamWidth, 1);
        else
            Beam.transform.localScale = new Vector3(0,0,0);
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
                Beam.transform.localScale = new Vector3(Beam.transform.localPosition.x * 2 - 1, BeamWidth, 1);
            }
        }
        else
            InitialDelay -= (Time.deltaTime * CurrGameSpeed);
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
