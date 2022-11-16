using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRay : MonoBehaviour
{
    Transform[] gameObjects;
    int[,] objectDepth = new int[10, 10];

    float halfWidth;
    float halfHeight;

    float imgWidth;
    float imgHeight;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GetComponentsInChildren<Transform>();

        imgWidth = GetComponent<RectTransform>().sizeDelta.x;
        imgHeight = GetComponent<RectTransform>().sizeDelta.y;
    }

    // Update is called once per frame
    void Update()
    {
        halfWidth = Screen.width / 2;
        halfHeight = Screen.height / 2;

        Vector3 rayPosition = new Vector3(halfWidth - imgWidth / 2 + imgWidth / 10 / 2, halfHeight - imgWidth / 2 + imgWidth / 10 / 2, 0f);

        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                objectDepth[i, j] = 100; // 기본 100으로 초기화

                rayPosition = new Vector3(halfWidth - imgWidth / 2 + imgWidth / 10 / 2 + j * imgWidth / 10, halfHeight + imgHeight / 2 - imgHeight / 10 / 2 - i * imgHeight / 10);
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
        GetComponent<DepthInfo>().SyncMasking(objectDepth);
    }

}
