using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{

    public GameObject SoundManager;
    public GameObject mainCamera;
    public GameObject levelScroll;
    // Use this for initialization
    void Start()
    {
        SoundManager = GameObject.Find("SoundManager");
        mainCamera = GameObject.Find("Main Camera");
        levelScroll = GameObject.Find("LevelScroll");
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
        print("hovered cursor");
        SoundManager.SendMessage("Hovered");
        mainCamera.SendMessage("Hovered", true);
        
    }

    public void OnMouseExit()
    {
        print("normal cursor");
        mainCamera.SendMessage("Hovered", false);
    }

    public void LevelSelect(int level)
    {
        levelScroll.SendMessage("LevelHover", level);
    }

    public void LevelUnlock()
    {
        levelScroll.SendMessage("Unlock");
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
