﻿using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{
    bool paused = false;
    bool mainPause = false;
    bool isInstructions = false;
    bool isSettings = false;

    public Canvas mainCanvas;
    public Canvas settingsCanvas;
    public Canvas instructionsCanvas;
    public Texture pauseOverlay;
    public Camera mainCam;

    // Use this for initialization
    void Start()
    {
        mainCanvas.enabled = false;
        settingsCanvas.enabled = false;
        instructionsCanvas.enabled = false;
        instructionsCanvas.GetComponentInChildren<InstructionArrayScript>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isSettings && !isInstructions)
            paused = TogglePause();
            else if (isSettings)
            {
                SwitchSettings();
            }
            else if (isInstructions)
            {
                SwitchInstructions();
            }
        }
    }

    bool TogglePause()
    {
        if (Time.timeScale <= 0f)
        {
            Time.timeScale = 1f;
            mainPause = false;
            mainCanvas.enabled = false;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            mainPause = true;
            mainCanvas.enabled = true;
            return (true);
        }
    }

    void OnGUI()
    {
        //GUIStyle pauseStyle = new GUIStyle();
        //pauseStyle.fontSize = 40;

        //Rect pause = new Rect(new Vector2((Screen.width / 2) - 40, (Screen.height / 2) - 120), new Vector2(20, 20));
        //GUIContent PauseHeader;

        Rect overlayRect = new Rect(new Vector3(0, 0, -20), new Vector2(Screen.width, Screen.height));
       
        ////GUIContent ResumeButton;
        //Rect resume = new Rect(pause.x + 20, pause.y+50, 80, 40);
        ////GUIContent SettingsButton;
        //Rect settings = new Rect(resume.x, resume.y + 50, 80, 40);
        ////GUIContent InstructionsButton;
        //Rect instructions = new Rect(settings.x, settings.y + 50, 80, 40);
        ////GUIContent MainMenuButton;
        //Rect mainmenu = new Rect(instructions.x, instructions.y + 50, 80, 40);
        ////GUIContent ExitGameButton;
        //Rect exit = new Rect(mainmenu.x, mainmenu.y + 50, 80, 40);
        if (paused)
        {
            GUI.DrawTexture(overlayRect, pauseOverlay);
            if (mainPause)
            {
                settingsCanvas.enabled = false;
                instructionsCanvas.enabled = false;
            }
            else if(isSettings)
            {
                settingsCanvas.enabled = true;
            }
            else if (isInstructions)
            {
                instructionsCanvas.enabled = true;
                instructionsCanvas.GetComponentInChildren<InstructionArrayScript>().enabled = true;

}
            
        }

        
    }

    void SwitchSettings()
    {
        mainPause = !mainPause;
        isSettings = !isSettings;
        mainCanvas.enabled = !mainCanvas.enabled;
        settingsCanvas.enabled = !settingsCanvas.enabled;
    }

    void SwitchInstructions()
    {
        mainPause = !mainPause;
        isInstructions = !isInstructions;
        mainCanvas.enabled = !mainCanvas.enabled;
        instructionsCanvas.enabled = !instructionsCanvas.enabled;
        instructionsCanvas.GetComponentInChildren<InstructionArrayScript>().enabled = !instructionsCanvas.GetComponentInChildren<InstructionArrayScript>().enabled;

    }

    void Unpause()
    {
        paused = false;
        TogglePause();
        isSettings = false;
        isInstructions = false;
        settingsCanvas.enabled = false;
        instructionsCanvas.enabled = false;
        instructionsCanvas.GetComponentInChildren<InstructionArrayScript>().enabled = false;
    }
}
