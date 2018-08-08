using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine.UI;
using System;
using UnityEngine.Video;



public class OpenPoseController : MonoBehaviour
{

    private bool Debug_UI = false;
    private bool Motion = true;
    private bool Debug_disp = true;
    //Declair Dll Functions 
    [DllImport("1_user_asynchronous_output")]
    private static extern bool get_ret();

    [DllImport("1_user_asynchronous_output", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string get_debug_str();

    [DllImport("1_user_asynchronous_output")]
    private static extern int Unity_Call();

    /*  [DllImport("6_user_asynchronous_output")]
      private static extern int Unity_stop();*/

    [DllImport("1_user_asynchronous_output")]
    private static extern int get_index();

    [DllImport("1_user_asynchronous_output")]
    private static extern void set_index(int num);

    [DllImport("1_user_asynchronous_output")]
    private static extern bool get_trust();

    [DllImport("1_user_asynchronous_output")]
    private static extern float get_float();

    [DllImport("1_user_asynchronous_output")]
    private static extern bool get_motion_flag();

    [DllImport("1_user_asynchronous_output")]
    private static extern void set_motion_flag(bool flag);

    [DllImport("1_user_asynchronous_output")]
    private static extern void set_debug(bool flag);


    public Text Debug_Text;
    public Text Debug_Text2;

    //global var
    //public
    public Text idle_Text;
    public Text Start_Text;

    public Text tip_Text;
    public Text great_Text;

    public Image tree;
    public Image triangle;
    public Image Start_image;


    //static
    public static bool TipFlag;
    public static int number;
    public static int play_time;
    public static bool next_flag;

    //for video
    public RawImage image;
    public Text ex_name;
    public Text ex_time;
    public Text ex_time_fix;
    public Text ex_r_time; // remain time
    public Text ex_r_time_fix;
    public Text time;
    public Text time_x;

    public VideoClip videoToPlay;
    public VideoClip videoToPlay2;
    public VideoClip videoToPlay3;
    public VideoClip videoToPlay4;
    public VideoClip[] videoToPlays;
    public AudioClip[] audioClips;
    public RenderTexture rt;

    //private
    private FadeController fade_tip;
    private FadeController fade_idle;
    private FadeController fade_start;
    private FadeController fade_great;
    private FadeController fade_image;


    private TipController tip;
    private VideoController vc;
    private VideoController vc2;
    private VRecogController recog;


    private int index;
    private bool trust;
    private int tmp_idx;
    private bool idx_flag;
    private bool motionFlag;
    private bool ret_match;
    private bool retc;
    private string ds;

    //thread class
    [System.Serializable]
    public class threadHandle
    {
        public void UnityCall()
        {

            Debug.Log("Finish : " + Unity_Call());
        }
    }
    Thread thread;

    // Use this for initialization
    void Start()
    {
        recog = new VRecogController();
        recog.start();

        init(); // initialize 

        if (!Debug_UI) // not play thread
        {
            try
            {
                threadHandle _handle = new threadHandle();
                thread = new Thread(_handle.UnityCall);
                thread.Start();

            }
            catch (Exception e)
            {
                Debug.Log("error : " + e.Message);
            }

        }

        index = 0;

    }


    void Update()
    {
        if (!Debug_UI)
        {
            getDataFromDLL();
        }
        Debug.Log("number : " + number);
        if (index != 0)
        {
            set_index(number);
        }
        set_index(5);
        playUI();

        if (motionFlag)
        {
            checkIdx();
        }
        else
        {
            videoControl();
        }
    }

    private void getDataFromDLL()
    {
        ds = "";
        retc = false;


        ds = get_debug_str();
        retc = get_ret();
        index = get_index();
        trust = get_trust();
        motionFlag = get_motion_flag();
        Debug_Text.text = ds;
        Debug_Text2.text = get_float().ToString();
       // Debug.Log("motion : " + get_motion_flag());
        //Debug.Log("angle : " + get_float());
       /// Debug.Log("idx : " + index);
     //   Debug.Log("ret : " + retc);

    }

    private void playUI()
    {
        Debug.Log("ret!! : " + ret_match);
        if (ret_match)
        {
            if (!fade_great.isRunning)
            {
               // Debug.Log("Run");
                tip_Text.text = ""; // tip disable
                great_Text.text = "잘하셨습니다.";
                StartCoroutine(fade_great.Fade());
            }
            //TipFlag = false;
        }
        if (fade_great.isRunning)
        {
            Debug.Log("Run");
            great_Text.text = "잘하셨습니다.";
        }
        else
            great_Text.text = "";


        ret_match = matching(ds); // matching result
        if (ret_match)
        {
            next_flag = true;
        }
        Debug.Log("next flag : " + next_flag);
        //tip
        tip.InsertData(ds, number);
       

        
        if (TipFlag)
        {
            if (!fade_tip.isRunning)
            {
                tip_Text.text = tip.Make_Tip();
                StartCoroutine(fade_tip.Fade_wait());
            }
        }
        else
        {
            tip_Text.text = "";

        }

        if (!fade_idle.isRunning && !trust)
        {
            StartCoroutine(fade_idle.Fade());
        }
        if (!fade_start.isRunning)
        {
            StartCoroutine(fade_start.Fade());
        }

        if (!fade_image.isRunning)
        {
            StartCoroutine(fade_image.Fade_img());
        }
    }


    private bool matching(string input)
    {
        if (input != null)
        {
            string[] input_sub = input.Split('/');
            int count = 0;
            for (int i = 0; i < input_sub.Length; i++)
            {
                if (input_sub[i] == "0")
                {
                    count++;
                }
            }
            if (number == 1 || number == 2)
            {
                if (count == 8)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (number == 3 || number == 4)
            {
                if (count == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(number == 5 || number == 6)
            {
                if (count == 3)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            else if(number == 7 || number == 8)
            {
                if(count == 3)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }else
        {
            return false;
        }
    }

    private void init()
    {
        trust = false;
        TipFlag = true;
        idx_flag = false;
        number = 1;

        ex_name.text = "";
        ex_time.text = "";
        ex_r_time.text = "";
        ex_time_fix.gameObject.SetActive(false);
        time.gameObject.SetActive(true);
        time_x.gameObject.SetActive(false);
        ex_r_time_fix.gameObject.SetActive(false);


        tree.gameObject.SetActive(false);
        triangle.gameObject.SetActive(false);
        // great_Text.text = "";

        //setting Flags
        set_debug(Debug_disp);
        set_motion_flag(Motion);

        fade_tip = new FadeController(tip_Text, 1.0f);
        fade_idle = new FadeController(idle_Text, 1.0f);
        fade_start = new FadeController(Start_Text, 1.0f);
        fade_great = new FadeController(great_Text, 1.0f);
        fade_image = new FadeController(Start_image, 1.0f);
        tip = new TipController(image, audioClips);

    }


    private void checkIdx()
    {
        int idx = get_index();

        if (idx_flag)
        {
            if (idx == 0)
            {

                vc.StopVideo();
                number = 1;
                TipFlag = true;
                next_flag = false;
                ex_name.text = "";
                ex_time.text = "";
                ex_r_time.text = "";
                great_Text.text = "";
                rt.Release();
                ex_time_fix.gameObject.SetActive(false);
                time.gameObject.SetActive(true);
                time_x.gameObject.SetActive(false);
                ex_r_time_fix.gameObject.SetActive(false);

                Start_image.gameObject.SetActive(true);
                if (Debug_UI)
                {
                    index = 0;
                }
                idx_flag = false;
            }

            if (!vc.isPlaying)
            {
                if (number == 3)
                {
                    ex_name.text = "";
                    ex_time.text = "";
                    ex_r_time.text = "";
                    if (idx == 3)
                    {
                        image.transform.position = new Vector3(522, image.transform.position.y, image.transform.position.z);

                        vc.StopVideo();
                        rt.Release();
                        vc = new VideoController(image, tree, triangle, ex_name, ex_time, ex_r_time, videoToPlay2, rt);

                        great_Text.text = "";
                        Start_image.gameObject.SetActive(false);
                        tip_Text.text = "";
                        StartCoroutine(vc.playVideo());
                        ex_time_fix.gameObject.SetActive(true);
                        ex_r_time_fix.gameObject.SetActive(true);
                        time.gameObject.SetActive(false);
                        time_x.gameObject.SetActive(true);
                      //  Debug.Log("2번");
                    }
                }
                else if(number == 5)
                {

                    ex_name.text = "";
                    ex_time.text = "";
                    ex_r_time.text = "";

                    vc.StopVideo();
                    rt.Release();
                    vc = new VideoController(image, tree, triangle, ex_name, ex_time, ex_r_time, videoToPlay3, rt);

                    great_Text.text = "";
                    Start_image.gameObject.SetActive(false);
                    tip_Text.text = "";
                    StartCoroutine(vc.playVideo());
                    ex_time_fix.gameObject.SetActive(true);
                    ex_r_time_fix.gameObject.SetActive(true);
                    time.gameObject.SetActive(false);
                    time_x.gameObject.SetActive(true);
                 //   Debug.Log("2번");
                }
                else if(number == 7)
                {
                    ex_name.text = "";
                    ex_time.text = "";
                    ex_r_time.text = "";

                    vc.StopVideo();
                    rt.Release();
                    vc = new VideoController(image, tree, triangle, ex_name, ex_time, ex_r_time, videoToPlay4, rt);

                    great_Text.text = "";
                    Start_image.gameObject.SetActive(false);
                    tip_Text.text = "";
                    StartCoroutine(vc.playVideo());
                    ex_time_fix.gameObject.SetActive(true);
                    ex_r_time_fix.gameObject.SetActive(true);
                    time.gameObject.SetActive(false);
                    time_x.gameObject.SetActive(true);
                 //   Debug.Log("2번");
                }
                else
                {
                    //   Debug.Log("esc");
                    next_flag = false;
                    number = 1;
                    vc.StopVideo();
                    if (!Debug_UI)
                    {
                        set_index(0);
                    }
                    else
                        index = 0;
                    ex_name.text = "";
                    ex_time.text = "";
                    ex_r_time.text = "";
                    rt.Release();
                    great_Text.text = "";
                    ex_time_fix.gameObject.SetActive(false);
                    ex_r_time_fix.gameObject.SetActive(false);
                    time.gameObject.SetActive(true);
                    time_x.gameObject.SetActive(false);

                    Start_image.gameObject.SetActive(true);
                }
            }


        }
        if (idx == 1)
        {
            idx_flag = true;
            TipFlag = true;
            //next_flag = false;
            //rt.Release();
            image.transform.position = new Vector3(592, image.transform.position.y, image.transform.position.z);

            vc = new VideoController(image, tree, triangle, ex_name, ex_time, ex_r_time, videoToPlay, rt);


            // Start_Text.text = "";
          //  great_Text.text = "";
            Start_image.gameObject.SetActive(false);
            StartCoroutine(vc.playVideo());//1번 영상 재생.
            ex_time_fix.gameObject.SetActive(true);
            ex_r_time_fix.gameObject.SetActive(true);
            time.gameObject.SetActive(false);
            time_x.gameObject.SetActive(true);
        }

    }



    private void videoControl()
    {
        if (index == 0 && Input.GetKeyUp(KeyCode.Return) /*음성인식 함수*/)
        {
            idx_flag = true;
            TipFlag = true;
            if (!Debug_UI)
            {
                set_index(1);

            }
            else
            {
                index = 1;
            }
            if (vc != null)
                vc.StopVideo();
            rt.Release();
            image.transform.position = new Vector3(592, image.transform.position.y, image.transform.position.z);

            vc = new VideoController(image, tree, triangle, ex_name, ex_time, ex_r_time, videoToPlay, rt);


            //Start_Text.text = "";
            Start_image.gameObject.SetActive(false);
            StartCoroutine(vc.playVideo());//1번 영상 재생.
            ex_time_fix.gameObject.SetActive(true);
            ex_r_time_fix.gameObject.SetActive(true);
            time.gameObject.SetActive(false);
            time_x.gameObject.SetActive(true);

        }

        if (index == 1 && Input.GetKey(KeyCode.Space) /*음성인식 함수*/)
        {
            // 2번 자세 play Using VideoController

            idx_flag = true;
            TipFlag = true;

            image.transform.position = new Vector3(522, image.transform.position.y, image.transform.position.z);
            Debug.Log("position x : " + image.transform.position.x);
            vc.StopVideo();
            rt.Release();
            vc = new VideoController(image, tree, triangle, ex_name, ex_time, ex_r_time, videoToPlay2, rt);
            if (!Debug_UI)
            {
                set_index(3);

            }
            else
                index = 3;
            //Start_Text.text = "";
            Start_image.gameObject.SetActive(false);
            StartCoroutine(vc.playVideo());
            ex_time_fix.gameObject.SetActive(true);
            ex_r_time_fix.gameObject.SetActive(true);
            time.gameObject.SetActive(false);
            time_x.gameObject.SetActive(true);
            Debug.Log("2번");
        }

        if (index == 3 && Input.GetKey(KeyCode.Alpha3) /*음성인식 함수*/)
        {
            // 2번 자세 play Using VideoController

            idx_flag = true;
            TipFlag = true;
           
            image.transform.position = new Vector3(572, 1052, image.transform.position.z);
            Debug.Log("position x : " + image.transform.position.x);
            Debug.Log("ccccc : " + image.transform.position.y);
            vc.StopVideo();
            rt.Release();
            vc = new VideoController(image, tree, triangle, ex_name, ex_time, ex_r_time, videoToPlay3, rt);
            if (!Debug_UI)
            {
                set_index(5);

            }
            else
                index = 5;
            //Start_Text.text = "";
            Start_image.gameObject.SetActive(false);
            StartCoroutine(vc.playVideo());
            ex_time_fix.gameObject.SetActive(true);
            ex_r_time_fix.gameObject.SetActive(true);
            time.gameObject.SetActive(false);
            time_x.gameObject.SetActive(true);
            Debug.Log("2번");
        }

        if (index == 5 && Input.GetKey(KeyCode.Alpha4) /*음성인식 함수*/)
        {
            // 2번 자세 play Using VideoController

            idx_flag = true;
            TipFlag = true;

            image.transform.position = new Vector3(522, image.transform.position.y, image.transform.position.z);
            Debug.Log("position x : " + image.transform.position.x);
            vc.StopVideo();
            rt.Release();
            vc = new VideoController(image, tree, triangle, ex_name, ex_time, ex_r_time, videoToPlay4, rt);
            if (!Debug_UI)
            {
                set_index(7);

            }
            else
                index = 7;
            //Start_Text.text = "";
            Start_image.gameObject.SetActive(false);
            StartCoroutine(vc.playVideo());
            ex_time_fix.gameObject.SetActive(true);
            ex_r_time_fix.gameObject.SetActive(true);
            time.gameObject.SetActive(false);
            time_x.gameObject.SetActive(true);
            Debug.Log("2번");
        }


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("esc");
            vc.StopVideo();
            if (!Debug_UI)
            {
                set_index(0);
            }
            else
                index = 0;
            ex_name.text = "";
            ex_time.text = "";
            ex_r_time.text = "";
            rt.Release();
            //vc = new VideoController(image, tree, triangle, ex_name, ex_time, videoToPlays[1]);

            ex_time_fix.gameObject.SetActive(false);
            ex_r_time_fix.gameObject.SetActive(false); 
            //Start_Text.text = "운동 시작하기";
            time.gameObject.SetActive(true);
            time_x.gameObject.SetActive(false);
            Start_image.gameObject.SetActive(true);

        }
    }

}
