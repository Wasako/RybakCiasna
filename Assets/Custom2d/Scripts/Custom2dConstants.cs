using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom2D
{
    public static class Custom2dConstants
    {
        public static readonly int _ColorTex = Shader.PropertyToID("_ColorTex");
        public static readonly int _NormalTex = Shader.PropertyToID("_NormalTex");
        public static readonly int _EmissionTex = Shader.PropertyToID("_EmissionTex");
        public static readonly int _LightingTex = Shader.PropertyToID("_LightingTex");
    }
}