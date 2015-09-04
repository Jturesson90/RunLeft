using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

public static class Extensions
{

    public static string ToFormattedTimeString(this long time)
    {
        TimeSpan ts = TimeSpan.FromMilliseconds(time);
        string elapsedTime = String.Format("{0:0}:{1:00}.{2:00}", ts.Minutes, ts.Seconds,
          ts.Milliseconds / 10);
        return elapsedTime;
    }
    public static string ToFormattedTimeString(this float time)
    {
        TimeSpan ts = TimeSpan.FromMilliseconds(time*1000);
        string elapsedTime = String.Format("{0:0}:{1:00}.{2:00}", ts.Minutes, ts.Seconds,
          ts.Milliseconds / 10);
        return elapsedTime;
    }
}
