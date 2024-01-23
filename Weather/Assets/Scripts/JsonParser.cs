using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JsonParser
{
    public static WeatherObject ConvertToWeather(string json)
    {
        return JsonUtility.FromJson<WeatherObject>(json);
    }

    public static TimeObject ConvertToTime(string json)
    {
        return JsonUtility.FromJson<TimeObject>(json);
    }
}

[System.Serializable]
public class Weather
{
    public string main;
    public string description;
}

[System.Serializable]
public class Coord
{
    public double lon;
    public double lat;
}

[System.Serializable]
public class Main
{
    public double temp;
    public double pressure;
    public double humidity;
}

[System.Serializable]
public class WeatherObject
{
    public List<Weather> weather;
    public Main main;
    public Coord coord;
    public WeatherConditions condition;
}

[System.Serializable]
public class TimeObject
{
    public string timeZoneId;
    public int rawOffset;
    public DateTime actualTime;
}
public enum WeatherConditions
{
    None,
    Thunderstorm,
    Drizzle,
    Rain,
    Snow,
    Clear,
    Clouds
}
