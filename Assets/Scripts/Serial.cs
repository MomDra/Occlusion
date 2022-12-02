using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class Serial : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM5", 115200, Parity.None, 8, StopBits.None);     //시리얼 포트 열어주기
    string recv_text = "";


    // Start is called before the first frame update
    void Start()
    {
        sp.Open();              //포트 열어줌
        sp.ReadTimeout = 100;



    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)          //포트 열리면
        {
            try
            {
                recv_text = sp.ReadLine();
            }
            catch (System.Exception e)
            {
                print(e);
                sp.Close();
                throw;

            }
        }

        Debug.LogWarning(recv_text);
    }
}
