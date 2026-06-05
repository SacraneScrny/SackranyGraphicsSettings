using SackranyConfig;

using UnityEngine;

namespace SackranyGraphicsSettings.Configs
{
    public class GraphicsConfig : IDynamicConfig
    {
        public int          ResolutionX         { get; set; } = 1920;
        public int          ResolutionY         { get; set; } = 1080;
        public FullScreenMode FullscreenMode    { get; set; } = FullScreenMode.FullScreenWindow;
        public int          RefreshRate         { get; set; } = 60;
        public int          TargetFps           { get; set; } = 60;
        public bool         Vsync               { get; set; } = true;

        public int          QualityLevel        { get; set; } = 2;
        public float        RenderScale         { get; set; } = 1f;
        public int          AntiAliasing        { get; set; } = 2;
        public bool         Hdr                 { get; set; } = true;

        public int  ShadowResolution { get; set; } = 1024;
        public float            ShadowDistance      { get; set; } = 100f;
        public int              ShadowCascades      { get; set; } = 2;

        public int          TextureQuality      { get; set; } = 0;
        public int          AnisotropicLevel    { get; set; } = 4;
        public bool         AnisotropicFiltering { get; set; } = true;

        public bool         Bloom               { get; set; } = true;
        public bool         AmbientOcclusion    { get; set; } = true;
        public bool         MotionBlur          { get; set; } = false;
        public bool         DepthOfField        { get; set; } = false;
        public bool         ChromaticAberration { get; set; } = false;
        public bool         Vignette            { get; set; } = true;
        public bool         ColorGrading        { get; set; } = true;
        public bool         FilmGrain           { get; set; } = false;
        public bool         Tonemapping         { get; set; } = true;

        public bool         RealtimeReflections { get; set; } = true;
        public bool         RealtimeGI          { get; set; } = false;
        public float        LodBias             { get; set; } = 1f;
        public int          MaximumLodLevel     { get; set; } = 0;

        public int          ParticleRaycastBudget { get; set; } = 256;
        public bool         SoftParticles       { get; set; } = true;
        public bool         SoftVegetation      { get; set; } = true;
        public float        Brightness          { get; set; } = 1f;
        public float        Gamma               { get; set; } = 1f;
    }
}
