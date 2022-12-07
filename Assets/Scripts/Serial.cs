using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class Serial : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 115200, Parity.None, 8, StopBits.None);     //시리얼 포트 열어주기
    string recv_text = "";

    public int[] prev = new int[3];
    public int[] curr = new int[3];


    // Start is called before the first frame update
    void Start()
    {
        sp.Open();              //포트 열어줌
        sp.ReadTimeout = 100;

        if (sp.IsOpen)          //포트 열리면
        {
            try
            {
                recv_text = sp.ReadLine();

            }
            catch (System.Exception e)
            {
                print(e);
                // sp.Close();
                throw;

            }
        }

        string[] split = recv_text.Split(',');



        for (int i = 0; i < 3; ++i)
        {
            // prev[i] = int.Parse(split[i]);
            curr[i] = int.Parse(split[i]);
        }

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
                // sp.Close();
                throw;

            }
        }

        // Debug.LogWarning(recv_text);
        set(recv_text);


    }



    void set(string text)
    {
        string[] split = text.Split(',');

        for (int i = 0; i < 3; ++i)
        {
            //  prev[i] = curr[i];
            // Debug.Log(split[i]);
            curr[i] = int.Parse(split[i]);
        }
    }

    public void U()
    {
        sp.Write("u");
        Debug.Log("UUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
    }

    public void D()
    {
        sp.Write("d");
        Debug.Log("DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
    }
}