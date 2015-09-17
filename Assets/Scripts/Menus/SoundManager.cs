using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource hover;
    public AudioSource click;
    public AudioSource back;


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update () {
	}

    void In()
    {
        click.Play();
    }
    void Out()
    {
        back.Play();
    }
    void Hovered()
    {
        hover.Play();
    }
}
