using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;

public class WeatherScript : MonoBehaviour
{
    public TMP_InputField CityField;
    [SerializeField] TMP_Text WeatherText;
    [SerializeField] TMP_Text WeatherDescr;
    [SerializeField] TMP_Text Temperature;
    [SerializeField] RainController rain;
    [SerializeField] SnowController snow;


    public void RunRequest()
    {
        StartCoroutine(WeatherRequest("https://api.openweathermap.org/data/2.5/weather?q=" + CityField.text + "&appid=ccb3c202154191d456bdaca135dd69ae"));
    }

    public IEnumerator WeatherRequest(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("WORKED");
            WeatherObject weather = JsonParser.ConvertToWeather(request.downloadHandler.text);
            weather.main.temp = weather.main.temp - 273.15;

            Temperature.text = Math.Round(weather.main.temp, 1).ToString();
            WeatherText.text = weather.weather[0].main;
            WeatherDescr.text = weather.weather[0].description;
            weather.condition = (WeatherConditions) Enum.Parse(typeof(WeatherConditions), weather.weather[0].main);
            ConjureWeather(weather);
            StartCoroutine(TimeRequest("https://maps.googleapis.com/maps/api/timezone/json?location=" + weather.coord.lat + "," + weather.coord.lon + "&timestamp=" + DateTimeOffset.UtcNow.ToUnixTimeSeconds() +"&key=AIzaSyAf14Iqn1N5MEAjyHv0V5nAiYMA13Zp0Ls"));
        }
        else
        {
            Debug.Log("FAILED");
        }
        //https://maps.googleapis.com/maps/api/timezone/json?location=39.6034810%2C-119.6822510&timestamp=1331161200&key=AIzaSyAf14Iqn1N5MEAjyHv0V5nAiYMA13Zp0Ls
        //https://api.openweathermap.org/data/2.5/weather?q=Almaty&appid=ccb3c202154191d456bdaca135dd69ae
    }

    public IEnumerator TimeRequest(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("WORKED");
            TimeObject time = JsonParser.ConvertToTime(request.downloadHandler.text);
            time.actualTime = DateTime.UtcNow.AddSeconds(time.rawOffset);

            GetComponent<TimeCycleScript>().ChangeCurrTime(time.actualTime);
        }
        else
        {
            Debug.Log("FAILED");
        }
    }

    public void ConjureWeather(WeatherObject weather)
    {
        rain.ResetValues();
        snow.ResetValues();

        switch (weather.condition)
        {
            case WeatherConditions.Drizzle:
                rain.rainIntensity = 0.2f;
                break;
            case WeatherConditions.Rain:
                rain.rainIntensity = 0.6f;
                break;
            case WeatherConditions.Thunderstorm:
                rain.rainIntensity = 1;
                rain.lightningIntensity = 0.8f;
                break;
            case WeatherConditions.Clouds:
                break;
            case WeatherConditions.Snow:
                snow.snowIntensity = 1f;
                break;
            default:
                break;
        }
    }
}
