using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TimeCycleScript : MonoBehaviour
{
    [SerializeField] Transform LightSource;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] TMP_Text TimeText;
    DateTime currentTime;
    float convertedTime;
    float TimeFlowWait;
    Coroutine coroutineAngle;
    Coroutine coroutineValue;

    private void Update()
    {
        TimeFlowWait = 240 - scrollbar.value * 240 + 1;
    }

    public void ChangeCurrTime(DateTime time)
    {
        currentTime = time;
        convertedTime = time.Second + time.Minute * 60 + time.Hour * 3600;
        TimeText.text = time.ToString();
        Debug.Log(convertedTime);
        Debug.Log(time);

        LightSource.localRotation = Quaternion.Euler(270 + convertedTime / 240, 0, 0);
        StartTimeFlow();
    }

    public void StartTimeFlow()
    {
        Debug.Log(TimeFlowWait);
        if (coroutineAngle != null)
        {
            StopCoroutine(coroutineAngle);
        }
        coroutineAngle = StartCoroutine(TimeFlowAngle());

        if (coroutineValue != null)
        {
            StopCoroutine(coroutineValue);
        }
        coroutineValue = StartCoroutine(TimeFlowValue());
    }

    IEnumerator TimeFlowAngle()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeFlowWait);
            Debug.Log("Angle");
            LightSource.Rotate(new Vector3(1, 0, 0));
        }
    }

    IEnumerator TimeFlowValue()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeFlowWait / 240);
            currentTime = currentTime.AddSeconds(1);
            TimeText.text = currentTime.ToString();
        }
    }
}