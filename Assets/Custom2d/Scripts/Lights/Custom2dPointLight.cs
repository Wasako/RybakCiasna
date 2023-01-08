using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom2D.Lights
{
    using static Internal.LightUtility;

    [ExecuteInEditMode]
    public class Custom2dPointLight : MonoBehaviour, ILight
    {
        public Mesh Geometry => quad;

        public Material LightMaterial => lightMaterial;

        public MaterialPropertyBlock LightMaterialProperties => lightMaterialProperties;

        public Matrix4x4 Transform => matrix;

        #region Shape Properties
        public float MinRadius
        {
            get => minRadius;
            set
            {
                value = Mathf.Clamp(value, 0, maxRadius);
                this.minRadius = value;
                UpdateRadius();
            }
        }

        public float MaxRadius
        {
            get => maxRadius;
            set
            {
                this.maxRadius = value;
                //Reclamp Radius and update material
                MinRadius = MinRadius;
                MarkUpdateMatrix();
            }
        }

        public float MinAngle
        {
            get => minAngle;
            set
            {
                value = Mathf.Clamp(value, 0, maxAngle);
                this.minAngle = value;
                lightMaterialProperties.SetFloat(_FullAngleRatio, Mathf.Clamp01(minAngle / Mathf.PI/2f));
            }
        }

        public float MaxAngle
        {
            get => maxAngle;
            set
            {
                this.maxAngle = value;
                lightMaterialProperties.SetFloat(_Angle, value / Mathf.PI / 2f);
                //Reclamp Angle and update material
                MinAngle = MinAngle;
            }
        }
        

        [SerializeField]
        private float minRadius;

        [SerializeField]
        private float maxRadius;

        [Range(0, Mathf.PI * 2f)]
        [SerializeField]
        private float maxAngle;

        [Range(0, Mathf.PI * 2f)]
        [SerializeField]
        private float minAngle;
        #endregion

        #region Other Properties
        public float Intensity
        {
            get => intensity;
            set
            {
                this.intensity = value;
                lightMaterialProperties.SetFloat(_Intensity, value);
            }
        }

        public float Height
        {
            get => height;
            set
            {
                this.height = value;
                lightMaterialProperties.SetFloat(_Height, value);
            }
        }

        public float RayDirectionDistribution
        {
            get => rayDirectionDistribution;
            set
            {
                this.rayDirectionDistribution = value;
                lightMaterialProperties.SetFloat(_RayDirectionDistribution, value);
            }
        }

        public float FalloffIntensity
        {
            get => falloffIntensity;
            set
            {
                this.falloffIntensity = value;
                lightMaterialProperties.SetFloat(_FalloffFactor, value);
            }
        }

        public LightFalloffType FalloffType
        {
            get => falloffType;
            set
            {
                this.falloffType = value;
                SetActiveMaterialVariant(value);
                //SetFalloffType(lightMaterialProperties, value);
            }
        }

        [SerializeField]
        public float intensity;

        [SerializeField]
        public float height;

        [Range(0f, 1f)]
        [SerializeField]
        public float rayDirectionDistribution;

        [Range(0, 1)]
        [SerializeField]
        public float falloffIntensity;

        [SerializeField]
        private LightFalloffType falloffType;
        #endregion

        [SerializeField]
        private Mesh quad;

        [SerializeField]
        private Material linearLightMaterial;

        [SerializeField]
        private Material cosLightMaterial;
        
        [SerializeField]
        private Material quintLightMaterial;
        
        private Material lightMaterial;

        private MaterialPropertyBlock lightMaterialProperties;

        private Matrix4x4 matrix;

        private bool shouldUpdateMatrix;

        private void Awake()
        {
            if (lightMaterialProperties == null)
            {
                lightMaterialProperties = new MaterialPropertyBlock();
            }
        }

        private void OnEnable()
        {
            Custom2D.Rendering.Custom2DLightManager.Instance.RegisterLight(this);
        }

        private void Start()
        {
            RecalculateMatrix();

            SetMaterialProperties();
        }

        private void Update()
        {
            if (shouldUpdateMatrix)
            {
                RecalculateMatrix();
                //shouldUpdateMatrix = false;
            }
        }

        private void OnDisable()
        {
            Custom2D.Rendering.Custom2DLightManager.Instance.UnregisterLight(this);
        }

        private void OnValidate()
        {
            shouldUpdateMatrix = true;

            SetMaterialProperties();
        }

        private void MarkUpdateMatrix()
        {
            shouldUpdateMatrix = true;
        }

        private void RecalculateMatrix()
        {
            matrix = Matrix4x4.TRS(transform.position, Quaternion.identity, Vector3.one * maxRadius);
        }

        private void UpdateRadius()
        {
            if (maxRadius < 0.0001f)
            {
                lightMaterialProperties.SetFloat(_InnerRadiusRatio, 0);
            }
            else
            {
                lightMaterialProperties.SetFloat(_InnerRadiusRatio, Mathf.Clamp01(minRadius / maxRadius));
            }
        }

        private void SetMaterialProperties()
        {
            if (lightMaterialProperties == null)
            {
                lightMaterialProperties = new MaterialPropertyBlock();
            }
            MinRadius = minRadius;
            MaxRadius = maxRadius;
            MinAngle = minAngle;
            MaxAngle = maxAngle;

            Intensity = intensity;
            RayDirectionDistribution = rayDirectionDistribution;
            Height = height;
            FalloffIntensity = falloffIntensity;
            FalloffType = falloffType;
        }

        private void SetActiveMaterialVariant(LightFalloffType falloffType)
        {
            lightMaterial = falloffType switch
            {
                LightFalloffType.Linear => linearLightMaterial,
                LightFalloffType.Cosine => cosLightMaterial,
                LightFalloffType.Quintic => quintLightMaterial,
                _ => linearLightMaterial
            };
        }
    }
}