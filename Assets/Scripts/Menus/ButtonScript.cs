using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{

    public GameObject SoundManager;
    public GameObject mainCamera;
    // Use this for initialization
    void Start()
    {
        SoundManager = GameObject.Find("SoundManager");
        mainCamera = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ArcadeMode()
    {
        SoundManager.SendMessage("ArcadeState");
        Destroy(GameObject.Find("LevelScroll"));
    }

    public void FreePlay()
    {
        SoundManager.SendMessage("FreePlayState");
        Destroy(GameObject.Find("LevelScroll"));
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

    public void SettingsCanvasOff()
    {
        GameObject.Find("GameOverWorld").SendMessage("SwitchSettings");
    }

    public void InstructionsCanvasOff()
    {
        GameObject.Find("GameOverWorld").SendMessage("SwitchInstructions");
    }

    public void Unpause()
    {
        GameObject.Find("GameOverWorld").SendMessage("Unpause");
    }
}
