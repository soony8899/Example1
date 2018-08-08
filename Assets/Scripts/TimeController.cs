using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Text m_my;
    public Text s_m_my;
    private FadeController fade;

    // Use this for initialization
    void Start()
    {
        Debug.Log(DateTime.Now.ToLongTimeString());
        fade = new FadeController(m_my, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        string timeText = DateTime.Now.ToLongTimeString();
        
        string input = TimeCalculator(timeText);
        m_my.text = input;
        s_m_my.text = input;
    }

    string TimeCalculator(string input)
    {
        string[] split_str = input.Split(':');
        string[] split_str2;
        string retVal = "";
        for (int i = 0; i < split_str.Length; i++)
        {
            //  Debug.Log(split_str[i]);



        }
        split_str2 = split_str[2].Split(' ');
        for (int j = 0; j < split_str2.Length; j++)
        {
            //  Debug.Log("2 : " + split_str2[j]);
        }
        if (split_str2[1] == "PM")
        {
            if (split_str[0] != "12")
            {
                int ts = Int32.Parse(split_str[0]);
                ts += 12;
                retVal = ts.ToString() + ":" + split_str[1] + ":" + split_str2[0];
            }


        }
        else
        {
            if (split_str[0] == "12")
            {
                split_str[0] = "00";
            }
            retVal = split_str[0] + ":" + split_str[1] + ":" + split_str2[0];
        }



        return retVal;
    }

}
