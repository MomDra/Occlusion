using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    Serial serial;
    Rigidbody rigid;

    public float speed = 2f;
    public float rotateSpeed = 30f;

    int[] diff = new int[3];



    // Start is called before the first frame update
    void Start()
    {
        serial = GetComponent<Serial>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    // void LateUpdate()
    // {
    //     if (serial.curr[0] / 10000 > 3)
    //     {
    //         transform.Translate(Vector3.right * speed * Time.deltaTime);
    //     }
    //     else if (serial.curr[0] / 10000 < 3)
    //     {
    //         transform.Translate(Vector3.left * speed * Time.deltaTime);
    //     }

    //     if (serial.curr[1] / 10000 > 3)
    //     {
    //         //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    //         transform.Rotate(Vector3.up * speed * Time.deltaTime);
    //     }
    //     else if (serial.curr[1] / 10000 < 3)
    //     {
    //         // transform.Translate(Vector3.back * speed * Time.deltaTime);
    //         transform.Rotate(Vector3.down * speed * Time.deltaTime);

    //     }
    //     //rigidbody.MovePosition(rigidbody.position + new Vector3(diff[0] / 30, diff[1] / 30, diff[2] / 30) * Time.deltaTime);
    //     // rigidbody.MovePosition(rigidbody.position + new Vector3(serial.curr[0] / 1000, serial.curr[1] / 1000, serial.curr[2] / 1000) * Time.deltaTime);
    // }

    private void FixedUpdate()
    {
        if (serial.curr[0] / 10000 > 3)
        {
            rigid.MovePosition(rigid.position + transform.forward * speed * Time.deltaTime);
            // transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (serial.curr[0] / 10000 < 3)
        {
            rigid.MovePosition(rigid.position + transform.forward * -speed * Time.deltaTime);
            // transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (serial.curr[1] / 10000 > 3)
        {
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            // rigid.rotation = Quaternion.AngleAxis(rotateSpeed, Vector3.up);
        }
        else if (serial.curr[1] / 10000 < 3)
        {
            // transform.Translate(Vector3.back * speed * Time.deltaTime);
            transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
            // rigid.rotation = Quaternion.AngleAxis(-rotateSpeed, Vector3.up);

        }
    }
}