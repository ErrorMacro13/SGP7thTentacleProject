using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {
    public AudioSource beep;
	// Use this for initialization
	void Start () {

	}
    void unmute()
    {
        
    }
	// Update is called once per frame
	void Update () {
	
	}
    public void Play()
    {
        beep.volume = gameObject.GetComponent<Slider>().value/100;
        beep.Play();
    }
    public void AlterBG()
    {
        beep.volume = GetComponent<Slider>().value / 100;
    }
}
