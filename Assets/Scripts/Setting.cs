using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    private static Setting instance;

    public static Setting Instance { get => instance; }


    [SerializeField]
    private int width;
    [SerializeField]
    private int height;

    public int Width { get => width; }
    public int Height { get => height; }

    [SerializeField]
    private float imgWidth;
    [SerializeField]
    private float imgHeight;

    public float ImgWidth { get => imgWidth; }
    public float ImgHeight { get => imgHeight; }

    [SerializeField]
    Transform targetObject;

    public Transform TargetObject { get => targetObject; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}
