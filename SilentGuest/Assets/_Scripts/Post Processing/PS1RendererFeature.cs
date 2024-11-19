using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;

///-///////////////////////////////////////////////////////////////////////////////////////////
///
/// Manages all the render passes that are made
/// 
public class PS1RendererFeature : ScriptableRendererFeature
{
    ///-///////////////////////////////////////////////////////////////////////////////////////////
    ///
    /// Handles all the passes and rendering
    ///  
    class PS1EffectPass : ScriptableRenderPass
    {
        private const string m_PassName = "PS1EffectPass";
        private Material m_BlitMaterial;

        public void Setup(Material mat)
        {
            m_BlitMaterial = mat;
            requiresIntermediateTexture = true;
        }
        
        // This class stores the data needed by the RenderGraph pass.
        // It is passed as a parameter to the delegate function that executes the RenderGraph pass.
        private class PassData
        {
        }

        // This static method is passed as the RenderFunc delegate to the RenderGraph render pass.
        // It is used to execute draw commands.
        static void ExecutePass(PassData data, RasterGraphContext context)
        {
        }

        // RecordRenderGraph is where the RenderGraph handle can be accessed, through which render passes can be added to the graph.
        // FrameData is a context container through which URP resources can be accessed and managed.
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            var stack = VolumeManager.instance.stack;
            var customEffect = stack.GetComponent<PS1FXComponent>();

            // Return if effect is active
            if (customEffect.IsActive() == false)
            {
                return;
            }
            
            UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();

            if (resourceData.isActiveTargetBackBuffer)
            {
                Debug.LogError($"Skipping render pass. PS1RendererFeature requires requires an immediate" +
                               $"ColorTexture, we cannot use BackBuffer as a texture input.");
                return;
            }

            var source = resourceData.activeColorTexture;
            var destinationDesc = renderGraph.GetTextureDesc(source);
            destinationDesc.name = $"CameraColor-{m_PassName}";
            destinationDesc.clearBuffer = false;

            TextureHandle destination = renderGraph.CreateTexture(destinationDesc);

            // Apply Blit Pass
            RenderGraphUtils.BlitMaterialParameters param = new(source, destination, m_BlitMaterial, 0);
            renderGraph.AddBlitPass(param, passName: m_PassName);
            
            // Swaps camera color with modified texture. Future passes will always use this. Does not need future Blit operations.
            resourceData.cameraColor = destination;
        }
    }

    public RenderPassEvent injectionPoint = RenderPassEvent.AfterRenderingPostProcessing;
    public Material material;
    
    PS1EffectPass m_ScriptablePass;

    /// <inheritdoc/>
    public override void Create()
    {
        m_ScriptablePass = new PS1EffectPass();

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = injectionPoint;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (material == null)
        {
            Debug.LogWarning("PS1RendererFeature material is null and will be skipped.");
        }

        m_ScriptablePass.Setup(material);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}
