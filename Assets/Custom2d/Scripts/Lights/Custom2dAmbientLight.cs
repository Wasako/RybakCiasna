using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Custom2D.Lights
{
    using static Internal.LightUtility;

    [ExecuteInEditMode]
    public class Custom2dAmbientLight : MonoBehaviour, ILight
    {
        public Mesh Geometry => null;

        public Material LightMaterial => lightMaterial;

        public MaterialPropertyBlock LightMaterialProperties => lightMaterialProperties;

        public Matrix4x4 Transform => Matrix4x4.zero;

        [SerializeField]
        private Material lightMaterial;

        public float Intensity
        {
            get => intensity;
            set
            {
                this.intensity = value;
                lightMaterialProperties.SetFloat(_Intensity, value);
            }
        }

        [SerializeField]
        private float intensity;

        private MaterialPropertyBlock lightMaterialProperties;

        private void Awake()
        {
            if (lightMaterialProperties == null)
            {
                lightMaterialProperties = new MaterialPropertyBlock();
            }
        }

        private void OnEnable()
        {
            Rendering.Custom2DLightManager.Instance.RegisterLight(this);
        }

        private void Start()
        {
            //lightMaterial;// = Instantiate(lightMaterial);
            SetMaterialProperties();
        }

        private void OnDisable()
        {
            Custom2D.Rendering.Custom2DLightManager.Instance.UnregisterLight(this);
        }

        private void OnValidate()
        {
            SetMaterialProperties();
        }

        private void SetMaterialProperties()
        {
            if (lightMaterialProperties == null)
            {
                lightMaterialProperties = new MaterialPropertyBlock();
            }

            Intensity = intensity;
        }
    }
}