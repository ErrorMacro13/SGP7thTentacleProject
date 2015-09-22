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

    public void ArcadeMode()
    {
        SoundManager.SendMessage("ArcadeState");
    }

    public void FreePlay()
    {
        SoundManager.SendMessage("FreePlayState");
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

    public void OnMouseEnter()
    {
        SoundManager.SendMessage("Hovered");
    }
    public void ToggleFS()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
