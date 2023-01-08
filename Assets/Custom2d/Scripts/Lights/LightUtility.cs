using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom2D.Lights.Internal
{
    public static class LightUtility
    {
        public static readonly int _InnerRadiusRatio = Shader.PropertyToID("_InnerRadiusRatio");
        public static readonly int _FullAngleRatio = Shader.PropertyToID("_FullAngleRatio");
        public static readonly int _Angle = Shader.PropertyToID("_Angle");
        public static readonly int _Intensity = Shader.PropertyToID("_Intensity");
        public static readonly int _Height = Shader.PropertyToID("_Height");
        public static readonly int _FalloffFactor = Shader.PropertyToID("_FalloffFactor");
        public static readonly int _RayDirectionDistribution = Shader.PropertyToID("_RayDirectionDistribution");

        /*public static void SetFalloffType(MaterialPropertyBlock lightMaterial, LightFalloffType fallofType)
        {
            switch (fallofType) 
            {
                case LightFalloffType.Linear:
                    {
                        lightMaterial.EnableKeyword(LightFalloffTypeKeywords.Linear);
                        lightMaterial.DisableKeyword(LightFalloffTypeKeywords.Cosine);
                        lightMaterial.DisableKeyword(LightFalloffTypeKeywords.Quintic);
                        break;
                    }
                case LightFalloffType.Cosine:
                    {
                        lightMaterial.DisableKeyword(LightFalloffTypeKeywords.Linear);
                        lightMaterial.EnableKeyword(LightFalloffTypeKeywords.Cosine);
                        lightMaterial.DisableKeyword(LightFalloffTypeKeywords.Quintic);
                        break;
                    }
                case LightFalloffType.Quintic:
                    {
                        lightMaterial.DisableKeyword(LightFalloffTypeKeywords.Linear);
                        lightMaterial.DisableKeyword(LightFalloffTypeKeywords.Cosine);
                        lightMaterial.EnableKeyword(LightFalloffTypeKeywords.Quintic);
                        break;
                    }
            }
        }*/
    }
}