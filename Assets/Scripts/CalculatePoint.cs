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
    DepthInfo depthInfo;

    float leftX;
    float rightX;
    float upY;
    float downY;


    // Start is called before the first frame update
    void Start()
    {


        Debug.Log(depthInfo.rightDownImgPos.x);
        Debug.Log(depthInfo.leftUpImgPos.x);

        // z - z1 = d.z * t 식에서 t를 구하고 그 t값에 따라 x, y를 구하면 될듯
        // t = (z - z1) / d.z
        // x = d.x * t + x1

    }

    // Update is called once per frame
    void Update()
    {
        rightX = depthInfo.rightDownImgPos.x + depthInfo.rightDownImgPos.x * 2 / (Setting.Instance.Width * 2 - 2);
        leftX = -rightX;
        upY = depthInfo.leftUpImgPos.y + (depthInfo.leftUpImgPos.y - depthInfo.rightDownImgPos.y) / (Setting.Instance.Height * 2 - 2);
        downY = depthInfo.rightDownImgPos.y - (depthInfo.leftUpImgPos.y - depthInfo.rightDownImgPos.y) / (Setting.Instance.Height * 2 - 2);

        Debug.Log(depthInfo.rightDownImgPos.x);
        Debug.Log(depthInfo.leftUpImgPos.x);

        Vector3 d1 = leftUpPoint.position - cameraPoint.position;
        Vector3 d2 = rightDownPoint.position - cameraPoint.position;

        // 404.83에 들어가는 숫자는 Canvas의 Plane Distance에 따라 달라짐

        float t1 = (404.83f - leftUpPoint.position.z) / d1.z;
        float t2 = (404.83f - rightDownPoint.position.z) / d2.z;

        float xStart = d1.x * t1 + leftUpPoint.position.x;
        float xEnd = d2.x * t2 + rightDownPoint.position.x;
        float yStart = d1.y * t1 + leftUpPoint.position.y;
        float yEnd = d2.y * t2 + rightDownPoint.position.y;

        Debug.Log($"xStart: {xStart}, xEnd: {xEnd}, yStart: {yStart}, yEnd: {yEnd}");

        int x1 = (int)((xStart - leftX) / (rightX * 2) * Setting.Instance.Width);
        int x2 = (int)((xEnd - leftX) / (rightX * 2) * Setting.Instance.Width);
        int y1 = (int)((upY - yStart) / (upY - downY) * Setting.Instance.Width);
        int y2 = (int)((upY - yEnd) / (upY - downY) * Setting.Instance.Width);
        Debug.Log($"leftX: {leftX}, rightX: {rightX}, downY: {downY}, upY: {upY}");


        Debug.Log($"x1: {x1}, y1: {y1}, x2: {x2}, y2: {y2}");


        if (x1 > Setting.Instance.Width) x1 = Setting.Instance.Width;
        else if (x1 < 0) x1 = 0;

        if (y1 > Setting.Instance.Height) y1 = Setting.Instance.Height;
        else if (y1 < 0) y1 = 0;

        if (x2 > Setting.Instance.Width) x2 = Setting.Instance.Width;
        else if (x2 < 0) x2 = 0;

        if (y2 > Setting.Instance.Height) y2 = Setting.Instance.Height;
        else if (y2 < 0) y2 = 0;

        depthInfo.SyncMasking(x1, y1, x2, y2);


        // float tmp = 26.43f * 2
    }
}
