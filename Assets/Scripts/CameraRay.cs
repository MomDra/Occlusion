using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRay : MonoBehaviour
{
    Transform[] gameObjects;
    int[,] objectDepth;

    float halfWidth;
    float halfHeight;

    float imgWidth;
    float imgHeight;

    int width;
    int height;

    DepthInfo depthInfo;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GetComponentsInChildren<Transform>();

        imgWidth = GetComponent<RectTransform>().sizeDelta.x;
        imgHeight = GetComponent<RectTransform>().sizeDelta.y;

        width = Setting.Instance.Width;
        height = Setting.Instance.Height;
        objectDepth = new int[height, width];

        depthInfo = GetComponent<DepthInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        halfWidth = Screen.width / 2;
        halfHeight = Screen.height / 2;

        Vector3 rayPosition;
        // new Vector3(halfWidth - imgWidth / 2 + imgWidth / width / 2, halfHeight - imgWidth / 2 + imgWidth / height / 2, 0f);

        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                objectDepth[i, j] = 100; // 기본 100으로 초기화

                rayPosition = new Vector3(halfWidth - imgWidth / 2 + imgWidth / width / 2 + j * imgWidth / width, halfHeight + imgHeight / 2 - imgHeight / height / 2 - i * imgHeight / height);
                Ray cameraRay = Camera.main.ScreenPointToRay(rayPosition);

                RaycastHit hit;
                if (Physics.Raycast(cameraRay, out hit, 100f))
                {
                    objectDepth[i, j] = (int)(hit.transform.position.z - Camera.main.transform.position.z);

                    Debug.Log(hit.transform.position.z - Camera.main.transform.position.z);
                    Debug.DrawRay(cameraRay.origin, cameraRay.direction * 100f, Color.red);
                }
            }
        }


        // GetComponent<DepthInfo>().SyncUITrueFalse(objectDepth);
        // depthInfo.SyncMasking(objectDepth);
    }

}
