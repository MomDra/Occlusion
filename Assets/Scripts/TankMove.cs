using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    Rigidbody rigid;
    float horizontalInput;
    float verticalInput;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 moveVec = new Vector3(horizontalInput * 5, 0f, verticalInput);
        rigid.MovePosition(rigid.position + transform.forward * verticalInput * 5 * Time.deltaTime);

        transform.Rotate(Vector3.up, horizontalInput * 3f);


        // if (rigid.position.x < 9.5f)
        // {
        //     rigid.MovePosition(rigid.position + Vector3.right * 5 * Time.deltaTime);
        // }
        // else
        // {
        //     Vector3 pos = rigid.position;
        //     pos.x = -9.5f;
        //     rigid.position = pos;
        // }
    }
}
