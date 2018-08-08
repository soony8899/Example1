using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoController : MonoBehaviour {
    public int number;
    public bool endFlag;
    public bool isPlaying;

    private RawImage background;
    private Image tree;
    private Image triangle;
    private RenderTexture vt;

    private Text ex_name;
    private Text str_start;
    private Text ex_time;
    private Text ex_r_time;
    private float first_x = 0f;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private VideoClip videoToPlay;

    private AudioSource audiosource;

    public VideoController(RawImage bg, Image tree, Image triangle, Text ex_name, Text ex_time, VideoClip videoToPlay, RenderTexture vt)
    {

        
        this.first_x = bg.transform.position.x; // 동영상 크기 차이에 따른 포지션 변경을 위한 작업
        this.background = bg;
        this.tree = tree;
        this.triangle = triangle;
        this.ex_name = ex_name;
        this.ex_time = ex_time;
        this.videoToPlay = videoToPlay;
        this.isPlaying = false;
        this.vt = vt;

        Debug.Log("init");
      
        if (background.GetComponent<VideoPlayer>() == null)
        {
            background.gameObject.AddComponent<VideoPlayer>();
          //  background.gameObject.AddComponent<AudioSource>();
        }

        background.texture = vt;
        background.color = Color.black;
    }

    public VideoController(RawImage bg, Image tree, Image triangle, Text ex_name, Text ex_time, Text ex_r_time, VideoClip videoToPlay, RenderTexture vt)
    {


        this.first_x = bg.transform.position.x; // 동영상 크기 차이에 따른 포지션 변경을 위한 작업
        this.background = bg;
        this.tree = tree;
        this.triangle = triangle;
        this.ex_name = ex_name;
        this.ex_time = ex_time;
        this.ex_r_time = ex_r_time;
        this.videoToPlay = videoToPlay;
        this.isPlaying = false;
        this.vt = vt;

        Debug.Log("init");

        if (background.GetComponent<VideoPlayer>() == null)
        {
            background.gameObject.AddComponent<VideoPlayer>();
           //background.gameObject.AddComponent<AudioSource>();
        }

        background.texture = vt;
        background.color = Color.black;
    }

    ~VideoController()
    {

      
    }

    public IEnumerator playVideo()
    {


        videoPlayer = background.GetComponent<VideoPlayer>();

        isPlaying = true;
        OpenPoseController.TipFlag = true;
       // OpenPoseController.next_flag = false;
        videoPlayer.waitForFirstFrame = false;
        videoPlayer.playOnAwake = false;

       
        videoPlayer.source = VideoSource.VideoClip;

        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audiosource);
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        WaitForSeconds waittime = new WaitForSeconds(1);
        

        while (!videoPlayer.isPrepared)
        {
            Debug.Log("preparing video");
            background.color = Color.black;
            yield return waittime;

            break;
        }

        videoPlayer.targetTexture = vt;

        ex_name.text = videoToPlay.name;

       

        background.gameObject.SetActive(true);
       
        videoPlayer.Play();
     

        while (videoPlayer.isPlaying)
        {
            background.color = Color.white;
            ex_time.text = Mathf.FloorToInt((float)videoPlayer.time).ToString() + "초";
            ex_r_time.text = ((int)videoToPlay.length - Mathf.FloorToInt((float)videoPlayer.time)).ToString() + "초";
            OpenPoseController.play_time = Mathf.FloorToInt((float)videoPlayer.time);
          
            if(OpenPoseController.number == 1 && OpenPoseController.next_flag && Mathf.FloorToInt((float)videoPlayer.time) == 25)
            {
                OpenPoseController.number = 2;
                OpenPoseController.next_flag = false;
            }

            if(OpenPoseController.number == 3 && OpenPoseController.next_flag && Mathf.FloorToInt((float)videoPlayer.time) == 15)
            {
                OpenPoseController.number = 4;
                OpenPoseController.next_flag = false;
            }
            if(OpenPoseController.number == 5 && OpenPoseController.next_flag && Mathf.FloorToInt((float)videoPlayer.time) == 25)
            {
                OpenPoseController.number = 6;
                OpenPoseController.next_flag = false;
            }
            if (OpenPoseController.number == 7 && OpenPoseController.next_flag && Mathf.FloorToInt((float)videoPlayer.time) == 17)
            {
                OpenPoseController.number = 8;
                OpenPoseController.next_flag = false;
            }
            /*  if (Mathf.FloorToInt((float)videoPlayer.time) == 25 && OpenPoseController.number == 2)
              {
                  Debug.Log("gg");
                    OpenPoseController.TipFlag = true;
              }*/
            yield return null;
        }

        OpenPoseController.TipFlag = false;
        if (OpenPoseController.next_flag)
        {
            Debug.Log("++");
            OpenPoseController.number++;
            OpenPoseController.next_flag = false;
        }

        if(OpenPoseController.number == 9)
        {
            OpenPoseController.number = 0;
        }

        if (number == 1 || number == 2)
        {
            tree.gameObject.SetActive(true);
            ex_time.text = "";
            //str_time_fix.gameObject.SetActive(false);
        }

        background.gameObject.SetActive(false);
        isPlaying = false;
    }

    public void StopVideo()
    {
        if (videoPlayer != null)
        {
            if (videoPlayer.isPlaying)
            {
                
                isPlaying = false;
                videoPlayer.Stop();

            }
        }
    }


}
