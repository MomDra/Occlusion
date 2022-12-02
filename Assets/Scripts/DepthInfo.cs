using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepthInfo : MonoBehaviour
{
    int[,] currBackGroundDepth;
    bool[,] crashArea;

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
    int buildingFrontDepth = 11;
    int buildingBackDepth = 14;

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
        crashArea = new bool[height, width];
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
                crashArea[i, j] = true;

                float mappedX = (float)j / width * 876;
                float mappedY = (float)i / height * 876;


                // 배경 정보 초기화
                // 직선의 방적식 만들어서 그 점이 직선 오른쪽이면 30 넣어주면 된다, 직선 두점: (560, 213), (581, 718)
                // 기울기 y = ax +b, (y2 - y1) / (x2 - x1) * (x - x1) - y + y1 = 0

                if ((505f / 21) * (mappedX - 560f) - mappedY + 213f <= 0f) // 세로선 기준 왼쪽
                { // 여기가 건물
                    Debug.Log(i + ", " + j);

                    currBackGroundDepth[i, j] = buildingFrontDepth;
                    images[i, j].color = new Color(1f, 1f, 1f, 0.5f);

                    // 건물 앞뒤 x 필셀 값 범위 247 ~ 581
                    float ratio;

                    if (mappedX >= 247f)
                    {
                        ratio = (mappedX - 247f) / 334f;

                        if (ratio <= 1f / 4)
                        {
                            currBackGroundDepth[i, j] = buildingFrontDepth;
                            images[i, j].color = new Color(1f, 0f, 0f, 0.5f);
                        }
                        else if (ratio <= 2f / 4)
                        {
                            currBackGroundDepth[i, j] = buildingFrontDepth + 1;
                            images[i, j].color = new Color(0f, 1f, 0f, 0.5f);
                        }
                        else if (ratio <= 3f / 4)
                        {
                            currBackGroundDepth[i, j] = buildingFrontDepth + 2;
                            images[i, j].color = new Color(0f, 0f, 1f, 0.5f);
                        }
                        else
                        {
                            currBackGroundDepth[i, j] = buildingFrontDepth + 3;
                            images[i, j].color = new Color(0f, 1f, 1f, 0.5f);
                        }
                    }
                    else
                    {
                        ratio = mappedX / 247f;

                        if (ratio <= 1f / 3)
                        {
                            currBackGroundDepth[i, j] = buildingFrontDepth + 2;
                            images[i, j].color = new Color(0f, 0f, 1f, 0.5f);
                        }
                        else if (ratio <= 2f / 3)
                        {
                            currBackGroundDepth[i, j] = buildingFrontDepth + 1;
                            images[i, j].color = new Color(0f, 1f, 0f, 0.5f);
                        }
                        else
                        {
                            currBackGroundDepth[i, j] = buildingFrontDepth;
                            images[i, j].color = new Color(1f, 0f, 0f, 0.5f);
                        }
                    }

                    // (236, 26), (560, 214)
                    if ((188f / 324f) * (mappedX - 236f) - mappedY + 26f > 0f) // 건물 오른쪽 위
                    {
                        images[i, j].color = new Color(0f, 0f, 1f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                        crashArea[i, j] = false;
                    }
                    // (236, 26), (0, 98)
                    else if (72f / (-236f) * (mappedX - 236f) - mappedY + 26f > 0f) // 건물 왼쪽 위
                    {
                        images[i, j].color = new Color(0f, 0f, 1f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                        crashArea[i, j] = false;
                    }
                    // (247, 781), (581, 718)
                    else if ((-63f) / 334f * (mappedX - 247f) - mappedY + 781f < 0) // 건물 오른쪽 밑
                    {
                        images[i, j].color = new Color(1f, 0f, 0f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                        crashArea[i, j] = false;
                    }
                    // (0, 758), (247, 781)
                    else if (23f / 247f * mappedX - mappedY + 758f < 0) // 건물 왼쪽 밑
                    {
                        images[i, j].color = new Color(1f, 0f, 0f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                        crashArea[i, j] = false;
                    }

                }
                else // 세로선 기준 오른쪽
                {
                    if (mappedY <= 595f) // 하늘
                    {
                        images[i, j].color = new Color(0f, 0f, 1f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                        crashArea[i, j] = false;
                    }
                    else // 바닥
                    {
                        images[i, j].color = new Color(1f, 0f, 0f, 0.5f);
                        currBackGroundDepth[i, j] = groundSkyDepth;
                        crashArea[i, j] = false;
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
                { // 탱크가 배경보다 앞에 있을 때
                    images[i, j].material = null;
                }
                else
                { // 가릴 때, 탱크가 건물 뒤에 있을 때
                    if (crashArea[i, j]) // 충돌 범위 && 탱크 위치가 건물의 맨앞 맨 뒤 사이일 때 충돌 조건임!  맨 앞을 11로 잡고 맨뒤를 14로 잡자
                    {
                        if (arr[i, j] >= buildingFrontDepth && arr[i, j] < buildingBackDepth)
                            Debug.Log("충돌!" + arr[i, j]);
                    }
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
