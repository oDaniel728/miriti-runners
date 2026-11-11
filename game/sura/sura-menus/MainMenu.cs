using Godot;

public partial class MainMenu : UISceneRoot
{
	[Signal] public delegate void PlayRequestedEventHandler();
	[Signal] public delegate void SettingsRequestedEventHandler();
	[Signal] public delegate void CreditsRequestedEventHandler();
	[Signal] public delegate void QuitRequestedEventHandler();

	public void OnPlayPressed() => EmitSignal(SignalName.PlayRequested);
	public void OnSettingsPressed() => EmitSignal(SignalName.SettingsRequested);
	public void OnCreditsPressed() => EmitSignal(SignalName.CreditsRequested);
	public void OnQuitPressed() => EmitSignal(SignalName.QuitRequested);
}
