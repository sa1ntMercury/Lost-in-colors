using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Points
{
    public static void RecordResult(int result)
    {
        if (PlayerPrefs.GetInt("HighScore") < result)
        {
            PlayerPrefs.SetInt("HighScore", result);
        }
    }
    
}
