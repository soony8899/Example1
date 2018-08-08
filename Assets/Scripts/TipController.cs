using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipController
{
    private RawImage back;
    private AudioSource audiosource;
    private AudioClip[] source;
    private int index;
    private string rawData;
    private string Tip;
    private string[] name = { "오른쪽 팔", "오른손", "왼쪽 팔", "왼손", "오른 발", "왼 발", "오른쪽 무릎", "왼쪽 무릎" };

    public TipController(RawImage back, AudioClip[] source)
    {
        this.back = back;
        this.source = source;
        if (this.back.GetComponent<AudioSource>() == null)
        {
            this.back.gameObject.AddComponent<AudioSource>();
        }
        audiosource = this.back.GetComponent<AudioSource>();
        audiosource.playOnAwake = false;
        audiosource.Pause();
        Debug.Log("initializing TipController...");
        Debug.Log("source Size : " + source.Length);

    }


    public void InsertData(string data, int inputIndex)
    {
        this.rawData = data;
        this.index = inputIndex;
    }

    public string Make_Tip()
    {
        if (rawData != null)
        {
            string[] rawData_sub = rawData.Split('/');
            Tip = "";

            if (index ==1  || index == 2)
            {
                for (int i = 0; i < rawData_sub.Length; i++)
                {

                    if (rawData_sub[i] == "1")
                    {

                        switch (index)
                        {
                            case 0:
                                break;
                            case 1:
                                if (i != 0 && i != 2 && i != 4 && i != 7)
                                {
                                    Tip = "Tip : " + name[i] + "을 더 올리세요.";
                                }
                                else if (i == 7)
                                {
                                    Tip = "Tip : " + name[i] + "을 더 접으세요";
                                }
                                else Tip = "";
                                playTTS(i, rawData_sub[i]);
                                break;
                            case 2:
                                if (i != 0 && i != 2 && i != 5 && i != 6)
                                {
                                    Tip = "Tip : " + name[i] + "을 더 올리세요.";
                                }
                                else if (i == 6)
                                {
                                    Tip = "Tip : " + name[i] + "을 더 접으세요";
                                }
                                else Tip = "";
                                playTTS(i, rawData_sub[i]);
                                break;
                            case 3:

                                break;
                            case 4:
                                break;
                        }
                    }
                    else if (rawData_sub[i] == "-1")
                    {
                        switch (index)
                        {
                            case 0:
                                break;
                            case 1:
                                if (i != 0 && i != 2 && i != 4 && i != 7)
                                {
                                    Tip = "Tip : " + name[i] + "을 더 내리세요.";
                                }
                                else if (i == 7)
                                {
                                    Tip = "Tip : " + name[i] + "을 더 접으세요";
                                }
                                else Tip = "";
                                playTTS(i, rawData_sub[i]);
                                break;
                            case 2:

                                if (i != 0 && i != 2 && i != 5 && i != 6)
                                {
                                    Tip = "Tip : " + name[i] + "을 더 내리세요.";
                                }
                                else if (i == 6)
                                {
                                    Tip = "Tip : " + name[i] + "을 더 접으세요";
                                }
                                else Tip = "";
                                playTTS(i, rawData_sub[i]);
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                        }
                    }
                }
            }
            else if(index == 3 || index == 4)
            {
                for (int i = 0; i < rawData_sub.Length - 1; i++)
                {
                    if (rawData_sub[i] == "1")
                    {
                        switch (i)
                        {
                            case 0:
                                Tip = "Tip : 왼팔과 오른팔을 정확히 겹치세요.";
                                audiosource.clip = source[12];
                                audiosource.Play();
                                break;
                            case 1:
                                Tip = "Tip : 왼팔과 오른팔을 정확히 겹치세요.";
                                audiosource.clip = source[12];
                                audiosource.Play();
                                break;
                            case 2:
                                Tip = "Tip : 다리를 어깨넓이로 벌려주세요.";
                                audiosource.clip = source[11];
                                audiosource.Play();
                                break;
                            case 3:
                                Tip = "Tip : 엉덩이를 더 내려주세요.";
                                audiosource.clip = source[10];
                                audiosource.Play();
                                break;

                        }
                    }
                    Debug.Log(i + " : " + rawData_sub[i]);
                }
            }
            else if(index == 5 || index  == 6)
            {
                for (int i = 0; i < rawData_sub.Length - 1; i++)
                {
                    if (rawData_sub[i] == "1")
                    {
                        switch (i)
                        {
                            case 0:
                                Tip = "Tip : 왼팔과 오른팔을 정확히 겹치세요.";
                                audiosource.clip = source[12];
                                audiosource.Play();
                                break;
                            case 1:
                                Tip = "Tip : 왼팔과 오른팔을 정확히 겹치세요.";
                                audiosource.clip = source[12];
                                audiosource.Play();
                                break;
                            case 2:
                                Tip = "Tip : 무릎을 더 굽혀주세요.";
                                audiosource.clip = source[11];
                                audiosource.Play();
                                break;

                        }
                    }
                    Debug.Log(i + " : " + rawData_sub[i]);
                }
            }
            else if (index == 7 || index == 8)
            {
                for (int i = 0; i < rawData_sub.Length - 1; i++)
                {
                    if (rawData_sub[i] == "1")
                    {
                        switch (i)
                        {
                            case 0:
                                Tip = "Tip : 다리를 어깨넓이로 벌려주세요.";
                                audiosource.clip = source[11];
                                audiosource.Play();
                                break;
                            case 1:
                                Tip = "Tip : 양손을 머리위로 정확히 올려주세요.";
                                audiosource.clip = source[12];
                                audiosource.Play();
                                break;
                            case 2:
                                if (index == 7)
                                {
                                    Tip = "Tip : 상체를 왼쪽으로 기울여 주세요.";
                                    audiosource.clip = source[11];
                                    audiosource.Play();
                                }else if(index == 8)
                                {
                                    Tip = "Tip : 상체를 오른쪽으로 기울여 주세요.";
                                    audiosource.clip = source[11];
                                    audiosource.Play();
                                }
                                break;
                        }
                    }
                    Debug.Log(i + " : " + rawData_sub[i]);
                }
            }
        }

        return Tip;
    }

    private void playTTS(int num, string check)
    {
        if(check == "1")
        {
            switch(num)
            {
                case 1:
                    audiosource.clip = source[4];
                    audiosource.Play();
                    break;
                case 3:
                    audiosource.clip = source[6];
                    audiosource.Play();
                    break;
                case 4:
                    audiosource.clip = source[0];
                    audiosource.Play();
                    break;
                case 5:
                    audiosource.clip = source[2];
                    audiosource.Play();
                    break;
                case 6:
                    audiosource.clip = source[8];
                    audiosource.Play();
                    break;
                case 7:
                    audiosource.clip = source[9];
                    audiosource.Play();
                    break;
            }
        }else if(check == "-1")
        {
            switch (num)
            {
                case 1:
                    audiosource.clip = source[5];
                    audiosource.Play();
                    break;
                case 3:
                    audiosource.clip = source[7];
                    audiosource.Play();
                    break;
                case 4:
                    audiosource.clip = source[1];
                    audiosource.Play();
                    break;
                case 5:
                    audiosource.clip = source[3];
                    audiosource.Play();
                    break;
                case 6:
                    audiosource.clip = source[8];
                    audiosource.Play();
                    break;
                case 7:
                    audiosource.clip = source[9];
                    audiosource.Play();
                    break;
            }
        }
    }
}
