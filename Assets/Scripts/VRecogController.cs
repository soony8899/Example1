
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Windows.Speech;

public class VRecogController
{

    private DictationRecognizer dictationRecognizer;


    // Use this for initialization
    public void start()
    {

        dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += onDictationResult;
        dictationRecognizer.DictationHypothesis += onDictationHypothesis;
        dictationRecognizer.DictationComplete += onDictationComplete;
        dictationRecognizer.DictationError += onDictationError;

        dictationRecognizer.Start();
    }

    void onDictationResult(string text, ConfidenceLevel confidence)
    {
        // write your logic here

        Debug.LogFormat("Dictation result: " + text);
        switch (text)
        {
            case "next":
                Debug.Log("다음 콘텐츠");
                break;
            case "main":
                Debug.Log("처음으로");
                break;
            case "previous":
                Debug.Log("이전 콘텐츠");

                break;
        }
    }

    void onDictationHypothesis(string text)
    {
        // write your logic here
        switch (text)
        {
            case "next":
                Debug.Log("다음 콘텐츠");
                break;
            case "main":
                Debug.Log("처음으로");
                break;
            case "previous":
                Debug.Log("이전 콘텐츠");

                break;
        }
        Debug.LogFormat("Dictation hypothesis: {0}", text);
    }

    void onDictationComplete(DictationCompletionCause cause)
    {
        // write your logic here
        if (cause != DictationCompletionCause.Complete)
            Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", cause);
    }

    void onDictationError(string error, int hresult)
    {
        // write your logic here
        Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
    }
}
