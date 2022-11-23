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

    // 바닥, 하늘 깊이
    int groundSkyDepth = 30;

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
        Color color = new Color(1f, 0f, 0f, 0.5f);

        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                // images 정보 가져오기
                GameObject imgPixel = Instantiate(imgPrefab, Vector3.zero, Quaternion.identity, transform);
                imgPixel.GetComponent<RectTransform>().localPosition = Vector3.zero;
                images[i, j] = imgPixel.GetComponent<Image>();
                images[i, j].color = color;
                images[i, j].material = null;


                // 배경 정보 초기화
                // 직선의 방적식 만들어서 그 점이 직선 오른쪽이면 30 넣어주면 된다, 직선 두점: (560, 213), (581, 718)\
                // 기울기 y = ax +b, (y2 - y1) / (x2 - x1) * (x - x1) - y + y1 = 0

                if ((505f / 21) * (((float)(j) / width * 876) - 560f) - ((float)i / height * 876) + 213f <= 0f) // 세로선 기준 왼쪽
                {
                    Debug.Log(i + ", " + j);

                    currBackGroundDepth[i, j] = 11;

                    images[i, j].color = new Color(0f, 1f, 1f, 0.5f);

                    // (236, 26), (560, 214)
                    if ((188f / 324f) * (((float)(j) / width * 876f) - 236f) - ((float)i / height * 876f) + 26f > 0f) // 건물 오른쪽 위
                    {
                        images[i, j].color = new Color(0f, 0f, 1f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                    }
                    // (236, 26), (0, 98)
                    else if (72f / (-236f) * (((float)(j) / width * 876f) - 236f) - ((float)i / height * 876f) + 26f > 0f) // 건물 왼쪽 위
                    {
                        images[i, j].color = new Color(0f, 0f, 1f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                    }
                    // (247, 781), (581, 718)
                    else if ((-63f) / 334f * (((float)(j) / width * 876f) - 247f) - ((float)i / height * 876f) + 781f < 0) // 건물 오른쪽 밑
                    {
                        images[i, j].color = new Color(1f, 0f, 0f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                    }
                    // (0, 758), (247, 781)
                    else if (23f / 247f * ((float)(j) / width * 876f) - ((float)i / height * 876f) + 758f < 0) // 건물 왼쪽 밑
                    {
                        images[i, j].color = new Color(1f, 0f, 0f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                    }

                }
                else // 세로선 기준 오른쪽
                {
                    if ((float)i / height * 876f <= 595f) // 하늘
                    {
                        images[i, j].color = new Color(0f, 0f, 1f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                    }
                    else // 바닥
                    {
                        images[i, j].color = new Color(1f, 0f, 0f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                    }

                    // currBackGroundDepth[i, j] = 30;
                    // images[i, j].material = masking;
                }
            }
        }
    }

    public void SyncMasking(int[,] arr)
    {
        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                if (currBackGroundDepth[i, j] > arr[i, j])
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
