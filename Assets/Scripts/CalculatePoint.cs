using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePoint : MonoBehaviour
{
    [SerializeField]
    Transform cameraPoint;
    [SerializeField]
    Transform leftUpPoint;
    [SerializeField]
    Transform rightDownPoint;
    [SerializeField]
    Transform middleUpPoint;
    [SerializeField]
    Transform middleDownPoint;

    [SerializeField]
    DepthInfo depthInfo;

    float leftX;
    float rightX;
    float upY;
    float downY;

    bool calculated;
    int count;


    // Start is called before the first frame update
    void Start()
    {


        Debug.Log(depthInfo.rightDownImgPos.x + "abcd");
        Debug.Log(depthInfo.leftUpImgPos.x + "abcd");

        // z - z1 = d.z * t 식에서 t를 구하고 그 t값에 따라 x, y를 구하면 될듯
        // t = (z - z1) / d.z
        // x = d.x * t + x1

    }

    // Update is called once per frame
    void Update()
    {
        if (calculated == false)
        {
            rightX = depthInfo.rightDownImgPos.x + depthInfo.rightDownImgPos.x * 2 / (Setting.Instance.Width * 2 - 2);
            leftX = -rightX;
            upY = depthInfo.leftUpImgPos.y + (depthInfo.leftUpImgPos.y - depthInfo.rightDownImgPos.y) / (Setting.Instance.Height * 2 - 2);
            downY = depthInfo.rightDownImgPos.y - (depthInfo.leftUpImgPos.y - depthInfo.rightDownImgPos.y) / (Setting.Instance.Height * 2 - 2);

            if (depthInfo.rightDownImgPos.x != 0)
            {
                calculated = true;
            }
        }


        // Debug.Log(depthInfo.rightDownImgPos.x);
        // Debug.Log(depthInfo.leftUpImgPos.x);

        Vector3 d1 = leftUpPoint.position - cameraPoint.position;
        Vector3 d2 = rightDownPoint.position - cameraPoint.position;
        Vector3 d3 = middleUpPoint.position - cameraPoint.position;
        Vector3 d4 = middleDownPoint.position - cameraPoint.position;

        // Debug.DrawRay(cameraPoint.position, d1 * 1000f, Color.red);
        // Debug.DrawRay(cameraPoint.position, d2 * 1000f, Color.red);

        // 404.83에 들어가는 숫자는 Canvas의 Plane Distance에 따라 달라짐

        float t1 = (404.83f - leftUpPoint.position.z) / d1.z;
        float t2 = (404.83f - rightDownPoint.position.z) / d2.z;
        float t3 = (404.83f - middleUpPoint.position.z) / d3.z;
        float t4 = (404.83f - middleDownPoint.position.z) / d4.z;


        float x1f = d1.x * t1 + leftUpPoint.position.x;
        float x2f = d2.x * t2 + rightDownPoint.position.x;
        float x3f = d3.x * t3 + middleUpPoint.position.x;
        float x4f = d4.x * t4 + middleDownPoint.position.x;

        float y1f = d1.y * t1 + leftUpPoint.position.y;
        float y2f = d2.y * t2 + rightDownPoint.position.y;
        float y3f = d3.y * t3 + middleUpPoint.position.y;
        float y4f = d4.y * t4 + middleDownPoint.position.y;

        // Debug.Log($"xStart: {xStart}, xEnd: {xEnd}, yStart: {yStart}, yEnd: {yEnd}");

        int[] x = new int[4];
        int[] y = new int[4];

        x[0] = (int)((x1f - leftX) / (rightX * 2) * Setting.Instance.Width);
        x[1] = (int)((x2f - leftX) / (rightX * 2) * Setting.Instance.Width);
        x[2] = (int)((x3f - leftX) / (rightX * 2) * Setting.Instance.Width);
        x[3] = (int)((x4f - leftX) / (rightX * 2) * Setting.Instance.Width);

        y[0] = (int)((upY - y1f) / (upY - downY) * Setting.Instance.Width);
        y[1] = (int)((upY - y2f) / (upY - downY) * Setting.Instance.Width);
        y[2] = (int)((upY - y3f) / (upY - downY) * Setting.Instance.Width);
        y[3] = (int)((upY - y4f) / (upY - downY) * Setting.Instance.Width);
        // Debug.Log($"leftX: {leftX}, rightX: {rightX}, downY: {downY}, upY: {upY}");


        // Debug.Log($"x1: {x1}, y1: {y1}, x2: {x2}, y2: {y2}");

        int x1 = int.MaxValue;
        int x2 = int.MinValue;
        int y1 = int.MaxValue;
        int y2 = int.MinValue;

        for (int i = 0; i < 4; ++i)
        {
            if (x[i] > Setting.Instance.Width) x[i] = Setting.Instance.Width;
            else if (x[i] < 0) x[i] = 0;

            if (x[i] < x1) x1 = x[i];
            if (x[i] > x2) x2 = x[i];


            if (y[i] > Setting.Instance.Height) y[i] = Setting.Instance.Height;
            else if (y[i] < 0) y[i] = 0;

            if (y[i] < y1) y1 = y[i];
            if (y[i] > y2) y2 = y[i];
        }

        // Debug.Log($"x1: {x1}, y1: {y1}, x2: {x2}, y2: {y2}");

        depthInfo.SyncMasking(x1, y1, x2, y2);


        // float tmp = 26.43f * 2
    }
}
