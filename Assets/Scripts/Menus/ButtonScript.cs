using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{

    public GameObject SoundManager;

    // Use this for initialization
    void Start()
    {
        SoundManager = GameObject.Find("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClick(string name)
    {
        if (name != "Back")
            SoundManager.SendMessage("In");
        else
            SoundManager.SendMessage("Out");
        Application.LoadLevel(name);
    }

    public void ChangeImages(string name)
    {
        SendMessageUpwards(name);
    }
    public void Quit()
    {
        Application.Quit();
    }

    void OnMouseEnter()
    {
        SoundManager.SendMessage("Hovered");
    }
}
