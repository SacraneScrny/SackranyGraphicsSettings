# Sackrany.GraphicsSettings

Настройки графики (разрешение, качество, тени, текстуры, URP-ассет) в одном конфиге.
Выделено из растворённого `GameSettings`.

```csharp
ConfigSet<GraphicsConfig>.DoAndSave(c => c.QualityLevel = 3);
GraphicsSettingsApplier.Apply();   // применить к движку
```

В билде сохранённый `GraphicsConfig` применяется автоматически на старте
(`AfterSceneLoad`). В редакторе авто-применение отключено, чтобы не дёргать окно.

**Конфиг:** `GraphicsConfig` (динамический).
**Зависимости:** `Sackrany.Config`, URP.
**Editor:** `Sackrany/GraphicsSettings/Generate Default Config` — снять текущие настройки
проекта в дефолтный json.
