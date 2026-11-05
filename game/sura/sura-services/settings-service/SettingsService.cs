using Godot;

/// <summary>Centralizes user preferences used by menu controls.</summary>
public partial class SettingsService : Service
{
	private const string SettingsPath = "user://settings.cfg";
	private readonly ConfigFile _config = new();

	public override void _Ready()
	{
		_config.Load(SettingsPath);
		ApplyAudioSettings();
		TranslationServer.SetLocale(_config.GetValue("general", "locale", "pt_BR").AsString());
	}

	public void SetAudioBusVolume(string bus, float linear)
	{
		int busIndex = AudioServer.GetBusIndex(bus);
		if (busIndex < 0) return;
		float clamped = Mathf.Clamp(linear, 0f, 1f);
		AudioServer.SetBusVolumeDb(busIndex, clamped == 0f ? -80f : Mathf.LinearToDb(clamped));
		_config.SetValue("audio", bus, clamped);
		Save();
	}

	public float GetAudioBusVolume(string bus)
	{
		int busIndex = AudioServer.GetBusIndex(bus);
		return busIndex < 0 ? 1f : Mathf.DbToLinear(AudioServer.GetBusVolumeDb(busIndex));
	}

	public void SetLocale(string locale)
	{
		TranslationServer.SetLocale(locale);
		_config.SetValue("general", "locale", locale);
		Save();
	}

	public void SetFullscreen(bool enabled)
	{
		if (OS.HasFeature("mobile")) return;
		DisplayServer.WindowSetMode(enabled ? DisplayServer.WindowMode.Fullscreen : DisplayServer.WindowMode.Maximized);
		_config.SetValue("video", "fullscreen", enabled);
		Save();
	}

	public void SetVSync(bool enabled)
	{
		if (OS.HasFeature("mobile")) return;
		DisplayServer.WindowSetVsyncMode(enabled ? DisplayServer.VSyncMode.Enabled : DisplayServer.VSyncMode.Disabled);
		_config.SetValue("video", "vsync", enabled);
		Save();
	}

	public bool GetFullscreen() => _config.GetValue("video", "fullscreen", false).AsBool();
	public bool GetVSync() => _config.GetValue("video", "vsync", true).AsBool();

	private void ApplyAudioSettings()
	{
		foreach (string bus in new[] { "Master", "SFX", "Music", "Ambience" })
		{
			float volume = _config.GetValue("audio", bus, 1f).AsSingle();
			int busIndex = AudioServer.GetBusIndex(bus);
			if (busIndex >= 0)
				AudioServer.SetBusVolumeDb(busIndex, volume == 0f ? -80f : Mathf.LinearToDb(volume));
		}
	}

	private void Save() => _config.Save(SettingsPath);
}
