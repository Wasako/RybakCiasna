using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom2D.Lights
{
    public enum LightFalloffType
    {
        Linear,
        Cosine,
        Quintic
    }

    public static class LightFalloffTypeKeywords 
    {
        public static string Linear = "_FALLOFFTYPE_EXP";
        public static string Cosine = "_FALLOFFTYPE_COSEXP";
        public static string Quintic = "_FALLOFFTYPE_QUINTEXP";
    }

}