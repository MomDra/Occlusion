using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSStatus : MonoBehaviour
{
    [Range(10, 150)]
    public int fontSize = 30;
    public Color color = new Color(.0f, .0f, .0f, 1.0f);
    public float width, height;

    int count;
    int showCount;
    float time;

    void OnGUI()
    {
        time += Time.deltaTime;
        ++count;
        string text = string.Format("FPS: {0}", showCount);

        if (time >= 1f)
        {
            text = string.Format("FPS: {0}", count);
            showCount = count;
            count = 0;

            time = 0f;
        }
        Rect position = new Rect(width, height, Screen.width, Screen.height);


        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = color;

        GUI.Label(position, text, style);
    }
}
