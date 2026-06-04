using Sackrany.ConfigSystem.SackranyConfig;
using Sackrany.GraphicsSettings.SackranyGraphicsSettings.Configs;

using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Sackrany.GraphicsSettings.SackranyGraphicsSettings
{
    /// <summary>
    /// Применяет <see cref="GraphicsConfig"/> к движку (разрешение, качество, тени, URP-ассет).
    /// Самодостаточная фича: зависит только от ConfigSystem.
    /// </summary>
    public static class GraphicsSettingsApplier
    {
        /// <summary>
        /// В билде применяем сохранённые настройки графики на старте.
        /// В редакторе не трогаем — чтобы не дёргать разрешение/качество окна.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
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

            // Разрешение и дисплей
            Screen.SetResolution(cfg.ResolutionX, cfg.ResolutionY, cfg.FullscreenMode,
                new RefreshRate { numerator = (uint)cfg.RefreshRate, denominator = 1 });
            Application.targetFrameRate = cfg.TargetFps;
            QualitySettings.vSyncCount  = cfg.Vsync ? 1 : 0;

            // Качество
            QualitySettings.SetQualityLevel(cfg.QualityLevel, true);

            // Тени — через URP Asset
            if (urp != null)
            {
                urp.shadowDistance               = cfg.ShadowDistance;
                urp.shadowCascadeCount           = cfg.ShadowCascades;
                urp.mainLightShadowmapResolution = cfg.ShadowResolution;
            }

            // Текстуры
            QualitySettings.globalTextureMipmapLimit = cfg.TextureQuality;
            QualitySettings.anisotropicFiltering     = cfg.AnisotropicFiltering
                ? AnisotropicFiltering.ForceEnable
                : AnisotropicFiltering.Disable;
            Texture.SetGlobalAnisotropicFilteringLimits(cfg.AnisotropicLevel, cfg.AnisotropicLevel);

            // Освещение
            QualitySettings.realtimeReflectionProbes = cfg.RealtimeReflections;
            QualitySettings.lodBias                  = cfg.LodBias;
            QualitySettings.maximumLODLevel          = cfg.MaximumLodLevel;

            // Прочее
            QualitySettings.particleRaycastBudget = cfg.ParticleRaycastBudget;
            QualitySettings.softParticles         = cfg.SoftParticles;
            QualitySettings.softVegetation        = cfg.SoftVegetation;

            DynamicConfigLoader.Save<GraphicsConfig>();
        }
    }
}
