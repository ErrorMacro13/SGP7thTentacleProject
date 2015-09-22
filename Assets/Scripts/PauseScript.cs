using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour
{
    bool paused = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = TogglePause();
        }
    }

    bool TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }

    void OnGUI()
    {
        GUIStyle pauseStyle = new GUIStyle();

        if (paused)
        {
            GUILayout.Label("Paused");
            if (GUILayout.Button("Resume"))
                paused = TogglePause();
        }
    }
}
