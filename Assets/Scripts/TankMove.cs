using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rigid.position.x < 9.5f)
        {
            rigid.MovePosition(rigid.position + Vector3.right * Time.deltaTime);
        }
        else
        {
            Vector3 pos = rigid.position;
            pos.x = -9.5f;
            rigid.position = pos;
        }
    }
}
