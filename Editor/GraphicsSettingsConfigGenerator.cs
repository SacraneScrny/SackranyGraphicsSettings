#if UNITY_EDITOR
using System.IO;

using Newtonsoft.Json;

using Sackrany.GraphicsSettings.SackranyGraphicsSettings.Configs;

using UnityEditor;

using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Sackrany.GraphicsSettings.Editor.SackranyGraphicsSettings.Editor
{
    public static class GraphicsSettingsConfigGenerator
    {
        const string ResourcesPath = "Assets/Resources/Configs";

        [MenuItem("Sackrany/GraphicsSettings/Generate Default Config")]
        static void Generate()
        {
            Directory.CreateDirectory(ResourcesPath);

            var cfg  = BuildFromCurrentProject();
            var path = Path.Combine(ResourcesPath, $"{nameof(GraphicsConfig)}.json");

            if (File.Exists(path) &&
                !EditorUtility.DisplayDialog("Overwrite?", $"{nameof(GraphicsConfig)}.json exists. Overwrite?", "Overwrite", "Skip"))
                return;

            File.WriteAllText(path, JsonConvert.SerializeObject(cfg, Formatting.Indented));
            AssetDatabase.Refresh();
            Debug.Log($"[GraphicsSettings] Generated default: {path}");
        }

        static GraphicsConfig BuildFromCurrentProject()
        {
            var urp = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline
                   ?? (UniversalRenderPipelineAsset)UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;

            var res = Screen.currentResolution;

            return new GraphicsConfig
            {
                ResolutionX    = res.width,
                ResolutionY    = res.height,
                FullscreenMode = Screen.fullScreenMode,
                RefreshRate    = (int)res.refreshRateRatio.numerator,
                TargetFps      = Application.targetFrameRate == -1 ? 60 : Application.targetFrameRate,
                Vsync          = QualitySettings.vSyncCount > 0,

                QualityLevel   = QualitySettings.GetQualityLevel(),
                RenderScale    = urp != null ? urp.renderScale : 1f,
                AntiAliasing   = urp != null ? (int)urp.msaaSampleCount : 2,

                ShadowDistance   = urp != null ? urp.shadowDistance : 100f,
                ShadowCascades   = urp != null ? urp.shadowCascadeCount : 2,
                ShadowResolution = urp != null ? urp.mainLightShadowmapResolution : 1024,

                TextureQuality       = QualitySettings.globalTextureMipmapLimit,
                AnisotropicFiltering = QualitySettings.anisotropicFiltering != AnisotropicFiltering.Disable,
                AnisotropicLevel     = 4,

                RealtimeReflections = QualitySettings.realtimeReflectionProbes,
                LodBias             = QualitySettings.lodBias,
                MaximumLodLevel     = QualitySettings.maximumLODLevel,

                ParticleRaycastBudget = QualitySettings.particleRaycastBudget,
                SoftParticles         = QualitySettings.softParticles,
                SoftVegetation        = QualitySettings.softVegetation,
            };
        }
    }
}
#endif
