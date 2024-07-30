using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Points
{
    private static int _bestResult;

    public static void RecordResult(int result)
    {
        if(_bestResult < result)
        {
            _bestResult = result;
        }
    }
    
}
