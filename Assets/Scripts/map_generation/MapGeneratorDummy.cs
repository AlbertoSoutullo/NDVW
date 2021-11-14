using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MapGeneratorDummy
{
    public static WallState[,] Generate(uint size)
    {
        WallState[,] map = new WallState[size, size];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int i = 0; i < size; ++i) 
        {
            for (int j = 0; j < size; ++j) 
            {
                map[i, j] = initial;
            }  
        }
        return map;
    }
}