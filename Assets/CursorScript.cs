using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{

    public Texture2D cursor;
    Vector2 cursorSize = new Vector2(32, 32);

    bool hovered = false;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        cursor = Resources.Load("Images/Cursors_Timelock_Cursor") as Texture2D;
    }

    // Update is called once per frame
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Event.current.mousePosition.x - cursorSize.x / 2, Event.current.mousePosition.y - cursorSize.y / 2, cursorSize.x, cursorSize.y), cursor);
    }

    void Hovered(bool hov)
    {    

        if (hov)
        {
            cursor = Resources.Load("Images/Cursors_Timelock_clock") as Texture2D;

        }
        else if (!hov)
        {
            cursor = Resources.Load("Images/Cursors_Timelock_Cursor") as Texture2D;
        }
    }
}
