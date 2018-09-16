using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {

    // Wrapper for noise function
    public static float PerlinNoise(float x, float width, float y, float height, float scale)
    {
        return Mathf.PerlinNoise(x / width * scale, y / height * scale);
    }

}
