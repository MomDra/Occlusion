using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO.Ports;
using UnityEngine;

public class Serial : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM4", 115200, Parity.None, 8, StopBits.None);     //시리얼 포트 열어주기
    string recv_text = "";

    string tmp = "a";

    byte[] buffer;


    // Start is called before the first frame update
    void Start()
    {
        sp.Open();              //포트 열어줌
        sp.ReadTimeout = 100;
        sp.WriteTimeout = 100;


        buffer = Encoding.Default.GetBytes(tmp);

        for (int i = 0; i < buffer.Length; ++i)
        {
            Debug.Log(buffer[i]);
        }

        buffer = new byte[2];
        buffer[0] = 97;
        buffer[1] = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sp.Write(buffer, 0, 2);
        }

        if (sp.IsOpen)
        {
            try
            {
                recv_text = sp.ReadLine();
                Debug.Log(recv_text);
            }
            catch (System.Exception e)
            {
                print(e);
            }


        }

    }

    void foo()
    {
        recv_text = sp.ReadLine();

        Debug.Log(recv_text);
    }

}
