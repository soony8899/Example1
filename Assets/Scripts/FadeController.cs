using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController {
    public Text inputText;
    public bool isRunning;
    private Image inputImage;
    private float time;
    private float animTime;
    private float start;
    private float end;
 
    public FadeController(Text input, float animationTime)
    {
        this.inputText = input;
        this.start = 1f;
        this.end = 0f;
        this.time = 0f;
        this.animTime = animationTime;
        this.isRunning = false;
    }

    public FadeController(Image input, float animationTime)
    {
        this.inputImage = input;
        this.start = 1f;
        this.end = 0f;
        this.time = 0f;
        this.animTime = animationTime;
        this.isRunning = false;
    }

    public IEnumerator Fade()
    {
        //
        isRunning = true;

        Color color = inputText.color;
        time = 0f;
        color.a = Mathf.Lerp(end, start, time);
        while (color.a < 1f)
        {
            time += Time.deltaTime / animTime;

            color.a = Mathf.Lerp(end, start, time);
            inputText.color = color;
            yield return null;
        }
        time = 0f;
        //yield return new WaitForSeconds(2f);
        while (color.a > 0f)
        {
            time += Time.deltaTime / animTime;

            color.a = Mathf.Lerp(start, end, time);
            inputText.color = color;
            yield return null;
        }

        isRunning = false;

    }

    public IEnumerator Fade_img()
    {
        //
        isRunning = true;

        Color color = inputImage.color;
        time = 0f;
        color.a = Mathf.Lerp(end, start, time);
        while (color.a < 1f)
        {
            time += Time.deltaTime / animTime;

            color.a = Mathf.Lerp(end, start, time);
            inputImage.color = color;
            yield return null;
        }
        time = 0f;
        //yield return new WaitForSeconds(2f);
        while (color.a > 0f)
        {
            time += Time.deltaTime / animTime;

            color.a = Mathf.Lerp(start, end, time);
            inputImage.color = color;
            yield return null;
        }

        isRunning = false;

    }

    public IEnumerator Fade_wait()
    {
        //
        isRunning = true;

        Color color = inputText.color;
        time = 0f;
        color.a = Mathf.Lerp(end, start, time);
        while (color.a < 1f)
        {
            time += Time.deltaTime / animTime;

            color.a = Mathf.Lerp(end, start, time);
            inputText.color = color;
            yield return null;
        }
        time = 0f;
        yield return new WaitForSeconds(1.5f);
        while (color.a > 0f)
        {
            time += Time.deltaTime / animTime;

            color.a = Mathf.Lerp(start, end, time);
            inputText.color = color;
            yield return null;
        }

        isRunning = false;

    }

}
