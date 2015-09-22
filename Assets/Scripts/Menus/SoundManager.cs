using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource hover;
    public AudioSource click;
    public AudioSource back;
    public int GameState = 0;
    public static SoundManager ths;

    void Awake()
    {
        if (ths == null)
        {
            DontDestroyOnLoad(this);
            ths = this;
        }
        else if(ths != this)
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
	}

    void ArcadeState()
    {
        GameState = 1;
    }

    void FreePlayState()
    {
        GameState = 2;
    }

    int GetState()
    {
        return GameState;
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
