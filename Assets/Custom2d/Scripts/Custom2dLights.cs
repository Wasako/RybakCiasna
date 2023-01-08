using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Custom2D.Rendering
{
    using static Utils.BBUtils;
    using Lights;

    public class Custom2dLights : ScriptableRendererFeature
    {
        class Custom2dLightsPass : ScriptableRenderPass
        {
            private static readonly ShaderTagId ColorPassId = new ShaderTagId("Custom2d-Color");
            private static readonly ShaderTagId NormalPassId = new ShaderTagId("Custom2d-Normal");
            private static readonly ShaderTagId EmissionPassId = new ShaderTagId("Custom2d-Emission");

            private RenderTargetHandle _SceneColor;
            private RenderTargetHandle _SceneNormals;
            private RenderTargetHandle _SceneEmission;
            private RenderTargetHandle _SceneLighting;

            private Material finalBlitMaterial;

            public Custom2dLightsPass(Material finalBlitMaterial)
            {
                _SceneColor.Init(nameof(Custom2dConstants._ColorTex));
                _SceneNormals.Init(nameof(Custom2dConstants._NormalTex));
                _SceneEmission.Init(nameof(Custom2dConstants._EmissionTex));
                _SceneLighting.Init(nameof(Custom2dConstants._LightingTex));

                this.finalBlitMaterial = finalBlitMaterial;
            } 

            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
            {
                RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
            }

            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
                base.Configure(cmd, cameraTextureDescriptor);

                RenderTextureDescriptor desc = cameraTextureDescriptor;

                cmd.GetTemporaryRT(_SceneColor.id, desc);
                cmd.GetTemporaryRT(_SceneNormals.id, desc);
                cmd.GetTemporaryRT(_SceneEmission.id, desc);
                cmd.GetTemporaryRT(_SceneLighting.id, desc);
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var target = renderingData.cameraData.renderer.cameraColorTarget;

                //Drawing settings and stuff
                var drawing = CreateDrawingSettings(ColorPassId, ref renderingData, SortingCriteria.CommonTransparent);
                var filter = FilteringSettings.defaultValue;
                
                //Scene Color Target Setup
                CommandBuffer cmd = CommandBufferPool.Get();
                cmd.ClearRenderTarget(RTClearFlags.All, Color.black, 1f, 0);
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();
                
                //Scene Color Rendering
                context.DrawRenderers(renderingData.cullResults, ref drawing, ref filter);
                cmd.CopyTexture(target, _SceneColor.Identifier());

                //Scene Emission Target Setup
                cmd.ClearRenderTarget(false, true, new Color(0f, 0f, 0f));
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                //Scane Emission Rendering
                drawing.SetShaderPassName(0, EmissionPassId);
                context.DrawRenderers(renderingData.cullResults, ref drawing, ref filter);
                cmd.CopyTexture(target, _SceneEmission.Identifier());

                //Scene Normals Target Setup
                cmd.ClearRenderTarget(false, true, new Color(0.25f, 0.25f, 0.25f));
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                //Scane Normals Rendering
                drawing.SetShaderPassName(0, NormalPassId);
                context.DrawRenderers(renderingData.cullResults, ref drawing, ref filter);
                cmd.CopyTexture(target, _SceneNormals.Identifier());

                //Set Light target
                cmd.ClearRenderTarget(false, true, new Color(0f, 0f, 0f, 0f));


                //Prepare the magic
                cmd.SetGlobalTexture(Custom2dConstants._ColorTex, _SceneColor.Identifier());
                cmd.SetGlobalTexture(Custom2dConstants._EmissionTex, _SceneEmission.Identifier());
                cmd.SetGlobalTexture(Custom2dConstants._LightingTex, _SceneLighting.Identifier());
                cmd.SetGlobalTexture(Custom2dConstants._NormalTex, _SceneNormals.Identifier());

                var cam = renderingData.cameraData.camera;
                var camTransform = cam.transform;
                var ortoSIze = cam.orthographicSize;
                float cameraRotation = Mathf.Deg2Rad * camTransform.eulerAngles.z;
                var widthHalf = ortoSIze * Screen.width / Screen.height;
                Rect cameraRect = new Rect(new Vector3(-widthHalf, -ortoSIze), new Vector2(widthHalf * 2f, ortoSIze * 2f));
                var camMatrix = cam.worldToCameraMatrix;

                var fullScreneMatrix = Matrix4x4.TRS(new Vector3(camTransform.position.x, camTransform.position.y), camTransform.rotation, new Vector3(widthHalf, ortoSIze));

#if UNITY_EDITOR
                IEnumerable<ILight> lights;
                if (Application.isPlaying)
                {
                    lights = Custom2DLightManager.Instance.AllLights;
                }
                else
                {
                    lights = Custom2DLightManager.Instance.EditModeLights;
                }
#else
                IEnumerable<ILight> lights = Custom2DLightManager.Instance.AllLights;
#endif
                //Do the magic
                foreach (var light in lights)
                {
                    if (light.Geometry == null)
                    {
                        cmd.CopyTexture(target, _SceneLighting.Identifier());
                        //cmd.Blit(target, target, light.LightMaterial, 0);
                        cmd.DrawMesh(RenderingUtils.fullscreenMesh, fullScreneMatrix, light.LightMaterial, 0, light.Pass, light.LightMaterialProperties);
                    }
                    else
                    {
                        //Cull Light
                        var lightInCamSpaceMatrix = camMatrix * light.Transform;
                        var bb = light.Geometry.GetTransformedBB2D(lightInCamSpaceMatrix);
                        
                        if (cameraRect.Overlaps(bb))
                        {
                            //Render Light
                            cmd.CopyTexture(target, _SceneLighting.Identifier());
                            if (light.LightMaterialProperties == null)
                            {
                                cmd.DrawMesh(light.Geometry, light.Transform, light.LightMaterial, 0, light.Pass);
                            }
                            else
                            {
                                cmd.DrawMesh(light.Geometry, light.Transform, light.LightMaterial, 0, light.Pass, light.LightMaterialProperties);
                            }
                        }
                    }
                }
                cmd.CopyTexture(target, _SceneLighting.Identifier());

                //Finaly combine color with lights
                cmd.Blit(_SceneColor.Identifier(), target, finalBlitMaterial, 0);
                //cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, finalBlitMaterial, 0, 0);

                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                CommandBufferPool.Release(cmd);
            }

            public override void FrameCleanup(CommandBuffer cmd)
            {
                base.FrameCleanup(cmd);

                cmd.ReleaseTemporaryRT(_SceneColor.id);
                cmd.ReleaseTemporaryRT(_SceneNormals.id);
                cmd.ReleaseTemporaryRT(_SceneEmission.id);
                cmd.ReleaseTemporaryRT(_SceneLighting.id);
            }

            public override void OnCameraCleanup(CommandBuffer cmd)
            {
            }
        }

        Custom2dLightsPass m_ScriptablePass;

        [SerializeField]
        private Material finalBlitMaterial;

        /// <inheritdoc/>
        public override void Create()
        {
            m_ScriptablePass = new Custom2dLightsPass(finalBlitMaterial);

            m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(m_ScriptablePass);
        }
    }
}