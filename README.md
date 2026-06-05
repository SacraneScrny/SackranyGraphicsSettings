# SackranyGraphicsSettings

Graphics settings (resolution, quality, shadows, textures, URP asset) in a single config.

```csharp
ConfigSet<GraphicsConfig>.DoAndSave(c => c.QualityLevel = 3);
GraphicsSettingsApplier.Apply();   // apply to the engine
```

In a build, the saved `GraphicsConfig` is applied automatically on startup
(`AfterSceneLoad`). In the editor, auto-apply is disabled so it doesn't change
the window resolution/quality.

**Config:** `GraphicsConfig` (dynamic).
**Dependencies:** `SackranyConfig`, URP.
**Editor:** `Sackrany/GraphicsSettings/Generate Default Config` — capture the
current project settings into the default json.
