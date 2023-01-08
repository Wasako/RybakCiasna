using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom2D.Rendering
{
    using Lights;

    public class Custom2DLightManager
    {
        public static Custom2DLightManager Instance { get; } = new Custom2DLightManager();

        public IEnumerable<ILight> AllLights => RegisteredLights;

        private HashSet<ILight> RegisteredLights = new HashSet<ILight>();

#if UNITY_EDITOR
        public IEnumerable<ILight> EditModeLights => RegisteredEditModeLights;

        private HashSet<ILight> RegisteredEditModeLights = new HashSet<ILight>();
#endif

        public void RegisterLight(ILight light)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                RegisteredEditModeLights.Add(light);
                return;
            }
#endif
            RegisteredLights.Add(light);
        }

        public void UnregisterLight(ILight light)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                RegisteredEditModeLights.Remove(light);
                return;
            }
#endif
            RegisteredLights.Remove(light);
        }

    }
}