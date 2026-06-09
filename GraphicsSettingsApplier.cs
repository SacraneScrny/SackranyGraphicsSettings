using SackranyGraphicsSettings.Configs;

using SackranyConfig;

using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SackranyGraphicsSettings
{
    public static class GraphicsSettingsApplier
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void AutoApply()
        {
#if !UNITY_EDITOR
            Apply();
#endif
        }

        public static void Apply() => Apply(ConfigGet<GraphicsConfig>.Value);

        public static void Apply(GraphicsConfig cfg)
        {
            var urp = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline
                      ?? (UniversalRenderPipelineAsset)UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;

            Screen.SetResolution(cfg.ResolutionX, cfg.ResolutionY, cfg.FullscreenMode,
                new RefreshRate { numerator = (uint)cfg.RefreshRate, denominator = 1 });
            Application.targetFrameRate = cfg.TargetFps;
            QualitySettings.vSyncCount  = cfg.Vsync ? 1 : 0;

            QualitySettings.SetQualityLevel(cfg.QualityLevel, true);

            if (urp != null)
            {
                urp.shadowDistance               = cfg.ShadowDistance;
                urp.shadowCascadeCount           = cfg.ShadowCascades;
                urp.mainLightShadowmapResolution = cfg.ShadowResolution;
            }

            QualitySettings.globalTextureMipmapLimit = cfg.TextureQuality;
            QualitySettings.anisotropicFiltering     = cfg.AnisotropicFiltering
                ? AnisotropicFiltering.ForceEnable
                : AnisotropicFiltering.Disable;
            Texture.SetGlobalAnisotropicFilteringLimits(cfg.AnisotropicLevel, cfg.AnisotropicLevel);

            QualitySettings.realtimeReflectionProbes = cfg.RealtimeReflections;
            QualitySettings.lodBias                  = cfg.LodBias;
            QualitySettings.maximumLODLevel          = cfg.MaximumLodLevel;

            QualitySettings.particleRaycastBudget = cfg.ParticleRaycastBudget;
            QualitySettings.softParticles         = cfg.SoftParticles;
            QualitySettings.softVegetation        = cfg.SoftVegetation;

            DynamicConfigLoader.Save<GraphicsConfig>();
        }
    }
}
