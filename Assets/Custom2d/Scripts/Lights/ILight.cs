using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom2D.Lights
{
    public interface ILight
    {
        /// <summary>
        /// Geometry of the light
        /// Return null for full screen lights
        /// </summary>
        Mesh Geometry { get; }

        /// <summary>
        /// Pass of the light material to use
        /// </summary>
        int Pass { get => 0; }

        /// <summary>
        /// Material to use for rendering the light
        /// </summary>
        Material LightMaterial { get; }

        /// <summary>
        /// Material properties to use for rendering the light
        /// </summary>
        MaterialPropertyBlock LightMaterialProperties { get; }

        /// <summary>
        /// Transform for the light's geomety
        /// </summary>
        Matrix4x4 Transform { get; }
    }
}