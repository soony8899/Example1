using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class StreamVideo : MonoBehaviour {
    public static int number;
    public static bool endFlag;

    public RawImage image;
    public Image tree;
    public Image triangle;
    public Text Item;

    private VideoPlayer videoPlayer;
    public Text str_name;
    public Text str_time;
    public Text str_start;
    public Text str_help;
    public Text str_time_fix;
    private VideoSource videoSource;
    public VideoClip videoToPlay;
    public VideoClip videoToPlay2;
    private AudioSource audiosource;

    private GameObject str_Tip;
    private bool check;
    private bool allClear;
    private bool firstCheck = false;
    private bool isRunning = true;


    
    private bool VideoPlayFlag = false;
    private float first_check = 0f;
    
    // Use this for initialization
    void Start() {
        Application.runInBackground = true;
        str_Tip = GameObject.Find("str_fix");
        check = true;
        allClear = false;
        StartCoroutine("PlayFade");
        first_check = image.transform.position.x;
        str_time_fix.gameObject.SetActive(false);
	}
	IEnumerator playVideo()
    {
        //OpenPoseController.tflag = true;
       // str_time_fix.gameObject.SetActive(true);
        str_time_fix.gameObject.SetActive(true);
        VideoPlayFlag = true;
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        audiosource = gameObject.AddComponent<AudioSource>();
        videoPlayer.waitForFirstFrame = false;
        videoPlayer.playOnAwake = false;
        audiosource.playOnAwake = false;
        audiosource.Pause();
        
        videoPlayer.source = VideoSource.VideoClip;
        if (first_check != image.transform.position.x)
        {
            image.transform.position = new Vector3(586.2969f, image.transform.position.y, image.transform.position.z);

        }

      
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audiosource);
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        WaitForSeconds waittime = new WaitForSeconds(1);

        while(!videoPlayer.isPrepared)
        {
            Debug.Log("preparing video");
            yield return waittime;

            break;
        }

        image.texture = videoPlayer.texture;
        str_name.text = videoToPlay.name;


        videoPlayer.Play();
        image.color = Color.white;
        audiosource.Play();
       
        while(videoPlayer.isPlaying)
        {
         
            str_time.text =  Mathf.FloorToInt((float)videoPlayer.time).ToString() + "초";
            if (Mathf.FloorToInt((float)videoPlayer.time) == 25)
            {
              //  OpenPoseController.TipFlag = true;
            }
            yield return null;
        }
        if(number == 1 || number == 2)
        {
            tree.gameObject.SetActive(true);
            str_time.text = "";
            str_time_fix.gameObject.SetActive(false);
        }
        else
        {
            VideoPlayFlag = false;
            allClear = true;
        }
        Destroy(videoPlayer);
        
    }

    void stopVideo()
    {
        
        if (firstCheck)
        {
       
            Destroy(videoPlayer);
          
           
            Destroy(audiosource);
        }
            
        
    }
    IEnumerator playVideo2()
    {
        //str_time_fix.gameObject.SetActive(true);
        str_time_fix.gameObject.SetActive(true);
       
        VideoPlayFlag = true;
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        audiosource = gameObject.AddComponent<AudioSource>();
        videoPlayer.waitForFirstFrame = false;
        videoPlayer.playOnAwake = false;
        audiosource.playOnAwake = false;
        audiosource.Pause();

        videoPlayer.source = VideoSource.VideoClip;


        //  videoPlayer.source = VideoSource.Url;
        // videoPlayer.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audiosource);
        videoPlayer.clip = videoToPlay2;
        videoPlayer.Prepare();

        WaitForSeconds waittime = new WaitForSeconds(1);

        while (!videoPlayer.isPrepared)
        {
            Debug.Log("preparing video");
            yield return waittime;

            break;
        }
        
        image.texture = videoPlayer.texture;

        str_name.text = videoToPlay2.name;
        image.transform.position = new Vector3(image.transform.position.x - 115, image.transform.position.y, image.transform.position.z);

        videoPlayer.Play();
        image.color = Color.white;
        audiosource.Play();

        while (videoPlayer.isPlaying)
        {

            str_time.text = Mathf.FloorToInt((float)videoPlayer.time).ToString() + "초";
            if (Mathf.FloorToInt((float)videoPlayer.time) == 35)
            {
               // OpenPoseController.TipFlag = true;
            }
            yield return null;
        }
        if (number == 0)
        {
            str_start.text = "운동 시작하기";
            StartCoroutine("PlayFade");
            str_time.text = "";
            str_name.text = "";
            str_Tip.GetComponent<Text>().text = "";
            // OpenPoseController.text.text = "";
            str_time_fix.gameObject.SetActive(false);
            VideoPlayFlag = false;
            triangle.gameObject.SetActive(false);
        } else
        {
            triangle.gameObject.SetActive(true);
            str_time.text = "";
            str_time_fix.gameObject.SetActive(false);
        }
            
        Destroy(videoPlayer);
        
    }
    // Update is called once per frame
    void Update () {

        
        Debug.Log("debug : " + image.transform.position.x);
        if(number == 0)
        {
            if (endFlag && !isRunning && VideoPlayFlag)
            {
                str_start.text = "운동 시작하기";
                str_time.text = "";
                str_name.text = "";
                
                triangle.gameObject.SetActive(false);
                str_Tip.GetComponent<Text>().text = "";
                GameObject Flag = GameObject.Find("tree");
                if (Flag != null)
                    tree.gameObject.SetActive(false);
              
            }
            
            if(endFlag)
              stopVideo();
            check = true;
            allClear = false;            
        }

        if (number == 1 && check)
        {
            StartCoroutine(playVideo());
            check = false;
            firstCheck = true;
            str_start.text = "";
            
            isRunning = false;
            
        }
        else if(number == 3 && allClear)
        {
           
            StartCoroutine(playVideo2());
            allClear = false;
            firstCheck = true;
            isRunning = false;
            GameObject Flag = GameObject.Find("tree");
            if(Flag != null)
                 tree.gameObject.SetActive(false);
        }
    }

   

   
}
