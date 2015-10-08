using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeslaAnimationScript : MonoBehaviour
{
    public GameObject[] images;
    private int activeImageCount = 0;
    public float TimeBetweenArcs = 0.5f;
    private float ArcTime;
    private float ArcLength;
    public float MaxArcs = 5;
    private bool Hiden = false;
    ////private List<float> ArcLengths;
    ////private List<GameObject> Arcs;

    // Use this for initialization
    void Start()
    {
        Show();
        ArcTime = TimeBetweenArcs;
        for (int i = 0; i < 9; i++)
        {
            images[i].GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        }
    }
    void Hide()
    {
        Hiden = true;
        for (int i = 0; i < 9; i++)
        {
            images[i].GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        }
    }
    void Show()
    {
        Hiden = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Hiden)
        {
            if (ArcTime <= 0)
            {
                ArcTime = TimeBetweenArcs;
                for (int i = 0; i < 9; i++)
                {
                    images[i].GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
                }
                for (int i = 0; i < Random.Range(1, MaxArcs); i++)
                {
                    int index = Random.Range(0, 8);
                    if (images[index].GetComponent<Transform>().localScale != new Vector3(1, 1, 1))
                        images[index].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                }
            }
            else ArcTime -= Time.deltaTime;
        }
        else
        {

        }

        ////if (!(Arcs.Count == MaxArcs) && ArcTime <= 0)
        ////{
        ////    ArcTime = TimeBetweenArcs;
        ////    DrawImage();
        ////}
        ////else
        ////{
        ////    ArcTime -= Time.deltaTime;
        ////}
        ////for (int i = 0; i < ArcLengths.Count; i++)
        ////{
        ////    if (ArcLengths[i] <= 0)
        ////    {
        ////        Destroy(Arcs[i]);
        ////        Arcs.RemoveAt(i);
        ////        ArcLengths.RemoveAt(i);
        ////    }
        ////    else ArcLengths[i] -= Time.deltaTime;
        ////}
    }

    void DrawImage()
    {

        ArcLength = Random.value;
        ////tempBolt = Instantiate(images[Random.Range(0, images.Length)], new Vector3(0, 1.2f, 0), new Quaternion(Random.Range(0, 60), 0, Random.Range(0, 360), 0)) as GameObject;
        ////tempBolt.transform.parent = transform;
        ////tempBolt.transform.localScale = new Vector3(1, 0.5f, 1);
        ////Arcs.Add(tempBolt);
        ////ArcLengths.Add(Random.value);
    }
    private float CurrGameSpeed = 1.0f;
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
    }
    void ResetOverWorld()
    {
        Start();
    }
}
