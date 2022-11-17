using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepthInfo : MonoBehaviour
{
    int[,] basicBackGroundDepth = new int[10, 10];
    int[,] currBackGroundDepth;

    GridLayoutGroup gridLayoutGroup;

    Image[,] images;
    [SerializeField] GameObject imgPrefab;

    [SerializeField] Material masking;

    // 해상도 크기
    int width;
    int height;

    // 이미지의 크기
    float imgWidth;
    float imgHeight;

    void Awake()
    {
        width = Setting.Instance.Width;
        height = Setting.Instance.Height;

        imgWidth = Setting.Instance.ImgWidth;
        imgHeight = Setting.Instance.ImgHeight;

        Vector2 imgSize = new Vector2(imgWidth, imgHeight);
        GetComponent<RectTransform>().sizeDelta = imgSize;
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(imgWidth / width, imgHeight / height);
    }

    void Start()
    {
        images = new Image[height, width];
        currBackGroundDepth = new int[height, width];

        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                GameObject imgPixel = Instantiate(imgPrefab, Vector3.zero, Quaternion.identity, transform);
                imgPixel.GetComponent<RectTransform>().localPosition = Vector3.zero;
                images[i, j] = imgPixel.GetComponent<Image>();
                images[i, j].color = new Color(1f, 0f, 0f, 0.5f);
            }
        }

        Color color = new Color(1f, 0f, 0f, 0.5f);

        for (int i = 0; i < 9; ++i)
        {
            basicBackGroundDepth[i, 0] = 5;
        }
        basicBackGroundDepth[9, 0] = 4;

        for (int i = 0; i < 8; ++i)
        {
            basicBackGroundDepth[i, 1] = 6;
        }
        basicBackGroundDepth[8, 1] = 5;
        basicBackGroundDepth[9, 1] = 4;

        for (int i = 0; i < 7; ++i)
        {
            basicBackGroundDepth[i, 2] = 20;
        }
        basicBackGroundDepth[7, 2] = 7;
        basicBackGroundDepth[8, 2] = 5;
        basicBackGroundDepth[9, 2] = 4;

        for (int i = 0; i < 7; ++i)
        {
            basicBackGroundDepth[i, 3] = 20;
        }
        basicBackGroundDepth[7, 3] = 17;
        basicBackGroundDepth[8, 3] = 5;
        basicBackGroundDepth[9, 3] = 4;

        for (int i = 0; i < 7; ++i)
        {
            basicBackGroundDepth[i, 4] = 20;
        }
        basicBackGroundDepth[7, 4] = 17;
        basicBackGroundDepth[8, 4] = 5;
        basicBackGroundDepth[9, 4] = 4;

        for (int i = 0; i < 7; ++i)
        {
            basicBackGroundDepth[i, 5] = 20;
        }
        basicBackGroundDepth[7, 5] = 17;
        basicBackGroundDepth[8, 5] = 5;
        basicBackGroundDepth[9, 5] = 4;

        for (int i = 0; i < 7; ++i)
        {
            basicBackGroundDepth[i, 6] = 20;
        }
        basicBackGroundDepth[7, 6] = 17;
        basicBackGroundDepth[8, 6] = 5;
        basicBackGroundDepth[9, 6] = 4;

        for (int i = 0; i < 6; ++i)
        {
            basicBackGroundDepth[i, 7] = 20;
        }
        basicBackGroundDepth[6, 7] = 7;
        basicBackGroundDepth[7, 7] = 7;
        basicBackGroundDepth[8, 7] = 5;
        basicBackGroundDepth[9, 7] = 4;

        for (int i = 0; i < 8; ++i)
        {
            if (i >= 2 && i <= 7)
                basicBackGroundDepth[i, 8] = 6;
            else
                basicBackGroundDepth[i, 8] = 20;
        }
        basicBackGroundDepth[8, 8] = 5;
        basicBackGroundDepth[9, 8] = 4;


        basicBackGroundDepth[0, 9] = 20;
        for (int i = 1; i < 9; ++i)
        {
            basicBackGroundDepth[i, 9] = 5;
        }
        basicBackGroundDepth[9, 9] = 4;
    }

    public void SyncMasking(int[,] arr)
    {
        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                if (basicBackGroundDepth[i, j] > arr[i, j])
                {
                    images[i, j].material = null;
                }
                else
                {

                    images[i, j].material = masking;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
