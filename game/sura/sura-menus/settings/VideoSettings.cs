using Godot;

public partial class VideoSettings : Control
{
	public override void _Ready()
	{
		GetNode<CheckButton>("Panel/ScrollContainer/MarginContainer/VBoxContainer/HBoxContainer/Buttons/FullScreen").ButtonPressed = GameServices.Current.Settings.GetFullscreen();
		GetNode<CheckButton>("Panel/ScrollContainer/MarginContainer/VBoxContainer/HBoxContainer/Buttons/VSync").ButtonPressed = GameServices.Current.Settings.GetVSync();
	}

	public void OnFullscreenToggled(bool enabled) => GameServices.Current.Settings.SetFullscreen(enabled);
	public void OnVSyncToggled(bool enabled) => GameServices.Current.Settings.SetVSync(enabled);
}
